using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleOption : MonoBehaviour
{
    public GameObject OptionUI;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToOption()
    {
        AudioManager.instance.PlaySFX("ButtonClick");

        OptionUI.SetActive(true);
    }
    public void GoToTitle()
    {
        AudioManager.instance.PlaySFX("ButtonClick");

        OptionUI.SetActive(false);
    }
}
