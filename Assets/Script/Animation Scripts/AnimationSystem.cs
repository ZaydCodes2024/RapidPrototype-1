using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using MEC;
using System.Collections.Generic;
using System;
public class AnimationSystem
{
    private PlayableGraph playableGraph;
    private List<AnimationClipPlayable> clipPlayableList = new List<AnimationClipPlayable>();
    readonly AnimationMixerPlayable topLevelMixer;
    readonly AnimationMixerPlayable locomotionMixer;
    private AnimationClipPlayable oneShotPlayable;
    CoroutineHandle blendInHandle;
    CoroutineHandle blendOutHandle;
    public AnimationSystem(Animator animator, List<AnimationClip> animationClip) 
    {
        playableGraph = PlayableGraph.Create("AnimationSystem");
        
        AnimationPlayableOutput playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", animator);
        
        topLevelMixer = AnimationMixerPlayable.Create(playableGraph, 3);
        playableOutput.SetSourcePlayable(topLevelMixer);
        
        locomotionMixer = AnimationMixerPlayable.Create(playableGraph, animationClip.Count);
        topLevelMixer.ConnectInput(0, locomotionMixer, 0);
        topLevelMixer.SetInputWeight(0, 1f);

        for (int i = 0; i < animationClip.Count; i++)
        {
            var clipPlayable = AnimationClipPlayable.Create(playableGraph, animationClip[i]);

            clipPlayable.GetAnimationClip().wrapMode = WrapMode.Loop;

            locomotionMixer.ConnectInput(i, clipPlayable, 0);
            locomotionMixer.SetInputWeight(i, i == 0 ? 1f : 0f); // default first active

            clipPlayableList.Add(clipPlayable);
        }
        
        playableGraph.Play();
        
        oneShotPlayable = AnimationClipPlayable.Create(playableGraph, animationClip[0]); // dummy clip

        topLevelMixer.ConnectInput(1, oneShotPlayable, 0);
        topLevelMixer.SetInputWeight(1, 0f);
    }
    
    public void UpdateLocomotion() 
    {
       float targetSpeed = GameInput.Instance.GetMovementSpeed();
       Vector2 input = GameInput.Instance.GetMovementVector();
       float inputMagnitude = input.magnitude;
       float currentSpeed = inputMagnitude * targetSpeed;

       float weight = Mathf.InverseLerp(0f, PlayerMovement.Instance.GetRunSpeed(), currentSpeed);
       locomotionMixer.SetInputWeight(0, 1f - weight);
       locomotionMixer.SetInputWeight(1, weight);
    }
    public void PlayOneShot(AnimationClip oneShotClip) 
    {
        InterruptOneShot();

        oneShotPlayable = AnimationClipPlayable.Create(playableGraph, oneShotClip);

        topLevelMixer.ConnectInput(1, oneShotPlayable, 0);
        topLevelMixer.SetInputWeight(1, 1f);
        
        // Calculate blendDuration as 10% of clip length,
        // but ensure that it's not less than 0.1f or more than half the clip length
        float blendDuration = Mathf.Clamp(oneShotClip.length * 0.1f, 0.1f, oneShotClip.length * 0.5f);
        
        BlendIn(blendDuration);
        BlendOut(blendDuration, oneShotClip.length - blendDuration);
    }
    private void BlendIn(float duration) 
    {
        blendInHandle = Timing.RunCoroutine(Blend(duration, blendTime => 
        {
            float weight = Mathf.Lerp(1f, 0f, blendTime);
            topLevelMixer.SetInputWeight(0, weight);
            topLevelMixer.SetInputWeight(1, 1f - weight);
        }));
    }
    
    private void BlendOut(float duration, float delay) 
    {
        blendOutHandle = Timing.RunCoroutine(Blend(duration, blendTime => 
        {
            float weight = Mathf.Lerp(0f, 1f, blendTime);
            topLevelMixer.SetInputWeight(0, weight);
            topLevelMixer.SetInputWeight(1, 1f - weight);
        }, delay, DisconnectOneShot));
    }
    private IEnumerator<float> Blend(float duration, Action<float> blendCallback, float delay = 0f, Action finishedCallback = null) 
    {
        if (delay > 0f) {
            yield return Timing.WaitForSeconds(delay);
        }
        
        float blendTime = 0f;
        while (blendTime < 1f) {
            blendTime += Time.deltaTime / duration;
            blendCallback(blendTime);
            yield return Timing.WaitForOneFrame;
        }
        
        blendCallback(1f);
        
        finishedCallback?.Invoke();
    }
    private void InterruptOneShot() 
    {
        Timing.KillCoroutines(blendInHandle);
        Timing.KillCoroutines(blendOutHandle);
        
        topLevelMixer.SetInputWeight(0, 1f);
        topLevelMixer.SetInputWeight(1, 0f);

        if (oneShotPlayable.IsValid()) {
            DisconnectOneShot();
        }
    }

    private void DisconnectOneShot() 
    {

        topLevelMixer.DisconnectInput(1);
        playableGraph.DestroyPlayable(oneShotPlayable);
    }
    public void Destroy() 
    {
        Timing.KillCoroutines(blendInHandle);
        Timing.KillCoroutines(blendOutHandle);

        if (playableGraph.IsValid()) {
            playableGraph.Destroy();
        }
    }
}
