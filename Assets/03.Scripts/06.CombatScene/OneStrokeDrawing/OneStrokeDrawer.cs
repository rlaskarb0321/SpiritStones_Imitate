using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneStrokeDrawer : MonoBehaviour
{
    [SerializeField] private List<string> _normalList;
    [SerializeField] private List<string> _itemList;
    [SerializeField] private int _normalCount; // 노말블럭을 몇 종류 선택할 수 있는 기회
    [SerializeField] private int _itemCount; // 아이템블럭을 몇 종류 선택할 수 있는 기회

    private BlockBreaker _breakBlock;
    private CanvasRayCaster _canvasRayCaster;

    private void Awake()
    {
        _normalList = new List<string>();
        _itemList = new List<string>();

        _breakBlock = GetComponent<BlockBreaker>();
        _canvasRayCaster = GetComponent<CanvasRayCaster>();

        InitBlockList(_normalList, _itemList);
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        if (GameManager._instance._gameOverMgr._heroLifeState == eHeroLife.Dead ||
            GameManager._instance._gameOverMgr._heroLifeState == eHeroLife.DeadSceneProgress)
            return;

        if (GameManager._instance._gameFlow != eGameFlow.Idle)
            return;

        if (Input.GetMouseButton(0))
        {
            _canvasRayCaster.CanvasRaycast();
            GameObject block = _canvasRayCaster.ReturnRayResult();
            if (block == null)
                return;

            BlockBase blockBase = block.GetComponent<BlockBase>();
            if (blockBase == null)
                return;

            // 블럭들의 태그값에 따른 행동
            switch (block.tag)
            {
                case "NormalBlock":
                    if (_normalCount > 0)
                    {
                        RemoveNormalTypeAtList(blockBase, _normalList);
                    }
                    else
                    {
                        if (!_normalList.Contains(blockBase.NormalType.ToString()))
                        {
                            _breakBlock.PushToDrawnBlockList(GameManager._instance._breakList, blockBase);
                        }
                    }
                    break;

                case "ItemBlock":
                    if (_itemCount > 0)
                    {
                        RemoveItemTypeAtList(blockBase, _itemList);
                    }
                    else
                    {
                        if (!_itemList.Contains(blockBase.SpecialType.ToString()))
                        {
                            _breakBlock.PushToDrawnBlockList(GameManager._instance._breakList, blockBase);
                        }
                    }
                    break;

                case "ObstacleBlock":
                    _breakBlock.PushToDrawnBlockList(GameManager._instance._breakList, blockBase);
                    break;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            InitBlockList(_normalList, _itemList);
            _breakBlock.BreakBlock(GameManager._instance._breakList);
        }
    }

    void InitBlockList(List<string> normalList, List<string> itemList)
    {
        normalList.Capacity = Enum.GetValues(typeof(eNormalBlockType)).Length;
        itemList.Capacity = Enum.GetValues(typeof(eSpecialBlockType)).Length;

        foreach (eNormalBlockType unreachable in Enum.GetValues(typeof(eNormalBlockType)))
        {
            if (unreachable == eNormalBlockType.None)
                continue;

            if (!normalList.Contains(unreachable.ToString()))
            {
                normalList.Add(unreachable.ToString());
            }
        }

        foreach (eSpecialBlockType unreachable in Enum.GetValues(typeof(eSpecialBlockType)))
        {
            if (unreachable == eSpecialBlockType.None)
                continue;

            if (!itemList.Contains(unreachable.ToString()))
            {
                itemList.Add(unreachable.ToString());
            }
        }

        _normalCount = 1;
        _itemCount = 1;
    }


    /// <summary>
    /// 더 이을수 없는 노말블럭들을 리스트에 남겨놓음
    /// </summary>
    void RemoveNormalTypeAtList(BlockBase block, List<string> unreachableNormalBlock)
    {
        _normalCount--;

        string blockType = block.NormalType.ToString();
        if (unreachableNormalBlock.Contains(blockType))
        {
            unreachableNormalBlock.Remove(blockType);
        }
    }


    /// <summary>
    /// 더 이을수 없는 아이템블럭들을 리스트에 남겨놓음
    /// </summary>
    /// <param name="block">ItemBlock의 enum종류를 추출하기 위한 변수</param>
    /// <param name="unreachableItemBlock">추출한 enum을 리스트에서 제거함</param>
    void RemoveItemTypeAtList(BlockBase block, List<string> unreachableItemBlock)
    {
        _itemCount--;

        string[] itemTag = block.SpecialType.ToString().Split('_');
        string itemType = itemTag[itemTag.Length - 1];
        for (int i = unreachableItemBlock.Count - 1; i >= 0; i--)
        {
            if (unreachableItemBlock[i].Contains(itemType))
            {
                unreachableItemBlock.RemoveAt(i);
            }
        }
    }
}
