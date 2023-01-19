using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatSceneMgr : MonoBehaviour, IGameFlow
{
    [Header("=== Stage ===")]
    public int _maxLevelValue;
    public int _currLevel;
    public float _obstacleBlockPercentage;
    public float _itemBlockPercentage;
    public Image _stageBackGroundImage;

    [Tooltip("아래의 두 Collection의 크기는 Max Level Value와 맞춰야 함")]
    public List<GameObject> _monsterFormationByStage; // 인덱스값에 맞춰 몬스터들 그룹을 넣어주자
    public List<bool> _isStageClear;

    [Header("=== target ===")]
    public GameObject _heroGroup;

    public void DoGameFlowAction()
    {
        DoEnemyAction();
    }

    void DoEnemyAction()
    {
        if (IsStageClear())
            return;

        GameObject currLevelMonsterFormation = _monsterFormationByStage[_currLevel - 1];
        for (int i = 0; i < currLevelMonsterFormation.transform.childCount; i++)
        {
            Transform monsterPos = currLevelMonsterFormation.transform.GetChild(i); // 스테이지별 몬스터 그룹속 몬스터의 위치
            EnemyBase monster = monsterPos.transform.GetChild(0).GetComponent<EnemyBase>(); // 그 하위에있는 몬스터들

            // 안 죽은 몬스터들 상대로 일 시킴
            if (monster._state != EnemyBase.eState.Die)
                monster.DoMonsterAction(_heroGroup);
        }

        GameManager._instance._gameFlow = eGameFlow.BackToIdle;
    }

    public void GoToNextStage()
    {
        GameManager._instance._gameFlow = eGameFlow.StageClear;
    }

    public bool IsStageClear()
    {
        if (GameManager._instance._gameFlow != eGameFlow.StageClear)
        {
            Debug.Log("클리어 하지 않음");
            GameManager._instance._gameFlow = eGameFlow.EnemyTurn;
            return false;
        }
        else
        {
            _isStageClear[_currLevel - 1] = true;

            _monsterFormationByStage[_currLevel - 1].SetActive(false);
            _currLevel++;
            _monsterFormationByStage[_currLevel - 1].SetActive(true);

            Debug.Log("클리어 했다구여");
            return true;
        }
    }
}
