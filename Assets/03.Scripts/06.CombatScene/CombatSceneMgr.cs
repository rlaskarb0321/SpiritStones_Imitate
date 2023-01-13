using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatSceneMgr : MonoBehaviour
{
    public int _maxLevelValue;
    public int _currLevel;
    public float _obstacleBlockPercentage;
    public float _itemBlockPercentage;
    public Image _stageBackGroundImage;
    [Tooltip("해당 Collection의 크기는 Max Level Value와 맞춰야 함")]
    public List<GameObject> _monsterFormationByStage; // 인덱스값에 맞춰 몬스터들 그룹을 넣어주자

    private void Start()
    {
        
    }

    IEnumerator DoEnemyAction()
    {
        yield return new WaitUntil(() => GameManager._instance._gameFlowQueue.Peek() == eGameFlow.EnemyTurn);
    }
}
