 using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    public void MENU_ACTION_GotoScene(string sceneName)
    {
        // Loads a scene
        SceneManager.LoadScene(sceneName);
    }

    public void MENU_ACTION_ReloadLevel()
    {
        // Reloads a scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void MENU_ACTION_ReloadLevel(string levelName)
    {
        // Reloads a scene by name
        SceneManager.LoadScene(levelName);
        Time.timeScale = 1;
    }

    public void MENU_ACTION_Quit()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    
}