using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBlock : BlockBase
{
    [SerializeField] private eNormalBlockType _normalType;
    public GameObject _spiritPrefabs;

    public override eNormalBlockType NormalType { get { return _normalType; } }
    public override float MovSpeed { get { return _movSpeed; } set { _movSpeed = value; } }
    public override bool IsDocked { get { return _isDocked; } set { _isDocked = value; } }

    void Start()
    {
        base.AddToMemoryList();
    }

    public override void DoAction()
    {
        // Debug.Log("NormalBlock ÆÄ±«Sound, Animation Àç»ý");
        GenerateSpirit(_spiritPrefabs);

        base.RemoveFromMemoryList();
        Destroy(gameObject);
    }

    void GenerateSpirit(GameObject spiritPrefabs)
    {
        Transform spiritGroup = GameObject.Find("Spirit Generator").transform;
        HeroTeamMgr heroMgr = GameObject.Find("TeamPositionGroup").GetComponent<HeroTeamMgr>();

        for (int i = 0; i < heroMgr._heroes[(int)_normalType].Count; i++)
        {
            GameObject spiritObj = Instantiate(spiritPrefabs, transform.position, Quaternion.identity, spiritGroup)
                as GameObject;
            Spirit spirit = spiritObj.GetComponent<Spirit>();
            spirit.SetTarget(heroMgr._heroes[(int)_normalType][i]);
        }
    }
}
