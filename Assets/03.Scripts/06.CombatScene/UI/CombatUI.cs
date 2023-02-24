using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour
{
    public Image _stageBackGroundImg;
    public Text _stageAlarm;
    
    [Header("=== Fade In&Out ===")]
    public Image _fadeInOutPanel;
    public Text _stageAlarmTxt;
    public float _fadeValue;
    public float _delayValue;
    public WaitForSeconds _ws;

    public Image _resultUI;

    private void Start()
    {
        _ws = new WaitForSeconds(_delayValue);
    }

    // »≠∏È¿ª æÓµ”∞‘ «ÿ¡‹
    public IEnumerator StartFadeOut()
    {
        if (!_fadeInOutPanel.gameObject.activeSelf)
            _fadeInOutPanel.gameObject.SetActive(true);

        Color color = _fadeInOutPanel.color;
        while (color.a < 1.0f)
        {
            color.a += _fadeValue * Time.deltaTime;
            _fadeInOutPanel.color = color;
            yield return null;
        }
    }

    // »≠∏È¿ª π‡∞‘ «ÿ¡‹
    public IEnumerator StartFadeIn()
    {
        _stageAlarmTxt.gameObject.SetActive(false);

        Color color = _fadeInOutPanel.color;
        while (color.a > 0.0f)
        {
            color.a -= _fadeValue * Time.deltaTime;
            _fadeInOutPanel.color = color;
            yield return null;
        }
        _fadeInOutPanel.gameObject.SetActive(false);
    }

    public void SetRountTxt(int currRound, int maxRound)
    {
        if (!_stageAlarmTxt.gameObject.activeSelf)
            _stageAlarmTxt.gameObject.SetActive(true);

        if (currRound == maxRound)
        {
            _stageAlarmTxt.text = $"STAGE 1\n LAST ROUND";
            _stageAlarm.text = $"stage {currRound}/{maxRound}";
        }
        else
        {
            _stageAlarmTxt.text = $"STAGE 1\n {currRound}/{maxRound}";
            _stageAlarm.text = $"stage {currRound}/{maxRound}";
        }
    }

    public IEnumerator ShowTxtFade()
    {
        if (!_stageAlarmTxt.gameObject.activeSelf)
            _stageAlarmTxt.gameObject.SetActive(true);

        Color color = _stageAlarmTxt.color;
        while (color.a < 1.0f)
        {
            color.a += (2.0f * _fadeValue) * Time.deltaTime;
            _stageAlarmTxt.color = color;
            yield return null;
        }
    }
}
