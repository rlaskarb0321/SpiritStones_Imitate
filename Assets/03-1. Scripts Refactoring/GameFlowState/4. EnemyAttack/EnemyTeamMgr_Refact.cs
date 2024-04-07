using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTeamMgr_Refact : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyTeam;

    private Button[] _enemyButton;

    private void Awake()
    {
        for (int i = 0; i < _enemyTeam.Length; i++)
        {
            _enemyButton[i] = _enemyTeam[i].GetComponent<Button>();
            //_enemyButton[i].onClick.AddListener()
        }
    }
}
