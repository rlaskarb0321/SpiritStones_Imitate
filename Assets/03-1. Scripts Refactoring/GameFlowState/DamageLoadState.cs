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

        // 스택에 블록이 3개 미만일 경우, 아무일도 일어나지않도록 설정
        if (stackOriginCount < 3)
        {
            while (_destroyStack.Count != 0)
            {
                _destroyStack.Pop().ActivateBlockSeletionUI(false);
                yield return null;
            }
        }
        // 3개 이상일 경우, 블록을 파괴시킴
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
