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
    [Tooltip("해당 Collection의 크기는 Max Level Value와 맞춰야 함")]
    public List<GameObject> _monsterFormationByStage; // 인덱스값에 맞춰 몬스터들 그룹을 넣어주자

    [Header("=== target ===")]
    public GameObject _heroGroup;

    public void DoGameFlowAction()
    {
        // eGameState.EnemyTurn 일때
        DoEnemyAction();
    }

    void DoEnemyAction()
    {
        GameManager._instance._gameFlow = eGameFlow.InProgress;

        // 스테이지별 몬스터 그룹
        GameObject currLevelMonsterFormation = _monsterFormationByStage[_currLevel - 1];
        for (int i = 0; i < currLevelMonsterFormation.transform.childCount; i++)
        {
            Transform monsterPos = currLevelMonsterFormation.transform.GetChild(i); // 스테이지별 몬스터 그룹속 몬스터의 위치
            EnemyBase monster = monsterPos.transform.GetChild(0).GetComponent<EnemyBase>(); // 그 하위에있는 몬스터들

            if (monster._state != EnemyBase.eState.Die)
                monster.DoMonsterAction(_heroGroup); 
        }
        GameManager._instance._gameFlow = eGameFlow.EnemyTurn;
        GameManager._instance._gameFlow++;
    }
}
