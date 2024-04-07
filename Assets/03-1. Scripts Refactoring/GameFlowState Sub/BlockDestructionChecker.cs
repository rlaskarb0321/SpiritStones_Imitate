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
    /// 선택 기회가 1이상이고, 매개변수로 넘어온 (아이템/노말)블럭이 처음 선택한 블럭과 같은 히어로 종류인지 판단
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
    /// 선택한 블럭이 큐에 완전 동일한 블럭이 있는지, 최근 선택한 블럭과 거리가 먼지를 판단
    /// </summary>
    public bool IsInsertableBlock(BlockBase_Refact block, Stack<BlockBase_Refact> stack)
    {
        if (stack.Count == 0)
            return true;
        if (stack.Peek() == block) // 동일한 블록인지 체크
            return false;

        // Peek 블록과 선택한 블록과의 열(Colum)비교, 바로 옆열이 아니면 False
        BlockBase_Refact peekBlock = stack.Peek();
        if (GetColDifference(block.transform, peekBlock.transform) >= 2)
            return false;

        // Peek 블록과 선택한 블록과의 행(Row)비교, 2행 이상 차이나면 False
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