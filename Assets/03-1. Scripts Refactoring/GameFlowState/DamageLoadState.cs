using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageLoadState : GameFlowState
{
    // ºí·° ÆÄ±« & ¿µ¿õ µ¥¹ÌÁö ½×±â ±¸Çö

    public override IEnumerator Handle()
    {
        yield return null;

        GameFlowMgr_Refact._instance.ChangeGameFlow(_nextGameFlow);
    }
}
