using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalBlock_Refact : BlockBase_Refact
{
    [Header("�븻 ���")]
    [SerializeField] private Spirit_Refact _spirit;
    [SerializeField] private Sprite[] _selectionImg;

    private Image _thisImg;

    private void Awake()
    {
        _thisImg = GetComponent<Image>();
    }

    public override void ActivateBlockSeletionUI(bool turnOn)
    {
        _selectionParticle.SetActive(turnOn);
        if (turnOn)
        {
            _thisImg.sprite = _selectionImg[(int)BlockSelectionImg.Select];
        }
        else
        {
            _thisImg.sprite = _selectionImg[(int)BlockSelectionImg.UnSelect];
        }
    }

    public override void DoBreakAction()
    {
        GenerateSpirit();
        Destroy(gameObject);
    }

    private void GenerateSpirit()
    {
        Transform spiritGroup = GameObject.Find("Spirit Generator").transform;
        Spirit_Refact spirit = Instantiate(_spirit, transform.position, Quaternion.identity, spiritGroup);
    }
}
