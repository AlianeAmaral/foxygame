using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeToOtherScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    
}
