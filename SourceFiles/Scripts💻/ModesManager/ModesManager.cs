using UnityEngine;
using UnityEngine.SceneManagement;


public class ModesManager : MonoBehaviour
{

    public void SetInteractiveMode()
    {
        Debug.Log("Interactive Mode selected.");
        SceneManager.LoadScene("FinalProjectDEPIInteractive");
    }

    public void SetTourGuideMode()
    {
        Debug.Log("Tour Guide Mode selected.");
        SceneManager.LoadScene("FinalProjectDEPITourGuided");
    }

    public void SetPianoPlayMode()
    {
        Debug.Log("🎹🎹Piano Play Mode selected.");
        SceneManager.LoadScene("PianoScene");
    }

    public void GoToMainMenu()
    {
        Debug.Log("Returning to Main Menu.");
        SceneManager.LoadScene("UIFinal");
    }

    
}
