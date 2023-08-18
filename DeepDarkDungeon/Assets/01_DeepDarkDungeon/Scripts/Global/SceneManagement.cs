using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagement : MonoBehaviour
{
    public static SceneManagement instance;

    public void ChangeDungeonScene()
    {
        SceneManager.LoadScene("DungeonScene");
    }
    public void ChangeReplayDungeonScene()
    {
        ResetData();
        SceneManager.LoadScene("DungeonScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ChangeGameTitleScene()
    {
        ResetData();
        SceneManager.LoadScene("TitleScene");
    }

    // ����׿� �׽�Ʈ ����
    public void ChangeGameTestDungeonScene()
    {
        SceneManager.LoadScene("TestDungeonScene");
    }
    void ResetData()
    {
        GameData.loadEnable = false;    // ���̺� �ε� ���ϰ� ����
        GameData.floor = 0;
    }
}
