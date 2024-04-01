using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockBreaker : MonoBehaviour, IGameFlow
{
    private BlockBreakSound _blockBreakSound;
    bool _isSelectable = true;

    private void Awake()
    {
        _blockBreakSound = GetComponent<BlockBreakSound>();
    }

    private void Start()
    {
        
    }

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
                BlockBase undoBlock = list[list.Count - 1];
                undoBlock.SetBlockHighLight(undoBlock.tag, false);
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
        block.SetBlockHighLight(block.tag, true);
        block._blockSound.PlayBlockPickUpSound();
    }

    // 아이템 효과에 적중한 블럭들을 넣는 메서드
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
            DoGameFlowAction();
            _blockBreakSound.PlayBreakSound();

            for (int i = list.Count - 1; i >= 0; i--)
            {
                BlockBase block = list[i];
                block.SetBlockHighLight(block.tag, false);
                block.DoAction();
                list.RemoveAt(i); 
            }
        }
        else
        {
            while (list.Count > 0)
            {
                BlockBase block = list[list.Count - 1];
                block.SetBlockHighLight(block.tag, false);
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
