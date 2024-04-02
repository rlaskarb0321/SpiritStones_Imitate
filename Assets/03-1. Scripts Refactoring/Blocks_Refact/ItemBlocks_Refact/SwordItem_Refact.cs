using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordItem_Refact : BlockBase_Refact
{
    public override void ActivateBlockSeletionUI(bool turnOn)
    {
        _selectionParticle.SetActive(turnOn);
    }

    public override void DoBreakAction()
    {

    }
}
