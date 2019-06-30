using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeTrigger : MonoBehaviour {

    //the Recipe Panel
    public GameObject _recipePanel;

    private bool _panelActivated;

	// Use this for initialization
	void Start () {

        _recipePanel.SetActive(false);
        _panelActivated = false;
	}

    private void Update()
    {
        if (Input.GetButtonDown("Action") && _panelActivated)
        {
            Time.timeScale = 1;
            _panelActivated = false;
            _recipePanel.SetActive(false);
            Destroy(gameObject);
            
            // Activates player after the text box was active
            FindObjectOfType<Player>().GetComponent<PlayerController2D>().ActivatePlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        // Checks if the collider is valid
        if (!hit)
            return;

        // Checks if the collider is the player
        if (!hit.CompareTag("Player"))
            return;

        // Deactivates player during text box was active
        FindObjectOfType<Player>().GetComponent<PlayerController2D>().DeactivatePlayer();

        Time.timeScale = 0;
        _recipePanel.SetActive(true);
        _panelActivated = true;
    }
}
