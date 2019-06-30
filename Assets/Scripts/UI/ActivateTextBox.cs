using UnityEngine;

public class ActivateTextBox : MonoBehaviour
{
    // Text asset component
    public TextAsset Text;

    // Start line
    public int StartLine;

    // End line
    public int EndLine;

    // Text box manager component
    public TextBoxManager TextBoxManager;

    // Defines if the text box is destroyed when activated
    public bool DestroyWhenActivated;

    // Defines if the button press is required
    public bool RequireButtonPress;

    // Defines if the text box waiting for press
    private bool _waitForPress;

    private void Update()
    {
        // Checks if the text box can be activated
        if (!_waitForPress || !Input.GetButtonDown("Action"))
            return;

        // Enables text box
        TextBoxManager.ReloadScript(Text, StartLine, EndLine);
        TextBoxManager.EnableTextBox();
        _waitForPress = false;
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        // Checks if the collider is valid
        if (!hit)
            return;

        // Checks if the collider is the player
        if (!hit.CompareTag("Player"))
            return;

        // Checks if the player needs to press a button
        if (RequireButtonPress)
        {
            _waitForPress = true;
            return;
        }

        // Enables text box
        TextBoxManager.ReloadScript(Text, StartLine, EndLine);
        TextBoxManager.EnableTextBox();

        // Destroys the object
        if (DestroyWhenActivated)
            Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D hit)
    {
        // Checks if the collider is valid
        if (!hit)
            return;

        // Checks if the collider is the player
        if (hit.CompareTag("Player"))
            _waitForPress = false;
    }
}