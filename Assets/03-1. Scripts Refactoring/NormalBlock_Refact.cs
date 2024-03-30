using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBlock_Refact : BlockBase_Refact, IBreakableBlock
{
    [SerializeField] private GameObject _spirit;
    [SerializeField] private eNormalBlockType_Refact _eNormalBlockType;

    public eNormalBlockType_Refact NormalBlockType { get { return _eNormalBlockType; } }

    public void DoBlockBreak()
    {
        GenerateSpirit();
        Destroy(gameObject);
    }

    private void GenerateSpirit()
    {
        Transform spiritGroup = GameObject.Find("Spirit Generator").transform;
    }
}
