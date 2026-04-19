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
        PlaySound(audioClipSO.footstepSounds, position, volumeMultiplier * volume);
    }
    public void PlayWeaponShootSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipSO.weaponShootSound, position, volumeMultiplier * volume);
    }

    public void PlayJumpSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipSO.jumpSounds, position, volumeMultiplier * volume);
    }
    public void PlayLandSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipSO.weaponLandSound, position, volumeMultiplier * volume);
    }
    public void PlayPlayerHurtSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipSO.playerHurtSounds, position, volumeMultiplier * volume);
    }
    public void PlayEnemyHurtSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipSO.enemyHurtSounds, position, volumeMultiplier * volume);
    }
    public void PlayEnemyDeathSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipSO.enemyDeathSounds, position, volumeMultiplier * volume);
    }
    public void PlayGameOverSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipSO.gameOverSounds, position, volumeMultiplier * volume);
    }
    public void ChangeVolume(float value)
    {
        volume = value;
        
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
