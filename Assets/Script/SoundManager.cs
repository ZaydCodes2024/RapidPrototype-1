using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClipSO audioClipSO;
    public static SoundManager Instance {get; private set;}
    private float volume = 1f;
    private const string PLAYER_PREFS_SFX_VOLUME = "SoundEffectsVolume";
    private void Awake()
    {
        Instance = this;
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SFX_VOLUME, 1f);
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1)
    {
        AudioSource.PlayClipAtPoint(audioClip,position, volume);
    }
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length)],position, volume);
    }
    public void PlayFootstepSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipSO.footstep, position, volumeMultiplier * volume);
    }
    public void ChangeVolume()
    {
        volume += .1f;
        if (volume > 1f)
            volume = 0f;

        PlayerPrefs.SetFloat(PLAYER_PREFS_SFX_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
