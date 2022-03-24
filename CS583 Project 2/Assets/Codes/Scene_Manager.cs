using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_Manager : MonoBehaviour
{
    public void LoadSceneByNumber(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }

    public void quitGame(int ignoreLevel)
    {
        Debug.Log("quit called");

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
