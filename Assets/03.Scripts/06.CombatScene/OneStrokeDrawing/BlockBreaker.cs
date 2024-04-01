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
                // Debug.Log("���� ���� ���� �� ����");
                return;
            }

            if (list[list.Count - 1] != block && list.Contains(block))
            {
                // Debug.Log("�Ѻױ׸��� Undo�ϴ� ��");
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
                // Debug.Log("�� �Ʒ��� ��ĭ�̻� or ������ ��ĭ �Ѱ�����");
                _isSelectable = false;
                return;
            }

            if (Mathf.Abs(peekColNum - blockColNum) == 1 &&
                Mathf.Abs(list[list.Count - 1].transform.position.y - block.transform.position.y) >= 0.4)
            {
                // Debug.Log("��ĭ������ ĭ���� 2ĭ�̻� ���̳�");
                _isSelectable = false;
                return;
            }
        }

        // Debug.Log("��� ���� ��������� ���� �� ����");
        list.Add(block);
        block.SetBlockHighLight(block.tag, true);
        block._blockSound.PlayBlockPickUpSound();
    }

    // ������ ȿ���� ������ ������ �ִ� �޼���
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
        // eGameState.Idle �϶�
        if (GameManager._instance._gameFlow == eGameFlow.Idle)
            GameManager._instance._gameFlow++;
    }
}
