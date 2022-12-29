using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPopUpUI : MonoBehaviour
{
    [Header("=== Mail PopUp ===")]
    public Button _mailPanel;
    public Button _mailExit;
    public Button _getAll;

    [Header("=== HeroInfo PopUp")]
    public Button _skill_1;
    public Button _skill_2;
    public Button _substitute;
    public Button _cancle;

    void Start()
    {
        if (_mailPanel != null)
        {
            _mailPanel.onClick.AddListener(OnClickMailPanel);
        }
        if (_mailExit != null)
        {
            _mailExit.onClick.AddListener(OnClickMailExit);
        }
        if (_getAll != null)
        {
            _getAll.onClick.AddListener(OnClickGetAll);
        }
    }

    void OnClickMailPanel()
    {
        GameObject.Find(_mailPanel.name).SetActive(false);
    }

    void OnClickMailExit()
    {
        GameObject.Find(_mailPanel.name).SetActive(false);
    }

    void OnClickGetAll()
    {

    }
}
