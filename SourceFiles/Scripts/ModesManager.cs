using UnityEngine;

public class ModesManager : MonoBehaviour
{
    public GameObject player;
    public GameObject tourGuide;
    public GameObject uiCanvas;


    public void SetInteractiveMode()
    {
        if (player != null) player.SetActive(true);
        if (tourGuide != null) tourGuide.SetActive(false);
        if (uiCanvas != null) uiCanvas.SetActive(false);
    }

    public void SetTourGuideMode()
    {
        if (player != null) player.SetActive(false);
        if (tourGuide != null) tourGuide.SetActive(true);
        if (uiCanvas != null) uiCanvas.SetActive(false);
    }
}
