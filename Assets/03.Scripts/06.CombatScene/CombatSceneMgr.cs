using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatSceneMgr : MonoBehaviour, IGameFlow
{
    [Tooltip("아래의 두 Collection의 크기는 Max Level Value와 맞춰야 함")]
    public List<GameObject> _monsterFormationByStage; // 인덱스값에 맞춰 몬스터들 그룹을 넣어주자
    [HideInInspector]public StageMgr _stageMgr;

    [Header("=== target ===")]
    public GameObject _heroGroup;

    private void Awake()
    {
        _stageMgr = GetComponent<StageMgr>();
    }

    public void DoGameFlowAction()
    {
        DoEnemyAction();
        #region 23/02/15 스테이지 이동코드를 StageMgr로 이동
        /*
        if (GameManager._instance._gameFlow == eGameFlow.StageClear)
        {
            GameManager._instance._gameFlow = eGameFlow.InProgress;

            _isStageClear[_currLevel - 1] = true;
            _monsterFormationByStage[_currLevel - 1].SetActive(false);

            // 여기에 보스가 죽으면 해야 할 일을 작성
            _currLevel++;
            if (_currLevel <= _monsterFormationByStage.Count)
            {
                _monsterFormationByStage[_currLevel - 1].SetActive(true);
            }
            else
            {
                _isBossStageClear = true;
            }

            GameManager._instance._gameFlow = eGameFlow.Idle;
            return;
        }
        */
        #endregion
    }

    void DoEnemyAction()
    {
        GameManager._instance._gameFlow = eGameFlow.InProgress;
        GameObject currLevelMonsterFormation = _monsterFormationByStage[_stageMgr._currLevel - 1];
        for (int i = 0; i < currLevelMonsterFormation.transform.childCount; i++)
        {
            Transform monsterPos = currLevelMonsterFormation.transform.GetChild(i); // 스테이지별 몬스터 그룹속 몬스터의 위치
            EnemyBase monster = monsterPos.transform.GetChild(0).GetComponent<EnemyBase>(); // 그 하위에있는 몬스터들

            // 안 죽은 몬스터들 상대로 일 시킴
            if (monster._state != EnemyBase.eState.Die)
                monster.DoMonsterAction(_heroGroup);
        }

        StartCoroutine(CheckAttackEnd(currLevelMonsterFormation));
    }

    IEnumerator CheckAttackEnd(GameObject currMonsterForm)
    {
        MonsterFormation monsterFormation = currMonsterForm.GetComponent<MonsterFormation>();
        yield return new WaitUntil(() => monsterFormation._endTurnMonsterCount == monsterFormation._monsterCount.Count);

        GameManager._instance._gameFlow = eGameFlow.BackToIdle;
        monsterFormation._endTurnMonsterCount = 0;
    }

    public void GoToNextStage()
    {
        _stageMgr._isStageClear[_stageMgr._currLevel - 1] = true;
        _monsterFormationByStage[_stageMgr._currLevel - 1].SetActive(false);
        _stageMgr._currLevel++;

        if (_stageMgr._currLevel <= _monsterFormationByStage.Count)
        {
            _monsterFormationByStage[_stageMgr._currLevel - 1].SetActive(true);
        }
        else
        {
            _stageMgr._isBossStageClear = true;
        }
    }
}
