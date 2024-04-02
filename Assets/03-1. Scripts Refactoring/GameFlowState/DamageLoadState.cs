using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageLoadState : GameFlowState
{
    [SerializeField] private float _gameFlowDelay;
    private Stack<BlockBase_Refact> _destroyStack;
    private WaitUntil _waitDocked;
    private WaitForSeconds _delayWait;

    private void Awake()
    {
        _waitDocked = new WaitUntil(() => GameFlowMgr_Refact._instance.DockedCount == 63);
        _delayWait = new WaitForSeconds(_gameFlowDelay);
    }

    public void SetDestroyQueue(Stack<BlockBase_Refact> stack)
    {
        _destroyStack = stack;
    }

    public override IEnumerator Handle()
    {
        int stackOriginCount = _destroyStack.Count;

        // ���ÿ� ����� 3�� �̸��� ���, �ƹ��ϵ� �Ͼ���ʵ��� ����
        if (stackOriginCount < 3)
        {
            while (_destroyStack.Count != 0)
            {
                _destroyStack.Pop().ActivateBlockSeletionUI(false);
                yield return null;
            }
        }
        // 3�� �̻��� ���, ����� �ı���Ŵ
        else
        {
            GameFlowMgr_Refact._instance.GameFlow++;
            while (_destroyStack.Count != 0)
            {
                _destroyStack.Pop().DoBreakAction();
                yield return null;
            }

            yield return _waitDocked;
            yield return _delayWait;
            GameFlowMgr_Refact._instance.ChangeGameFlow(_nextGameFlow);
        }
    }
}
