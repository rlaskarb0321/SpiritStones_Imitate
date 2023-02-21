using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageMgr : MonoBehaviour, IGameFlow
{
    [Header("=== Stage ===")]
    public int _maxLevelValue;
    public int _currLevel;
    public float _itemBlockPercentage;
    public bool _isBossStageClear;
    public List<bool> _isStageClear;

    private CombatUI _ui;
    private CombatSceneMgr _combatMgr;

    private void Awake()
    {
        _ui = GetComponent<CombatUI>();
        _combatMgr = GetComponent<CombatSceneMgr>();

        _ui.SetRountTxt(_currLevel, _maxLevelValue);
    }

    public void DoGameFlowAction()
    {
        GameManager._instance._gameFlow = eGameFlow.InStageClear;
        StartCoroutine(DoStageClearAction());
    }

    IEnumerator DoStageClearAction()
    {
        HeroTeamMgr heroTeamMgr = _combatMgr._heroGroup.GetComponent<HeroTeamMgr>();
        heroTeamMgr.LooseHeroDmg();

        while (GameManager._instance._gameFlow == eGameFlow.InStageClear)
        {
            yield return StartCoroutine(_ui.StartFadeOut());
            _combatMgr.GoToNextStage();
            _ui.SetRountTxt(_currLevel, _maxLevelValue);
            StartCoroutine(_ui.ShowTxtFade());

            yield return _ui._ws;
            yield return StartCoroutine(_ui.StartFadeIn());

            GameManager._instance._gameFlow = eGameFlow.Idle;
            yield return null;
        }
    }
}
