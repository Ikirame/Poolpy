using UnityEngine;
using UnityEngine.UI;

public class SwitchControllerImage : MonoBehaviour
{
    public Sprite KeyboardSprite;

    public Sprite ControllerSprite;

    private Image _image;

    // Use this for initialization
    private void Start()
    {
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Get Joystick Names
        var joystickNames = Input.GetJoystickNames();

        //Iterate over every element
        foreach (var joystickName in joystickNames)
        {
            _image.sprite = !string.IsNullOrEmpty(joystickName) ? ControllerSprite : KeyboardSprite;
        }
    }
}