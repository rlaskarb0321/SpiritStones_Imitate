using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFormation : MonoBehaviour
{
    public int _dieCount;
    public List<GameObject> _monsterCount;
    public bool[] _isFocusTargetOn;
    public int _endTurnMonsterCount;

    private void OnEnable()
    {
        _dieCount = 0;
        _endTurnMonsterCount = 0;
        EnemyBase[] monsters = new EnemyBase[_monsterCount.Count];
        for (int i = 0; i < monsters.Length; i++)
        {
            monsters[i] = _monsterCount[i].transform.GetChild(0).GetComponent<EnemyBase>();
        }

        StartCoroutine(CheckMonsterTurn(monsters));
    }

    public void UpdateDieCount()
    {
        _dieCount++;
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

    IEnumerator CheckMonsterTurn(EnemyBase[] monsters)
    {
        while (gameObject.activeSelf)
        {
            if (_dieCount == _monsterCount.Count)
            {
                GameManager._instance._gameFlow = eGameFlow.StageClear;
                yield break;
            }

            for (int i = 0; i < monsters.Length; i++)
            {
                switch (monsters[i]._state)
                {
                    case EnemyBase.eState.EndTurn:
                        _endTurnMonsterCount++;
                        monsters[i]._state = EnemyBase.eState.Alive;
                        break;

                    case EnemyBase.eState.Die:
                        _endTurnMonsterCount = _dieCount;
                        break;
                }
            }

            yield return null;
        }
    }
}
