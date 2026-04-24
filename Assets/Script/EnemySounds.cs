using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    [SerializeField] private bool setRandomPitchOnStart;
    [SerializeField] Vector2 randomPitch = new Vector3(0.9f, 1.4f);
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {   
        if (setRandomPitchOnStart) SetRandomPitch();
    }

    private void Update()
    {
        if (GameInput.Instance.IsGamePaused())
        {
            audioSource.enabled = false;
        }
        else
        {
            audioSource.enabled = true;
        }

        audioSource.volume = SoundManager.Instance.GetVolume();
    }
    private void SetRandomPitch()
    {
        if (audioSource != null)
        {
            audioSource.pitch = Random.Range(randomPitch.x, randomPitch.y);
        }
    }
}
