using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFormation : MonoBehaviour
{
    public int _dieCount;
    public List<GameObject> _monsterCount;
    public bool[] _isFocusTargetOn;

    private void OnEnable()
    {
        _dieCount = 0;
    }

    public void UpdateDieCount()
    {
        _dieCount++;

        if (_dieCount == _monsterCount.Count)
        {
            CombatSceneMgr combatMgr = transform.parent.GetComponent<CombatSceneMgr>();
            combatMgr.GoToNextStage();
        }
    }

    public void UpdateFocusTargetInfo(GameObject monster)
    {
        for (int i = 0; i < _monsterCount.Count; i++)
        {
            EnemyUI enemyUI = _monsterCount[i].transform.GetChild(0).GetComponent<EnemyUI>();
            if (System.Object.ReferenceEquals(monster, _monsterCount[i]))
                enemyUI.CtrlFocusTargetActive();
            else
                if (enemyUI._focusTarget.activeSelf)
                    enemyUI.DisableFocusTarget();

            _isFocusTargetOn[i] = enemyUI._focusTarget.activeSelf;
        }
    }
}
