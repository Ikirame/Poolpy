using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour
{
    // Content game object prefab
    public GameObject Content;

    // Checks if a key is needed to open this chest
    public bool NeedKey = false;

    // Text asset
    public TextAsset Text;

    // Text box manager game object
    public GameObject TextBoxManager;

    // Player component
    private Player _player;

    // Checks if the chest for a key is pressed
    private bool _waitForPress;

    // Checks if the chest is opened
    private bool _isOpen;

    // Animator component
    private Animator _animator;

    // Transform of the container
    private Transform _container;

    //Press_E sprite
    public GameObject _press_E;

    public AudioClip OpenSound;

    // Content collider
    private Collider2D _contentCollider;
    private AudioSource _source;


    private void Start()
    {
        // Gets player object
        _player = FindObjectOfType<Player>();

        // Gets collider component
        _contentCollider = Content.GetComponent<Collider2D>();

        // Sets the content inactive and collider to disable
        Content.SetActive(false);
        _contentCollider.enabled = false;

        // Gets the animator component
        _animator = GetComponentInChildren<Animator>();

        //Hide the press_E sprite
        _press_E.SetActive(false);

        // Gets the transform of the container
        _container = gameObject.transform.GetChild(0).GetChild(0);
        Content.transform.SetParent(_container.transform);
        Content.transform.localPosition = new Vector3(0, 0, 0);

        _source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Checks if the chest is already opened
        if (_isOpen || !_waitForPress || !Input.GetButtonDown("Action"))
            return;

        // Checks if the chest needs a key to be open
        if (NeedKey)
        {
            // Gets the inventory of the player
            var inventory = _player.GetComponent<InventoryManager>().Items;

            // Checks if the player holds the key and destroy it
            if (inventory.ContainsKey(PickUp.PickUpType.Key) && inventory[PickUp.PickUpType.Key] > 0)
                inventory.Remove(PickUp.PickUpType.Key);
            else
            {
                // Enables a text of the player can't open the chest
                TextBoxManager.GetComponent<TextBoxManager>().ReloadScript(Text, 0, 0);
                TextBoxManager.GetComponent<TextBoxManager>().EnableTextBox();
                return;
            }
        }

        // Calls the method to open the chest
        Open();
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        // Checks if the player is in the collision box of the chest
        if (!hit || !hit.CompareTag("Player") || _isOpen)
            return;

        // Sets the wait for press to true
        _press_E.SetActive(true);
        _waitForPress = true;
    }

    private void OnTriggerExit2D(Collider2D hit)
    {
        // Checks if the player is out the collision box of the chest
        if (!hit || !hit.CompareTag("Player"))
            return;

        // Sets the wait for press to false
        _press_E.SetActive(false);
        _waitForPress = false;
    }

    private void Open()
    {
        // Opens the chest
        _source.PlayOneShot(OpenSound);
        _isOpen = true;
        _animator.SetTrigger("Open");
        Content.SetActive(true);
        StartCoroutine(CantBePickedUp());
    }

    private IEnumerator CantBePickedUp()
    {
        // Waiting for 1 second before to enable collider of the content
        yield return new WaitForSeconds(1);

        // Activates collide of the content
        _contentCollider.enabled = true;
    }
}