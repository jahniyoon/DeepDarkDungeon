using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagement : MonoBehaviour
{
    public static SceneManagement instance;
    public Animator fadeAnimation;
    string sceneName;

    public void Start()
    {
        Animator fadeAnimation = GetComponent<Animator>();

    }

    public void ChangeDungeonScene()    // 던전씬으로 이동
    {
        fadeAnimation.SetTrigger("FadeOut");
        sceneName = "DungeonScene";
        StartCoroutine("LoadScene");
    }
    public void ChangeReplayDungeonScene()  // 던전 재시작 (데이터 초기화)
    {
        fadeAnimation.SetTrigger("FadeOut"); 
        sceneName = "DungeonScene";
        ResetData();
        StartCoroutine("LoadScene");
    }
    public void ExitGame()  // 게임 종료
    {
        StartCoroutine("FadeOut");
        Application.Quit();
    }
    public void ChangeGameTitleScene()  // 타이틀 씬으로 이동
    {
        Time.timeScale = 1;
        fadeAnimation.SetTrigger("FadeOut");
        ResetData();
        sceneName = "TitleScene";
        StartCoroutine("LoadScene");

    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);

    }
    void ResetData()
    {
        GameData.loadEnable = false;    // 세이브 로드 못하게 설정
        GameData.floor = 0;
    }
}
