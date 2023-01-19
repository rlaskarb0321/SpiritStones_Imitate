using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFormation : MonoBehaviour
{
    public int _dieCount;
    public List<GameObject> _monsterCount;

    private void OnEnable()
    {
        _dieCount = 0;
    }

    public void UpdateDieCount()
    {
        _dieCount++;

        if (_dieCount == _monsterCount.Count)
        {
            Debug.Log("Stage Clear");

            CombatSceneMgr combatMgr = transform.parent.GetComponent<CombatSceneMgr>();
            combatMgr.GoToNextStage();
        }
    }
}
