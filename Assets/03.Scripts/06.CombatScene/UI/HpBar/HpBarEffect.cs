using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarEffect : MonoBehaviour
{
    public Image _redHpFill;

    public IEnumerator MatchRedHpFill(float currHp, float maxHp, float speed)
    {
        float hpPercentage = currHp / maxHp;

        while (Mathf.Abs(_redHpFill.fillAmount - hpPercentage) >= 0.01f)
        {
            _redHpFill.fillAmount -= speed * Time.deltaTime;
            yield return null;
        }

        _redHpFill.fillAmount = hpPercentage;
    }

    public void FillRedHPFill(float fillamount)
    {
        _redHpFill.fillAmount = fillamount;
    }
}
