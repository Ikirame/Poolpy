using UnityEngine;

public class PlayerMenuController : MonoBehaviour
{
    // Character controller
    private CharacterController2D _controller;

    // Animator component
    private Animator _animator;

    // Maximum horizontal speed
    public float MaxHorizontalSpeed = 8f;

    // Speed acceleration on ground
    public float SpeedAccelerationOnGround = 10f;

    // Current time
    private float _now = 5f;

    // Sets if the player is moving or not
    private bool _playerIsMoving;

    // Use this for initialization
    private void Start()
    {
        // Gets character controller component
        _controller = GetComponent<CharacterController2D>();

        // Gets animator component
        _animator = GetComponent<Animator>();

        _playerIsMoving = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_playerIsMoving)
        {
            // Sets the new horizontal force
            _controller.SetHorizontalForce(Mathf.Lerp(_controller.Velocity.x,
                Vector2.right.x * MaxHorizontalSpeed, Time.deltaTime * SpeedAccelerationOnGround));

            _animator.SetFloat("MoveSpeed", Mathf.Abs(Vector2.right.x));
        }
        else
        {
            _now -= Time.deltaTime;

            // Flips the player
            if (_now <= 0)
            {
                Flip();
                _now = 5f;
            }
        }

        // Sets the animations of the player
        _animator.SetBool("IsGrounded", _controller.IsGrounded);
    }

    private void Flip()
    {
        // Flips the orientation of the player
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void MovingPlayer()
    {
        if (transform.localScale.x < 0)
            Flip();

        _playerIsMoving = true;
    }
}