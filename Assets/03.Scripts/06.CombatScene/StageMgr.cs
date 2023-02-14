using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageMgr : MonoBehaviour, IGameFlow
{
    public Image _stageBackGroundImg;
    public Text _stageAlarmTxt;
    private CombatSceneMgr _combatMgr;

    Animator _anim;
    private int _hashToUp = Animator.StringToHash("isTimeToUp");

    private void Awake()
    {
        _combatMgr = GetComponent<CombatSceneMgr>();
        _anim = _stageAlarmTxt.GetComponent<Animator>();
    }

    public void DoGameFlowAction()
    {
        _stageAlarmTxt.text = $"STAGE 1\n{_combatMgr._currLevel} / {_combatMgr._maxLevelValue}";

        HeroTeamMgr heroTeamMgr = _combatMgr._heroGroup.GetComponent<HeroTeamMgr>();
        heroTeamMgr.LooseHeroDmg();

        Debug.Log("Stage Clear!!");
    }
}
