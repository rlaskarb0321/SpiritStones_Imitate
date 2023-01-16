using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatSceneMgr : MonoBehaviour, IGameFlow
{
    public int _maxLevelValue;
    public int _currLevel;
    public float _obstacleBlockPercentage;
    public float _itemBlockPercentage;
    public Image _stageBackGroundImage;
    [Tooltip("해당 Collection의 크기는 Max Level Value와 맞춰야 함")]
    public List<GameObject> _monsterFormationByStage; // 인덱스값에 맞춰 몬스터들 그룹을 넣어주자

    public void DoGameFlowAction()
    {
        // eGameState.EnemyTurn 일때
        DoEnemyAction();
    }

    void DoEnemyAction()
    {
        Debug.Log("EnemyTurn");
    }
}
