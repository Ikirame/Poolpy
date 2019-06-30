using UnityEngine;

public class Moving : MonoBehaviour
{
    // Horizontal speed of the movement
    public float HorizontalSpeed;

    // Vertical speed of the movement
    public float VerticalSpeed;

    // Duration of the movement
    public float Duration;

    // Direction of the movement
    private int _direction;

    // Duration time minus current time
    private float _now;

    private void Start()
    {
        // Sets now to duration time
        _now = Duration;

        // Sets direction of the movement
        _direction = 1;
    }

    private void Update()
    {
        // Updates current time
        _now -= Time.deltaTime;

        // Creates new position
        var newPosition = new Vector3(transform.position.x + _direction * HorizontalSpeed * Time.deltaTime,
            transform.position.y + _direction * VerticalSpeed * Time.deltaTime, transform.position.z);

        // Sets the new position
        transform.position = newPosition;

        if (!(_now <= 0))
            return;

        // Update the direction and the current time
        _direction *= -1;
        _now += Duration;
    }
}