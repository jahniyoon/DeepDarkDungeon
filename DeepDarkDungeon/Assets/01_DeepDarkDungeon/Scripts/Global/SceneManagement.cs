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

    // 디버그용 테스트 던전
    public void ChangeGameTestDungeonScene()
    {
        SceneManager.LoadScene("TestDungeonScene");
    }
    void ResetData()
    {
        GameData.loadEnable = false;    // 세이브 로드 못하게 설정
        GameData.floor = 0;
    }
}
