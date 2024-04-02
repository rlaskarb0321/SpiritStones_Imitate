using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBlock_Refact : BlockBase_Refact
{
    [SerializeField] private GameObject _spirit;
    
    public override void DoBreakAction()
    {
        GenerateSpirit();
        Destroy(gameObject);
    }

    private void GenerateSpirit()
    {
        Transform spiritGroup = GameObject.Find("Spirit Generator").transform;
    }
}
