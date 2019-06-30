using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeDisplay : MonoBehaviour
{

    // Text for the Recipe panel
    public Text _recipeText;

    // Text of slots fir the recipe section in the Inventory Panel
    public Text[] CountSlot;

    // Images of slots fir the recipe section in the Inventory Panel
    public Image[] ImageSlots;

    //Recipe Component
    private Recipe _recipeComponent;

    // Use this for initialization
    void Start()
    {


        _recipeComponent = GetComponent<Recipe>();

        // Set the text on the recipe panel
        var recipe = _recipeComponent.recipe;
        string newText = "";

        for (int i = 0; i < recipe.Length; i++)
        {
            newText += "- " + recipe[i].quantity + " " + recipe[i].type + "\n\n";
        }
        _recipeText.text = newText;

        // Set the images for the inventory panel
        int nbSlots = CountSlot.Length;
        for (int i = 0; i < nbSlots; i++)
            CountSlot[i].text = "";

        var sprites = GetComponent<PlayerInventoryDisplay>().Sprites;

        for (int i = 0; i < recipe.Length; i++)
        {
            for (int j = 0; j < sprites.Length; j++)
            {
                if (sprites[j].Type == recipe[i].type && recipe[i].quantity != 0)
                {
                    ImageSlots[i].sprite = sprites[j].Sprite;
                    CountSlot[i].text = recipe[i].quantity.ToString();
                }
            }
        }
    }
}
