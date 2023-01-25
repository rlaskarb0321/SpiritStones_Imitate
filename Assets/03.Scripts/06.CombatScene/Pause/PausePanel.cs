using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    public Image _finalConfirmUI;
    public Image _blueBackGround;

    #region PausePanel Method
    public void OnClickResumeBtn()
    {
        if (Time.timeScale == 0.0f)
        {
            Time.timeScale = 1.0f;
            this.gameObject.SetActive(false);
            return;
        }
    }

    public void OnClickSettingBtn()
    {
        Debug.Log("세팅 버튼 클릭");
    }

    // 나가기 버튼 클릭시 최종확인을 위한 팝업창 등장
    public void OnClickExitBtn()
    {
        _finalConfirmUI.gameObject.SetActive(true);
        _blueBackGround.gameObject.SetActive(false);

        Color color = this.gameObject.GetComponent<Image>().color;
        color.a = 0.9f;
        this.gameObject.GetComponent<Image>().color = color;
    }
    #endregion PausePanel Method

    #region FinalConfirm Panel Method
    public void OnClickYesBtn()
    {
        // Debug.Log("나가버리기");
        if (Time.timeScale == 0.0f)
            Time.timeScale = 1.0f;

        LoadingSceneManager.LoadScene("MainScene");
    }

    public void OnClickNoBtn()
    {
        if (Time.timeScale == 0.0f)
        {
            Time.timeScale = 1.0f;

            _blueBackGround.gameObject.SetActive(true);
            _finalConfirmUI.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
            return;
        }
    }
    #endregion FinalConfirm Panel Method
}
