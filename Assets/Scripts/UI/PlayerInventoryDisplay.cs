using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

[Serializable]
public class SpriteType
{
    public PickUp.PickUpType Type;
    public Sprite Sprite;
}

public class PlayerInventoryDisplay : MonoBehaviour
{
    // Text of slots
    public Text[] CountSlot;

    // Images of slots
    public Image[] ImageSlots;

    // Sprites
    public SpriteType[] Sprites;

    // Number of slot
    private int _nbSlots;

    // Dictionary of sprites
    [HideInInspector]
    public readonly Dictionary<PickUp.PickUpType, Sprite> _spriteDictionary = new Dictionary<PickUp.PickUpType, Sprite>();

    public void Start()
    {
        // Gets the number of slot
        _nbSlots = CountSlot.Length;

        // Populates text of slots
        for (var i = 0; i < _nbSlots; i++)
            CountSlot[i].text = "";

        // Populates sprite of slots
        foreach (var sprite in Sprites)
            _spriteDictionary[sprite.Type] = sprite.Sprite;
    }

    public void OnChangeInventory(Dictionary<PickUp.PickUpType, int> inventory)
    {
        var i = 0;

        // Updates inventory
        foreach (var item in inventory)
        {
            CountSlot[i].text = item.Value.ToString();

            if (item.Value > 0)
                ImageSlots[i].sprite = _spriteDictionary[item.Key];

            i++;
        }
    }
}