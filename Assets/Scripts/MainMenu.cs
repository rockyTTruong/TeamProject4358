using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {   
        SceneManager.LoadScene("PersistentScene");
        SceneManager.LoadScene("StartingZone", LoadSceneMode.Additive);
    }
    public void LoadGamge()
    {
        SceneManager.LoadScene("PersistentScene");
        SceneManager.LoadScene("StartingZone", LoadSceneMode.Additive);
    }
    public void Option()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void exitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
}
