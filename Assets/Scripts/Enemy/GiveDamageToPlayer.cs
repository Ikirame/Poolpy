using UnityEngine;

public class GiveDamageToPlayer : MonoBehaviour
{
    // Damage to give to player
    public int DamageToGive = 1;

    // Last position of the enemy
    private Vector2 _lastPosition;

    // Velocity of the enemy
    private Vector2 _velocity;

    // Update is called once per frame
    private void LateUpdate()
    {
        // Gets the velocity of the enemy
        _velocity = (_lastPosition - (Vector2)transform.position) / Time.deltaTime;

        // Gets the last position of the enemy
        _lastPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        // Gets player component
        var player = hit.GetComponent<Player>();

        // Checks if the enemy can be hurt
        if (player == null || player.IsHurt || player.IsDead)
            return;

        // Calls the damage method of the player
        player.TakeDamage(DamageToGive);
        var controller = hit.GetComponent<CharacterController2D>();
        var totalVelocity = controller.Velocity + _velocity;

        // Knockback the player
        controller.SetForce(new Vector2(
            -1 * Mathf.Sign(totalVelocity.x) * Mathf.Clamp(Mathf.Abs(totalVelocity.x) * 5, 10, 20),
            -1 * Mathf.Sign(totalVelocity.y) * Mathf.Clamp(Mathf.Abs(totalVelocity.y) * 2, 0, 15)));
    }
}