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
    /// ���� ��ȸ�� 1�̻��̰�, �Ű������� �Ѿ�� (������/�븻)���� ó�� ������ ���� ���� ����� �������� �Ǵ��� ����
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
    /// ������ ���� ť�� ���� ������ ���� �ִ���, �ֱ� ������ ���� �Ÿ��� ������ �Ǵ��� Enqueue
    /// </summary>
    public bool IsInsertableBlock(BlockBase_Refact block, Queue<BlockBase_Refact> queue)
    {
        // ���� ������ �� üũ
        if (queue.Count != 0 && queue.Peek().Equals(block))
            return false;
        // �Ÿ� üũ

        return true;
    }

    public void ResetCondition()
    {
        _selectNormalBlock = eBlockHeroType_Refact.None;
        _selectItemBlock = eBlockHeroType_Refact.None;
        _normalBlockSelectionChance = 1;
        _itemBlockSelectionChance = 1;
    }
}