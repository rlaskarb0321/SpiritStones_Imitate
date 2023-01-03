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
        Debug.Log("NormalBlock ÆÄ±«Sound, Animation Àç»ý");
        GenerateSpirit(_spiritPrefabs);

        base.RemoveFromMemoryList();
        Destroy(gameObject);
    }

    void GenerateSpirit(GameObject spirit)
    {
        Transform spiritGroup = GameObject.Find("Spirit Generator").transform;
        Instantiate(spirit, transform.position, Quaternion.identity, spiritGroup);
    }
}
