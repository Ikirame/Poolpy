using UnityEngine;

public class BoundManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (!hit)
            return;

        // Gets player component
        var player = hit.GetComponent<Player>();

        // Checks if the enemy can be hurt
        if (player == null)
            return;

        player.TakeDamage(1);
        player.ResetPosition();
    }
}