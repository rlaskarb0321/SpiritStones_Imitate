using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroDmgText : MonoBehaviour
{
    Text _thisTxt;

    void Start()
    {
        _thisTxt = GetComponent<Text>();
        UpdateText(0.0f);
    }

    public void UpdateText(float damage)
    {
        if (damage == 0.0f)
        {
            _thisTxt.text = "";
            return;
        }

        _thisTxt.text = damage.ToString();
    }
}
