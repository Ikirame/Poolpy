using UnityEngine;
using UnityEngine.EventSystems;

public class SwitchCanvas : MonoBehaviour
{
    public GameObject OffCanvas;
    public GameObject OnCanvas;
    public GameObject FirstButton;

    public void Switch()
    {
        OffCanvas.SetActive(true);
        OnCanvas.SetActive(false);

        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(FirstButton, null);
    }
}