using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CombatSceneTopUI : MonoBehaviour
{
    public Image _pauseUIPanel;

    public void OnClickPauseBtn()
    {
        Time.timeScale = 0.0f;
        _pauseUIPanel.gameObject.SetActive(true);
    }
}
