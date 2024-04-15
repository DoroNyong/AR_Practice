using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject settingPanel;

    [SerializeField]
    private Button weaponLeftBtn;

    [SerializeField]
    private Button weaponRightBtn;

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

    public void ActivateWeaponBtn()
    {
        weaponLeftBtn.interactable = true;
        weaponRightBtn.interactable = true;
    }
}
