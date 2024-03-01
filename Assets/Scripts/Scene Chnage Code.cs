using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChnageCode : MonoBehaviour
{
    // Start is called before the first frame update
    public void restartgame()
    {
        SceneManager.LoadScene(0);
    }

    public void quitgame()
    {
        Application.Quit();
    }

    public void Returntomenu()
    {
        Debug.Log("click");
        SceneManager.LoadScene("StartMenu");
    }
}
