using UnityEngine;


public class PauseMenu : MonoBehaviour
{
    // Pause panel game object
    public GameObject PausePanel;

    // Inventory panel game object
    public GameObject InventoryPanel;

    //Options Panel game object
    public GameObject OptionsPanel;

    // Defines if the game is paused or not
    private bool _isPaused;

    // Defines if the inventory and options panel is displayed or not
    private bool _inventoryDisplayed = false;
    private bool _optionsDisplayed = false;

    private void Start()
    {
        // Sets the pause panel to enable
        PausePanel.SetActive(false);

        // Sets the inventory panel to disable
        InventoryPanel.SetActive(false);

        //Sets the options panel to inactive
        OptionsPanel.SetActive(false);
    }

    private void Update()
    {
        // Checks if the button to make pause is pressed and the inventory is not displayed
        if (Input.GetButtonDown("Pause") && !_inventoryDisplayed)
        {
            if (_optionsDisplayed)
            {
                HideOptions();
                return;
            }
            // Do pause or disable pause
            if (!_isPaused)
                DoPause();
            else
                UnPause();
        }

        // Checks if the inventory button is pressed and the pause menu is not enabled
        if (!Input.GetButtonDown("Inventory") || _isPaused)
            return;

        // Shows or hides the inventory
        if (!_inventoryDisplayed)
            ShowInventory();
        else
            HideInventory();
    }

    private void DoPause()
    {
        // Do pause
        _isPaused = true;
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }

    private void UnPause()
    {
        // Disable pause
        _isPaused = false;
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }

    private void ShowInventory()
    {
        // Shows inventory
        _inventoryDisplayed = true;
        InventoryPanel.SetActive(true);
    }

    private void HideInventory()
    {
        // Hides inventory
        _inventoryDisplayed = false;
        InventoryPanel.SetActive(false);
    }

    public void ShowOptions()
    {
        // Shows options panel
        _optionsDisplayed = true;
        OptionsPanel.SetActive(true);
    }

    public void HideOptions()
    {
        // Hides option panel
        _optionsDisplayed = false;
        OptionsPanel.SetActive(false);
    }
}