using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagement : MonoBehaviour
{
    public static SceneManagement instance;

    public void ChangeGameStartScene()
    {
        SceneManager.LoadScene("DungeonScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ChangeGameTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    // ����׿� �׽�Ʈ ����
    public void ChangeGameTestDungeonScene()
    {
        SceneManager.LoadScene("TestDungeonScene");
    }
}
