using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    // Maximum horizontal speed
    public float MaxHorizontalSpeed = 8f;

    // Magnitude for a player who is jumping
    public float JumpMagnitude = 12f;

    // Speed acceleration on ground
    public float SpeedAccelerationOnGround = 10f;

    // Speed acceleration in air
    public float SpeedAccelerationInAir = 5f;

    // Character controller
    private CharacterController2D _controller;

    // Animator component
    private Animator _animator;

    // Defines player script
    private Player _playerScript;

    // Normalized horizontal speed
    private float _normalizedHorizontalSpeed;

    // Direction of the player
    private bool _isFacingRight;

    // Use this for initialization
    private void Start()
    {
        // Gets character controller component
        _controller = GetComponent<CharacterController2D>();

        // Gets animator component
        _animator = GetComponent<Animator>();

        // Gets player script
        _playerScript = GetComponent<Player>();

        // Sets the default direction of the player
        _isFacingRight = transform.localScale.x > 0;
    }

    // Update is called once per frame
    private void Update()
    {
        // Checks if the player is dead
        if (_playerScript.IsDead)
        {
            // Checks if the animation has been launched
            if (_animator.GetBool("IsDead"))
                return;

            // Otherwise the animation is launched
            _animator.SetBool("IsDead", true);
            _controller.IsAvailable = false;
        }
        else if (_controller.IsAvailable)
        {
            // Handle inputs
            HandleInput();

            // Sets the new horizontal force
            var movementFactor = _controller.IsGrounded ? SpeedAccelerationOnGround : SpeedAccelerationInAir;
            _controller.SetHorizontalForce(Mathf.Lerp(_controller.Velocity.x,
                _normalizedHorizontalSpeed * MaxHorizontalSpeed, Time.deltaTime * movementFactor));
        }
    }

    private void HandleInput()
    {
        // Gets the horizontal movement
        _normalizedHorizontalSpeed = Input.GetAxisRaw("Horizontal");

        // Flips the player
        Flip();

        // Sets the animations of the player
        _animator.SetBool("IsGrounded", _controller.IsGrounded);
        _animator.SetFloat("JumpForce", _controller.Velocity.y);
        _animator.SetFloat("MoveSpeed", Mathf.Abs(_normalizedHorizontalSpeed));

        // Checks if the player is grounded and the button "Jump" is pressed and jump
        if (_controller.IsGrounded && Input.GetButton("Jump"))
            Jump();
    }

    private void Jump()
    {
        // Checks if the player has already a vertical force
        if (_controller.Velocity.y > 0.001f)
            _controller.SetVerticalForce(0);

        // Adds the new vertical force
        _controller.AddForce(new Vector2(_controller.Velocity.x, JumpMagnitude));
    }

    private void Flip()
    {
        // Checks if the player needs to be flip
        if (_normalizedHorizontalSpeed >= 1 && !_isFacingRight ||
            _normalizedHorizontalSpeed <= -1 && _isFacingRight)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        // Applies the flip
        _isFacingRight = transform.localScale.x > 0;
    }

    public void DeactivatePlayer()
    {
        // Deactivates controller on player
        if (_controller != null)
            _controller.IsAvailable = false;
    }

    public void ActivatePlayer()
    {
        // Activates controller on player
        if (_controller != null)
            _controller.IsAvailable = true;
    }
}