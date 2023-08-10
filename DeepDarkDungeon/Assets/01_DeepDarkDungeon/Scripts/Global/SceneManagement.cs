using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagement : MonoBehaviour
{

    public void ChangeGameStartScene()
    {
        SceneManager.LoadScene("DungeonScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
