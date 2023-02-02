using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBreaker : IGameFlow
{
    bool _isSelectable = true;

    public void PushToDrawnBlockList(List<BlockBase> list, BlockBase block)
    {
        if (!_isSelectable)
            return;

        if (list.Count != 0)
        {
            if (list[list.Count - 1] == block)
            {
                // Debug.Log("같은 블럭은 넣을 수 없음");
                return;
            }

            if (list[list.Count - 1] != block && list.Contains(block))
            {
                // Debug.Log("한붓그리기 Undo하는 중");
                list.RemoveAt(list.Count - 1);
                return;
            }

            string peekCol = list[list.Count - 1].transform.parent.ToString().Split(" ")[0];
            string blockCol = block.transform.parent.ToString().Split(" ")[0];

            int peekColNum = peekCol[peekCol.Length - 1] - '0';
            int blockColNum = blockCol[blockCol.Length - 1] - '0';

            if (Mathf.Abs(peekColNum - blockColNum) >= 2 ||
                Mathf.Abs(list[list.Count - 1].transform.localPosition.y - block.transform.localPosition.y) >= 120)
            {
                // Debug.Log("위 아래로 두칸이상 or 옆으로 두칸 넘게있음");
                _isSelectable = false;
                return;
            }

            if (Mathf.Abs(peekColNum - blockColNum) == 1 &&
                Mathf.Abs(list[list.Count - 1].transform.position.y - block.transform.position.y) >= 0.4)
            {
                // Debug.Log("옆칸이지만 칸수가 2칸이상 차이남");
                _isSelectable = false;
                return;
            }
        }

        // Debug.Log("모든 조건 통과했으니 넣을 수 있음");
        list.Add(block);
    }

    public void PushItemActionBlock(List<BlockBase> list, BlockBase block)
    {
        if (list.Contains(block))
            return;

        list.Add(block);
    }

    public void BreakBlock(List<BlockBase> list)
    {
        _isSelectable = true;

        if (list.Count >= 3)
        {
            /* 이곳에 블록이 깨질때 사운드를 재생시켜야한다. 
             * 그러려면 이 스크립트에 MonoBehavior를 상속받게하고 Canvas에 부착시킨다음
             * Canvas에 AudioSource 컴포넌트를 추가하고 재생시키게 하자
             */

            DoGameFlowAction();

            for (int i = list.Count - 1; i >= 0; i--)
            {
                BlockBase block = list[i];
                block.DoAction();
                list.RemoveAt(i); 
            }
        }
        else
        {
            while (list.Count > 0)
            {
                BlockBase block = list[list.Count - 1];
                list.RemoveAt(list.Count - 1);
            }
        }
    }

    public void DoGameFlowAction()
    {
        // eGameState.Idle 일때
        if (GameManager._instance._gameFlow == eGameFlow.Idle)
            GameManager._instance._gameFlow++;
    }
}
