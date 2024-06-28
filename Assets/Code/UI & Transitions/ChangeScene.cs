using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
   public void SceneEnd()
    {
        SceneManager.LoadScene("IntroCutscene");
    }
}
