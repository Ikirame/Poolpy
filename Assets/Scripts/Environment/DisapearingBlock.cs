using UnityEngine;
using UnityEngine.Tilemaps;

public class DisapearingBlock : MonoBehaviour
{
    // Speed of disappearance
    public float Speed;

    // default Color
    private Color _color;

    // Current time
    private float _now;

    // TileMap component
    private Tilemap _tileMap;

    // TileMap collider component
    private TilemapCollider2D _collider;

    private void Start()
    {
        // Gets tileMap component
        _tileMap = GetComponent<Tilemap>();

        // Gets tileMap collider component
        _collider = GetComponent<TilemapCollider2D>();

        // Sets current time to speed
        _now = Speed;

        // Gets default color
        _color = _tileMap.color;
    }

    private void Update()
    {
        // Sets current at each update
        _now -= Time.deltaTime;

        // Checks the time
        if (_now > 0)
            return;

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (_tileMap.color.a == 0f)
        {
            _color.a = 1f;
            _collider.enabled = true;
        }
        else
        {
            _color.a = 0f;
            _collider.enabled = false;
        }

        // Sets the new color
        _tileMap.color = _color;

        // Sets the current time to speed
        _now = Speed;
    }
}