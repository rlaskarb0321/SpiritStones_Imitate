using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestructionChecker
{
    private BlockHeroType_Refact _selectNormalBlock;
    private BlockHeroType_Refact _selectItemBlock;
    private int _normalBlockSelectionChance;
    private int _itemBlockSelectionChance;

    public BlockDestructionChecker()
    {
        _selectNormalBlock = BlockHeroType_Refact.None;
        _selectItemBlock = BlockHeroType_Refact.None;
        _normalBlockSelectionChance = 1;
        _itemBlockSelectionChance = 1;
    }

    /// <summary>
    /// 선택 기회가 1이상이고, 매개변수로 넘어온 (아이템/노말)블럭이 처음 선택한 블럭과 같은 히어로 종류인지 판단후 선택
    /// </summary>
    public bool IsDestructibleBlock(string blockTag, BlockHeroType_Refact blockType)
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
    /// 선택한 블럭이 큐에 완전 동일한 블럭이 있는지, 최근 선택한 블럭과 거리가 먼지를 판단후 Enqueue
    /// </summary>
    public bool IsInsertableBlock(BlockBase_Refact block, Stack<BlockBase_Refact> stack)
    {
        // 완전 동일한 블럭 체크
        if (stack.Count != 0 && stack.Peek() == block)
            return false;
        // 거리 체크

        return true;
    }

    public void ResetCondition()
    {
        _selectNormalBlock = BlockHeroType_Refact.None;
        _selectItemBlock = BlockHeroType_Refact.None;
        _normalBlockSelectionChance = 1;
        _itemBlockSelectionChance = 1;
    }
}