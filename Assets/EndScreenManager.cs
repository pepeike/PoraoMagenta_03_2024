using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    public void replaygame()
    {
        SceneManager.LoadScene("IntroCutscene");
    }

    public void QuitingGame()
    {
        Application.Quit();    
    }
    

}