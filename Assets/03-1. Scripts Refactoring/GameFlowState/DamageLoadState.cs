using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageLoadState : GameFlowState
{
    // �� �ı� & ���� ������ �ױ� ����

    public override IEnumerator Handle()
    {
        yield return null;

        GameFlowMgr_Refact._instance.ChangeGameFlow(_nextGameFlow);
    }
}
