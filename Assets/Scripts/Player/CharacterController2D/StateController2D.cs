using UnityEngine;

public struct CollisionState
{
    public bool IsCollidingLeft;
    public bool IsCollidingRight;
    public bool IsCollidingAbove;
    public bool IsCollidingBelow;
}

public struct SlopeState
{
    public bool IsMovingUpSlope;
    public bool IsMovingDownSlope;
}

public class StateController2D
{
    // Collision states
    public CollisionState CollisionStates;

    // Slope states
    public SlopeState SlopeStates;

    // Slope angle
    public float SlopeAngle;

    // Slope limit tangent
    public float SlopeLimitTangent = Mathf.Tan(75f * Mathf.Deg2Rad);

    public void Reset()
    {
        // Resets the state of collisions
        CollisionStates.IsCollidingLeft = false;
        CollisionStates.IsCollidingRight = false;
        CollisionStates.IsCollidingAbove = false;
        CollisionStates.IsCollidingBelow = false;

        // Resets the state of slopes
        SlopeStates.IsMovingUpSlope = false;
        SlopeStates.IsMovingDownSlope = false;

        // Resets slope angle
        SlopeAngle = 0;
    }
}