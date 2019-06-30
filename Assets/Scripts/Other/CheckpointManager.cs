using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    // Defines if the checkpoint is activated
    private bool _isActivated;

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (!hit || _isActivated)
            return;

        // Gets player component
        var player = hit.GetComponent<Player>();

        // Checks if the enemy can be hurt
        if (player == null || player.IsDead)
            return;
        
        // Activates the checkpoint
        _isActivated = true;
        player.SaveCheckpoint(transform.position);
    }
}