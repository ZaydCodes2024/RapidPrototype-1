using UnityEngine;

public class PlayGameOver : MonoBehaviour
{
    private void Awake()
    {
        SoundManager.Instance.PlayGameOverSound(transform.position, 10f);
    }
}
