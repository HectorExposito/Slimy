using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private Sprite activeCheckpoint;
    private bool isActive;

    public Sound checkpointFx;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        activeCheckpoint = SpritesManager.Instance.GetActiveCheckpointsSprites();
        if (collision.transform.CompareTag(Tags.player))
        {
            if (!isActive)
            {
                AudioManager.Instance.PlaySound(checkpointFx);
                PlayerManager.lastCheckpointPosition = transform.position;
                HeartManager.Instance.RecoverLife();
            }
            isActive = true;
            GetComponent<SpriteRenderer>().sprite = activeCheckpoint;
        }
    }
}
