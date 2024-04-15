using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject settingPanel;

    public void SettingBtnON()
    {
        settingPanel.SetActive(true);
    }

    public void SettingBtnOFF()
    {
        settingPanel.SetActive(false);
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
