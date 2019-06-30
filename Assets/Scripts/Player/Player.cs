using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Total lives of the player
    public int TotalLives;
    public AudioClip HitSound;
    public AudioClip pickUpSound;
    public AudioClip HeartSound;

    // Defines the status of the player (Hurt)
    [HideInInspector] public bool IsHurt;

    // Defines the status of the player (Dead)
    [HideInInspector] public bool IsDead;

    // Remaining lives of the player
    private int _remainingLives;

    // Inventory manager script
    private InventoryManager _inventoryManager;

    // Player heart display script
    private PlayerHeartDisplay _playerHeartDisplay;

    // Manager to launch legacy animation
    private Animation _animationManager;

    // Defines the last checkpoint
    private Vector3 _lastCheckpointPosition;
    private AudioSource _source;

    private void Awake()
    {
        // Gets inventory manager component
        _inventoryManager = GetComponent<InventoryManager>();

        // Gets player heart display component
        _playerHeartDisplay = GetComponent<PlayerHeartDisplay>();

        // Gets animation manager
        _animationManager = GetComponent<Animation>();

        // Gets the position of the beginning of the game
        _lastCheckpointPosition = transform.position;
        _source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // Sets the remaining lives of the player to total lives
        _remainingLives = TotalLives;
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        // Checks if the collider is valid
        if (!hit)
            return;

        // Checks if the object can be picked up
        if (hit.CompareTag("PickUp"))
        {
            // Pickup the object
            _source.PlayOneShot(pickUpSound);
            var item = hit.GetComponent<PickUp>();
            _inventoryManager.Add(item);
            Destroy(hit.gameObject);
        }
        else if (hit.CompareTag("Heart"))
        {
            // Pickup heart to regain lives
            if (TotalLives > _remainingLives)
            {
                _source.PlayOneShot(HeartSound);
                _remainingLives++;
                _playerHeartDisplay.OnChangeHeart(TotalLives, _remainingLives);
                
                // Destroys the game object
                Destroy(hit.gameObject);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        // Checks if the remaining lives of the player is greater than or equal to 0
        if (_remainingLives <= 0)
            return;

        // Sets new life
        _source.PlayOneShot(HitSound);
        _remainingLives--;
        _playerHeartDisplay.OnChangeHeart(TotalLives, _remainingLives);
        StartCoroutine(_remainingLives == 0 ? DieAnimation() : HurtAnimation());
    }

    public void ResetPosition()
    {
        // Resets position to the last checkpoint
        if (!IsDead)
            transform.position = _lastCheckpointPosition;
    }

    public void SaveCheckpoint(Vector3 checkpointPosition)
    {
        // Save the position of the last checkpoint
        _lastCheckpointPosition = checkpointPosition;
    }

    private IEnumerator HurtAnimation()
    {
        // Specifying to the enemy that the player has already been hurt
        IsHurt = true;

        // Playing animation
        _animationManager.Play("Hurt");

        // Waiting for the animation finish
        yield return new WaitForSeconds(_animationManager.GetClip("Hurt").averageDuration);

        // Specifying that can be hurt again
        IsHurt = false;
    }

    private IEnumerator DieAnimation()
    {
        // Defines the player as dead
        IsDead = true;

        // Waiting 2 seconds before load game over scene
        yield return new WaitForSeconds(0.5f);

        // Reloads the scene
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
}