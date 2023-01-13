using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    public GameObject _combatMgrObj;
    private CombatSceneMgr _combatMgr;

    public GameObject[] _normalBlockPrefabsArr;
    public GameObject[] _normalItemBlockPrefabsArr;
    public GameObject[] _specialItemBlockPrefabsArr;
    public GameObject[] _obstacleBlockPrefabsArr;

    private void Start()
    {
        _combatMgr = _combatMgrObj.GetComponent<CombatSceneMgr>();
    }

    /*
     * 보스라운드라면 obstacleBlock도 생성된다.
     */
    eBlockType DetermineBlockType()
    {
        eBlockType blockType;
        int randomVal = Random.Range(0, 100);

        // 현재가 보스라운드인지 아닌지 확인
        if (_combatMgr._currLevel == _combatMgr._maxLevelValue)
        {
            if (randomVal <= _combatMgr._obstacleBlockPercentage)
            {
                blockType = eBlockType.Obstacle;
            }
            else if (randomVal <= _combatMgr._itemBlockPercentage)
            {
                blockType = eBlockType.Item;
            }
            else
            {
                blockType = eBlockType.Normal;
            }
        }
        else
        {
            if (randomVal <=
                (_combatMgr._itemBlockPercentage - _combatMgr._obstacleBlockPercentage))
            {
                blockType = eBlockType.Item;
            }
            else
            {
                blockType = eBlockType.Normal;
            }
        }

        return blockType;
    }

    public void GenerateBlock(Vector2 spawnPos, Transform parent)
    {
        eBlockType blockType = DetermineBlockType();
        int randomValue;
        switch (blockType)
        {
            case eBlockType.Normal:
                randomValue = 
                    Random.Range(0, _normalBlockPrefabsArr.Length);
                GameObject normalBlock = Instantiate
                    (_normalBlockPrefabsArr[(int)randomValue], spawnPos, Quaternion.identity, parent) as GameObject;
                break;

            case eBlockType.Item:
                randomValue =
                    Random.Range(0, _normalItemBlockPrefabsArr.Length);
                GameObject itemBlock = Instantiate
                    (_normalItemBlockPrefabsArr[(int)randomValue], spawnPos, Quaternion.identity, parent) as GameObject;
                break;

            case eBlockType.Obstacle:
                break;
        }
    }

    public GameObject GenerateSpecialItemBlock()
    {
        int randomValue = Random.Range(0, _specialItemBlockPrefabsArr.Length);
        return null;
    }
}
