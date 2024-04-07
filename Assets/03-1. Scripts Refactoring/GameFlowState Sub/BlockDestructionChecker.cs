using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestructionChecker
{
    private eBlockHeroType_Refact _selectNormalBlock;
    private eBlockHeroType_Refact _selectItemBlock;
    private int _normalBlockSelectionChance;
    private int _itemBlockSelectionChance;

    public BlockDestructionChecker()
    {
        _selectNormalBlock = eBlockHeroType_Refact.None;
        _selectItemBlock = eBlockHeroType_Refact.None;
        _normalBlockSelectionChance = 1;
        _itemBlockSelectionChance = 1;
    }

    /// <summary>
    /// ���� ��ȸ�� 1�̻��̰�, �Ű������� �Ѿ�� (������/�븻)���� ó�� ������ ���� ���� ����� �������� �Ǵ�
    /// </summary>
    public bool IsDestructibleBlock(string blockTag, eBlockHeroType_Refact blockType)
    {
        switch (blockTag)
        {
            case "NormalBlock":
                if (_normalBlockSelectionChance <= 0)
                {
                    if (_selectNormalBlock == blockType)
                        return true;

                    return false;
                }

                _selectNormalBlock = blockType;
                _normalBlockSelectionChance--;
                return true;

            case "ItemBlock":
                if (_itemBlockSelectionChance <= 0)
                {
                    if (_selectItemBlock == blockType)
                        return true;

                    return false;
                }

                _selectItemBlock = blockType;
                _itemBlockSelectionChance--;
                return true;
        }

        return true;
    }

    /// <summary>
    /// ������ ���� ť�� ���� ������ ���� �ִ���, �ֱ� ������ ���� �Ÿ��� ������ �Ǵ�
    /// </summary>
    public bool IsInsertableBlock(BlockBase_Refact block, Stack<BlockBase_Refact> stack)
    {
        if (stack.Count == 0)
            return true;
        if (stack.Peek() == block) // ������ ������� üũ
            return false;

        // Peek ��ϰ� ������ ��ϰ��� ��(Colum)��, �ٷ� ������ �ƴϸ� False
        BlockBase_Refact peekBlock = stack.Peek();
        if (GetColDifference(block.transform, peekBlock.transform) >= 2)
            return false;

        // Peek ��ϰ� ������ ��ϰ��� ��(Row)��, 2�� �̻� ���̳��� False
        if (GetRowDifference(peekBlock.transform, block.transform) >= 2)
            return false;

        return true;
    }

    public void ResetCondition()
    {
        _selectNormalBlock = eBlockHeroType_Refact.None;
        _selectItemBlock = eBlockHeroType_Refact.None;
        _normalBlockSelectionChance = 1;
        _itemBlockSelectionChance = 1;
    }

    private int GetRowDifference(Transform compareA, Transform compareB)
    {
        return Mathf.Abs(compareA.GetSiblingIndex() - compareB.GetSiblingIndex());
    }

    private int GetColDifference(Transform compareA, Transform compareB)
    {
        int aCol = int.Parse(compareA.parent.tag.Split("_")[1]);
        int bCol = int.Parse(compareB.parent.tag.Split("_")[1]);

        return Mathf.Abs(aCol - bCol);
    }
}