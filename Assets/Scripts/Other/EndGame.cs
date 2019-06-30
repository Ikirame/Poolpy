using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

    public GameObject EndGamePanel;
    public GameObject YesNoPanel;
    public Text ScoreText;
    public Text EndText;
    public Image DishImage;

    private bool _waitForPress;
    private int _enterCount = 0;

	// Use this for initialization
	void Start () {
        EndGamePanel.SetActive(false);
        YesNoPanel.SetActive(false);
	}

    private void Update()
    {
        if (Input.GetButtonDown("Action") && _waitForPress)
        {
            ShowYesNo();
        }
    }

    public void ShowYesNo()
    {
        YesNoPanel.SetActive(true);
        EndGamePanel.SetActive(false);

        // Deactivates player during text box was active
        FindObjectOfType<Player>().GetComponent<PlayerController2D>().DeactivatePlayer();

        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(GameObject.Find("YesButton").gameObject, null);
    }

    public void HideYesNo()
    {
        YesNoPanel.SetActive(false);

        // Activates player after the text box was active
        FindObjectOfType<Player>().GetComponent<PlayerController2D>().ActivatePlayer();
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        // Checks if the collider is valid
        if (!hit)
            return;

        // Checks if the collider is the player
        if (!hit.CompareTag("Player"))
            return;

        _enterCount++;
        if (_enterCount > 1)
        {
            EndGamePanel.SetActive(true);
            _waitForPress = true;
        }
    }

    private void OnTriggerExit2D(Collider2D hit)
    {
        // Checks if the collider is valid
        if (!hit)
            return;

        // Checks if the collider is the player
        if (!hit.CompareTag("Player"))
            return;

        EndGamePanel.SetActive(false);
        _waitForPress = false;
    }

    public void ShowScore()
    {
        Player player = FindObjectOfType<Player>();
        Recipe recipe = player.GetComponent<Recipe>();
        int score = recipe.GetScore(player.GetComponent<InventoryManager>().Items);

        ScoreText.text = "Score : " + score.ToString();
        _waitForPress = false;

        if (score == recipe.MaxScore)
        {
            DishImage.sprite = recipe.DishSprite;
            EndText.text = "A perfect Recipe ! Good job !";
        }
        else
        {
            DishImage.sprite = recipe.MissedSprite;
            EndText.text = "Oh no you didn't respect the recipe !";
        }
    }
}
