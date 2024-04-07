using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalBlock_Refact : BlockBase_Refact
{
    [Header("노말 블록")]
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

    public override void DoBreakAction(HeroTeamMgr_Refact heroTeam)
    {
        GenerateSpirit(heroTeam.TeamHeroDict);
        Destroy(gameObject);
    }

    private void GenerateSpirit(Dictionary<eBlockHeroType_Refact, List<HeroBase_Refact>> heroTeamDict)
    {
        Transform spiritGroup = GameObject.Find("Spirit Generator").transform;
        for (int i = 0; i < heroTeamDict[_eBlockHeroType].Count; i++)
        {
            Spirit_Refact spirit = Instantiate(_spirit, transform.position, Quaternion.identity, spiritGroup);
            spirit.SetTarget(heroTeamDict[_eBlockHeroType][i]);
        }
    }
}
