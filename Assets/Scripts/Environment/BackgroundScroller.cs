using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class BackgroundScroller : MonoBehaviour
{
    // Target used by the sprite to set the new offset
    public Transform Target;

    // Speed for scrolling background
    public float ScrollingSpeed = 0.01f;

    // The renderer of the sprite
    private MeshRenderer _renderer;

    // The offset of the sprite
    private Vector2 _offset;

    private void Start()
    {
        // Gets the renderer component
        _renderer = GetComponent<MeshRenderer>();

        // Gets the offset of the sprite
        _offset = _renderer.material.mainTextureOffset;
    }
    
    private void FixedUpdate()
    {
        // Update the offset of sprite according to target position
        _offset.x = Target.position.x;
        _renderer.material.mainTextureOffset = _offset * ScrollingSpeed;
    }
}