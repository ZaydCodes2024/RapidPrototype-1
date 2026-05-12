using UnityEngine;

public class GameUISounds : MonoBehaviour
{
    public void PlayRoundNumberTextSound()
    {
        SoundManager.Instance.PlayRoundNumberEndSound(Player.Instance.transform.position, 1.5f);
    }
}
