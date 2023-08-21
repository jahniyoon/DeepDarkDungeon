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

    public void ChangeDungeonScene()    // ���������� �̵�
    {
        fadeAnimation.SetTrigger("FadeOut");
        sceneName = "DungeonScene";
        StartCoroutine("LoadScene");
    }
    public void ChangeReplayDungeonScene()  // ���� ����� (������ �ʱ�ȭ)
    {
        fadeAnimation.SetTrigger("FadeOut"); 
        sceneName = "DungeonScene";
        ResetData();
        StartCoroutine("LoadScene");
    }
    public void ExitGame()  // ���� ����
    {
        StartCoroutine("FadeOut");
        Application.Quit();
    }
    public void ChangeGameTitleScene()  // Ÿ��Ʋ ������ �̵�
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
        GameData.loadEnable = false;    // ���̺� �ε� ���ϰ� ����
        GameData.floor = 0;
    }
}
