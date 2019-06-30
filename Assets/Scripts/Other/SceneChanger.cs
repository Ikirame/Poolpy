using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Audio source component
    public Animator AudioSourceAnimator;

    // Defines if the audio is fading
    public bool IsFadingAudio;

    // Defines if fading
    public bool IsFading;

    // Scene to launch after fading
    private string _sceneToLaunch;

    // Animator needed to launch fading animation
    private Animator _animator;

    private void Start()
    {
        // Gets animator component
        _animator = GetComponent<Animator>();
    }

    public void FadeToScene(string sceneToLaunch)
    {
        // Launching animation to fade
        _sceneToLaunch = sceneToLaunch;

        if (IsFading)
            _animator.SetTrigger("FadeOut");

        if (IsFadingAudio)
            AudioSourceAnimator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        // Loads new scene
        SceneManager.LoadScene(_sceneToLaunch);
    }
}