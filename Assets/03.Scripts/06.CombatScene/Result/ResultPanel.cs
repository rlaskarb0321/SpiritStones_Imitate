using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    public void OnClickReStartBtn()
    {
        LoadingSceneManager.LoadScene("CombatScene");
    }

    public void OnClickExitBtn()
    {
        LoadingSceneManager.LoadScene("MainScene");
    }
}
