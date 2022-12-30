using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 모든 블럭들이 기본적으로 가지고 있어야 하는 스크립트
 */

public class BlockBase : MonoBehaviour
{
    public float _movSpeed;
    public eNormalBlockType _normalType;
    public eSpecialBlockType _specialType;

    public bool _isDocked;
    private WaitUntil _wu;

    private void Start()
    {
        GameManager._instance._blockMgrList.Add(this.gameObject);
        MoveBlock(this.gameObject);
        _wu = new WaitUntil(() => !_isDocked);
    }

    public void DoNormalBlockAction()
    {
        Debug.Log("NormalBlock 파괴Sound, Animation 재생");
    }

    public void DoItemBlockAction(SpecialBlock specialBlock)
    {
        specialBlock.DoAction();
    }

    public void DoObstacleBlockAction()
    {
        Debug.Log("Do ObstacleBlock Action!!!");
    }

    public void MoveBlock(GameObject block)
    {
        StartCoroutine(MovePosition(block));
    }

    IEnumerator MovePosition(GameObject block)
    {
        yield return _wu; // !_isDocked 이면 실행

        // 동작 구현
        block.transform.Translate(Vector2.down * _movSpeed * Time.deltaTime);

        StartCoroutine(MovePosition(block));
    }
}
