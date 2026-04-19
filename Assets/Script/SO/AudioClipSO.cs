using UnityEngine;

[CreateAssetMenu()]
public class AudioClipSO : ScriptableObject
{
    public AudioClip[] footstepSounds;
    public AudioClip[] jumpSounds;
    public AudioClip[] playerHurtSounds;
    public AudioClip[] enemyHurtSounds;
    public AudioClip[] enemyDeathSounds;
    public AudioClip[] gameOverSounds;
    public AudioClip weaponShootSound;
    public AudioClip weaponLandSound;
   
}
