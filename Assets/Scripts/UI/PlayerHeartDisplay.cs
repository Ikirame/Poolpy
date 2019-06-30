using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeartDisplay : MonoBehaviour
{
    // Full heart sprite
    public Sprite HeartSprite;

    // Empty heart sprite
    public Sprite NoHeartSprite;

    // List of heart sprites
    public List<Image> Images = new List<Image>();

    private void Start()
    {
        // Updates display
        Player player = GetComponent<Player>();
        OnChangeHeart(player.TotalLives, player.TotalLives);
    }

    public void OnChangeHeart(int totalLives, int remainingLives)
    {
        // Updates sprites
        for (var i = 0; i < totalLives; i++)
            Images[i].sprite = i >= remainingLives ? NoHeartSprite : HeartSprite;
    }
}