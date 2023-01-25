using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : EnemyBase
{
    public Text _alertText;
    public Image _alertPanel;

    private void OnEnable()
    {
        StartCoroutine(EndGame());
    }

    IEnumerator EndGame()
    {
        _alertText.gameObject.SetActive(true);
        _alertPanel.gameObject.SetActive(true);

        yield return new WaitForSeconds(3.0f);
    }
}
