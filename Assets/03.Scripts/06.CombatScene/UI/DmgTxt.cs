using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DmgTxt : MonoBehaviour
{
    private Text _txt;

    private void Start()
    {
        _txt = GetComponent<Text>();
    }

    void Update()
    {
        if (_txt.color.a <= 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
