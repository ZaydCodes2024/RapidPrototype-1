using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using MEC;
using System.Collections.Generic;
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
        
        topLevelMixer = AnimationMixerPlayable.Create(playableGraph, 2);
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
    public void Destroy() 
    {
        if (playableGraph.IsValid()) {
            playableGraph.Destroy();
        }
    }
}
