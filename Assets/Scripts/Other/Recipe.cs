using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Ingredient
{
    public PickUp.PickUpType type;
    public int quantity;
}

public class Recipe : MonoBehaviour {

    public Ingredient[] recipe;
    public Sprite DishSprite;
    public Sprite MissedSprite;

    [HideInInspector]
    public int MaxScore = 0;

    private Dictionary<PickUp.PickUpType, int> recipeDictionnary = new Dictionary<PickUp.PickUpType, int>();

	// Use this for initialization
	void Start () {

        foreach (Ingredient ing in recipe)
        {
            recipeDictionnary[ing.type] = ing.quantity;
            MaxScore += ing.quantity * 100;
        }
    }


    public int GetScore(Dictionary<PickUp.PickUpType, int> items)
    {
        int score = 0;

        foreach (var item in items)
        {
            int inventory_quantity = item.Value;
            int recipe_quantity = 0;
            recipeDictionnary.TryGetValue(item.Key, out recipe_quantity);

            int diff = recipe_quantity - inventory_quantity;
            if (diff == 0)
            {
                score += recipe_quantity * 100;
            } else
            {
                score += diff * 100;
            }
        }
        return (score);
    }
}
