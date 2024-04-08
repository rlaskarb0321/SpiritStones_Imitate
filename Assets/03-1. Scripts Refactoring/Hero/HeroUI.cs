using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroUI : MonoBehaviour
{
    [SerializeField] private Text _storedDamageTxt;

    public void SetDamageText(float value)
    {
        _storedDamageTxt.text = value.ToString();
    }
}