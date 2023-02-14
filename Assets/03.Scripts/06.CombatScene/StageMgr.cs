using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageMgr : MonoBehaviour, IGameFlow
{
    public Image _stageBackGroundImg;
    public Image _moveStagePanel;
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
        GameManager._instance._gameFlow = eGameFlow.InStageClear;
        StartCoroutine(DoStageClearAction());
    }

    IEnumerator DoStageClearAction()
    {
        _stageAlarmTxt.text = $"STAGE 1\n{_combatMgr._currLevel} / {_combatMgr._maxLevelValue}";
        HeroTeamMgr heroTeamMgr = _combatMgr._heroGroup.GetComponent<HeroTeamMgr>();
        heroTeamMgr.LooseHeroDmg();

        while (GameManager._instance._gameFlow == eGameFlow.InStageClear)
        {
            // ui들은 나중에 새로운 스크립트로 분리
            // 패널로 Fade in&out 효과를 주고 스테이지 이동때 필요한 작업을 함

            /* 패널
             * 패널은 평상시엔 activeSelf = false이고, InStageClear일때 true로 놓음, 아이템블럭과 유사한 방식으로 애님재생
             * 패널로 Fade in&out 효과를 줄 때 stage 알림 ui 애니메이션도 같이재생, 가장 어두운 시점에 event를 달아놓는다
             * 해당 event때 스테이지 이동 코드작업
             */
            yield return null;
        }
    }
}
