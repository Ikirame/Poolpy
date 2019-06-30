using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour
{
    // Text asset component
    public TextAsset TextFile;

    // Text component
    public Text Text;

    // Text lines
    public string[] TextLines;

    // Index of the current line
    public int CurrentLine;

    // Index of the end of the line
    public int EndAtLine;

    // Defines if the text box manager is active
    public bool IsActive;

    // Defines if the game is paused or not
    public bool PauseGame;
    
    private void Start()
    {
        // Splits the between after each end of line
        if (TextFile != null)
            TextLines = TextFile.text.Split('\n');

        // Checks if the end of line is at the index 0
        if (EndAtLine == 0)
            DisableTextBox();

        // Enables of disables the text box
        if (IsActive)
            EnableTextBox();
        else
            DisableTextBox();
    }
    
    private void Update()
    {
        // Checks if the text box is active
        if (!IsActive)
            return;

        // Updates the current text
        Text.text = TextLines[CurrentLine];

        // Changes the current line
        if (Input.GetButtonDown("Action"))
            CurrentLine++;

        // Disables the text box when the text is finished
        if (CurrentLine > EndAtLine)
            DisableTextBox();
    }

    public void EnableTextBox()
    {
        // Pauses the game
        if (PauseGame)
            Time.timeScale = 0;

        // Deactivates player during text box was active
        FindObjectOfType<Player>().GetComponent<PlayerController2D>().DeactivatePlayer();

        // Enables the text box
        gameObject.SetActive(true);
        IsActive = true;
    }

    public void DisableTextBox()
    {
        // Disables the pause
        if (PauseGame)
            Time.timeScale = 1;

        // Activates player after the text box was active
        FindObjectOfType<Player>().GetComponent<PlayerController2D>().ActivatePlayer();

        // Disables the text box
        gameObject.SetActive(false);
        IsActive = false;
    }

    public void ReloadScript(TextAsset newText, int startLine, int endLine)
    {
        // Reload script
        if (newText == null)
            return;

        CurrentLine = startLine;
        EndAtLine = endLine;
        TextLines = new string[1];
        TextLines = (newText.text.Split('\n'));
    }
}