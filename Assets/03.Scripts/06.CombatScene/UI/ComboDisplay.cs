using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboDisplay : MonoBehaviour
{
    public Text _txt;
    private WaitForSeconds _ws;

    private void Start()
    {
        _ws = new WaitForSeconds(0.3f);
        StartCoroutine(UpdateText());
    }

    IEnumerator UpdateText()
    {
        yield return new WaitUntil(() => GameManager._instance._playerComboCount > 0);

        while (true)
        {
            switch (GameManager._instance._gameFlow)
            {
                case eGameFlow.InProgress:
                    if (GameManager._instance._playerComboCount > 0)
                        _txt.text = $"{GameManager._instance._playerComboCount} Combo!!";
                    yield return _ws;
                    break;

                default:
                    _txt.text = "";
                    yield return _ws;
                    break;
            }

            yield return null;
        }
    }
}
