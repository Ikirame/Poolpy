using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CharacterController2D : MonoBehaviour
{
    // Gravity applied to the character
    public float Gravity = -25f;

    // The limit slope angle hat the character can climb
    [Range(0, 90)] public float SlopeAngleLimit = 45f;

    // Slope speed
    public float SlopeSpeed = 0.5f;

    // The layer which represent the ground
    public LayerMask GroundMask;

    // Defines if the player is interact able
    public bool IsAvailable;

    // Velocity of the character
    [HideInInspector] public Vector2 Velocity;

    // Property to know if the character is grounded or not
    [HideInInspector]
    public bool IsGrounded
    {
        get { return _stateController.CollisionStates.IsCollidingBelow; }
    }

    // State controller
    [HideInInspector] private StateController2D _stateController;

    // RayCast controller
    private RayCastController2D _rayCastController;

    // Platform controller
    private PlatformController2D _platformController;

    // Character transform
    private Transform _transform;

    // Character collider
    private Collider2D _collider;

    public void Awake()
    {
        // Caches the character transform
        _transform = transform;

        // Gets the collider component
        _collider = GetComponent<BoxCollider2D>();

        // Sets all controllers
        _stateController = new StateController2D();
        _rayCastController = new RayCastController2D(_collider);
        _platformController = new PlatformController2D();
    }

    public void AddForce(Vector2 force)
    {
        // Adds force to the current velocity
        Velocity = force;
    }

    public void SetHorizontalForce(float x)
    {
        // Sets new horizontal velocity
        Velocity.x = x;
    }

    public void SetVerticalForce(float y)
    {
        // Sets new vertical velocity
        Velocity.y = y;
    }

    public void SetForce(Vector2 force)
    {
        // Sets new force
        Velocity += force;
    }

    public void LateUpdate()
    {
        if (!IsAvailable)
            return;

        // Updates velocity
        Velocity.y += Gravity * Time.deltaTime;

        // Moves character
        Move(Velocity * Time.deltaTime);
    }

    private void Move(Vector2 deltaMovement)
    {
        // Gets the below collision state
        var wasGrounded = _stateController.CollisionStates.IsCollidingBelow;

        // Resets all states
        _stateController.Reset();

        // Handles platform
        _platformController.HandlePlatforms(_transform);

        // Handles enemies


        // Calculates all ray origins
        _rayCastController.CalculateRayOrigins(_collider, transform);

        // Handles vertical slope
        if (deltaMovement.y < 0 && wasGrounded)
            HandleVerticalSlope(ref deltaMovement);

        // Moves character horizontally
        if (Mathf.Abs(deltaMovement.x) > .001f)
            MoveHorizontally(ref deltaMovement);

        // Moves character vertically
        MoveVertically(ref deltaMovement);

        // Corrects the placement when a platform hit the character and the character is not moving
        CorrectHorizontalPlacement(ref deltaMovement, true);
        CorrectHorizontalPlacement(ref deltaMovement, false);

        // Applies translation
        _transform.Translate(deltaMovement, Space.World);

        if (Time.deltaTime > 0)
            Velocity = deltaMovement / Time.deltaTime;

        // Sets vertical velocity to 0 when the character climbing a slope
        if (_stateController.SlopeStates.IsMovingUpSlope)
            Velocity.y = 0;

        // Checks if the character is on a platform
        if (_platformController.Platform == null)
            return;

        // Sets the new position of the platform
        _platformController.ActiveGlobalPlatformPoint = transform.position;
        _platformController.ActiveLocalPlatformPoint =
            _platformController.Platform.transform.InverseTransformPoint(transform.position);
    }

    private void CorrectHorizontalPlacement(ref Vector2 deltaMovement, bool isRight)
    {
        var halfWidth = _collider.bounds.size.x / 2;
        var rayOrigin = isRight ? _rayCastController.RayCastBottomRight : _rayCastController.RayCastBottomLeft;

        if (isRight)
            rayOrigin.x -= halfWidth - RayCastController2D.SkinWidth;
        else
            rayOrigin.x += halfWidth - RayCastController2D.SkinWidth;

        var rayDirection = isRight ? Vector2.right : Vector2.left;
        var offset = 0f;

        for (var i = 1; i < RayCastController2D.HorizontalRaysCount - 1; i++)
        {
            var rayVector = new Vector2(deltaMovement.x + rayOrigin.x,
                deltaMovement.y + rayOrigin.y + i * _rayCastController.VerticalDistanceBetweenRays);

            var rayCastHit = Physics2D.Raycast(rayVector, rayDirection, halfWidth, GroundMask);
            if (!rayCastHit)
                continue;

            offset = isRight
                ? rayCastHit.point.x - _transform.position.x - halfWidth
                : halfWidth - (_transform.position.x - rayCastHit.point.x);
        }

        deltaMovement.x += offset;
    }

    private void MoveHorizontally(ref Vector2 deltaMovement)
    {
        var isGoingRight = deltaMovement.x > 0;
        var rayDistance = Mathf.Abs(deltaMovement.x) + RayCastController2D.SkinWidth;
        var rayDirection = isGoingRight ? Vector2.right : Vector2.left;
        var rayOrigin = isGoingRight ? _rayCastController.RayCastBottomRight : _rayCastController.RayCastBottomLeft;

        for (var i = 0; i < RayCastController2D.HorizontalRaysCount; i++)
        {
            var rayVector = new Vector2(rayOrigin.x, rayOrigin.y + i * _rayCastController.VerticalDistanceBetweenRays);

            Debug.DrawRay(rayVector, rayDirection * rayDistance, Color.red);

            var rayCastHit = Physics2D.Raycast(rayVector, rayDirection, rayDistance, GroundMask);
            if (!rayCastHit)
                continue;

            if (i == 0 && HandleHorizontalSlope(ref deltaMovement, Vector2.Angle(rayCastHit.normal, Vector2.up),
                    isGoingRight))
                break;

            deltaMovement.x = rayCastHit.point.x - rayVector.x;
            rayDistance = Mathf.Abs(deltaMovement.x);

            if (isGoingRight)
            {
                deltaMovement.x -= RayCastController2D.SkinWidth;
                _stateController.CollisionStates.IsCollidingRight = true;
            }
            else
            {
                deltaMovement.x += RayCastController2D.SkinWidth;
                _stateController.CollisionStates.IsCollidingLeft = true;
            }

            if (rayDistance < RayCastController2D.SkinWidth + .0001f)
                break;
        }
    }

    private void MoveVertically(ref Vector2 deltaMovement)
    {
        var isGoingUp = deltaMovement.y > 0;
        var rayDistance = Mathf.Abs(deltaMovement.y) + RayCastController2D.SkinWidth;
        var rayDirection = isGoingUp ? Vector2.up : -Vector2.up;
        var rayOrigin = isGoingUp ? _rayCastController.RayCastTopLeft : _rayCastController.RayCastBottomLeft;

        rayOrigin.x += deltaMovement.x;

        var standingOnDistance = float.MaxValue;
        for (var i = 0; i < RayCastController2D.VerticalRaysCount; i++)
        {
            var rayVector = new Vector2(rayOrigin.x + i * _rayCastController.HorizontalDistanceBetweenRays,
                rayOrigin.y);
            Debug.DrawRay(rayVector, rayDirection * rayDistance, Color.red);

            var rayCastHit = Physics2D.Raycast(rayVector, rayDirection, rayDistance, GroundMask);
            if (!rayCastHit)
                continue;

            if (!isGoingUp)
            {
                var verticalDistanceToHit = _transform.position.y - rayCastHit.point.y;
                if (verticalDistanceToHit < standingOnDistance)
                {
                    standingOnDistance = verticalDistanceToHit;
                    _platformController.Platform = rayCastHit.collider.gameObject;
                }
            }

            deltaMovement.y = rayCastHit.point.y - rayVector.y;
            rayDistance = Mathf.Abs(deltaMovement.y);

            if (isGoingUp)
            {
                deltaMovement.y -= RayCastController2D.SkinWidth;
                _stateController.CollisionStates.IsCollidingAbove = true;
            }
            else
            {
                deltaMovement.y += RayCastController2D.SkinWidth;
                _stateController.CollisionStates.IsCollidingBelow = true;
            }

            if (!isGoingUp && deltaMovement.y > .0001f)
                _stateController.SlopeStates.IsMovingUpSlope = true;

            if (rayDistance < RayCastController2D.SkinWidth + .0001f)
                break;
        }
    }

    private void HandleVerticalSlope(ref Vector2 deltaMovement)
    {
        var center = (_rayCastController.RayCastBottomLeft.x + _rayCastController.RayCastBottomRight.x) / 2;
        var direction = -Vector2.up;

        var slopeDistance = _stateController.SlopeLimitTangent * (_rayCastController.RayCastBottomRight.x - center);
        var slopeRayVector = new Vector2(center, _rayCastController.RayCastBottomLeft.y);

        Debug.DrawRay(slopeRayVector, direction * slopeDistance, Color.yellow);

        var rayCastHit = Physics2D.Raycast(slopeRayVector, direction, slopeDistance, GroundMask);
        if (!rayCastHit)
            return;

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        var isMovingDownSlope = Mathf.Sign(rayCastHit.normal.x) == Mathf.Sign(deltaMovement.x);
        if (!isMovingDownSlope)
            return;

        var angle = Vector2.Angle(rayCastHit.normal, Vector2.up);
        if (Mathf.Abs(angle) < .0001f)
            return;

        _stateController.SlopeStates.IsMovingDownSlope = true;
        _stateController.SlopeAngle = angle;

        deltaMovement.y = rayCastHit.point.y - slopeRayVector.y;
    }

    private bool HandleHorizontalSlope(ref Vector2 deltaMovement, float angle, bool isGoingRight)
    {
        if (Mathf.RoundToInt(angle) == 90)
            return false;

        if (angle > SlopeAngleLimit)
        {
            deltaMovement.x = 0;
            return true;
        }

        if (deltaMovement.y > .07f)
            return true;
        
        //deltaMovement.x += isGoingRight ? -RayCastController2D.SkinWidth : RayCastController2D.SkinWidth;
        deltaMovement.y = Mathf.Abs(Mathf.Tan(angle * Mathf.Deg2Rad) * deltaMovement.x);

        _stateController.SlopeStates.IsMovingUpSlope = true;
        _stateController.CollisionStates.IsCollidingBelow = true;

        return true;
    }
}