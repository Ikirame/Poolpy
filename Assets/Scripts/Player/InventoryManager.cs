using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Defines items in the inventory of the player
    [HideInInspector] public Dictionary<PickUp.PickUpType, int> Items = new Dictionary<PickUp.PickUpType, int>();

    // Script which is interact with the display
    private PlayerInventoryDisplay _playerInventoryDisplay;

    private void Awake()
    {
        // Gets the player inventory display script component
        _playerInventoryDisplay = GetComponent<PlayerInventoryDisplay>();
    }

    private void Start()
    {
        // Updates the inventory
        _playerInventoryDisplay.OnChangeInventory(Items);
    }

    public void Add(PickUp item)
    {
        int oldTotal;

        // Gets the total of an object and add 1 if the object already exists
        if (Items.TryGetValue(item.Type, out oldTotal))
            Items[item.Type] = oldTotal + 1;
        else
            Items.Add(item.Type, 1);

        // Updates the inventory
        _playerInventoryDisplay.OnChangeInventory(Items);
    }

    public void Remove(PickUp item)
    {
        int oldTotal;

        // Gets the total of an object and remove 1 if the object exists
        if (Items.TryGetValue(item.Type, out oldTotal))
        {
            if (oldTotal > 0)
                Items[item.Type] = oldTotal - 1;
        }

        // Updates the inventory
        _playerInventoryDisplay.OnChangeInventory(Items);
    }
}