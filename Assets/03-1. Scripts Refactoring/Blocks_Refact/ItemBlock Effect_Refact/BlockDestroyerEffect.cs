using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroyerEffect : MonoBehaviour
{
    // 아이템 블록의 파괴 효과에 적중된 블록들을 저장하는 스택
    private Stack<BlockBase_Refact> _triggeredBlockStack;
    private bool _isTriggerEnd;

    public bool IsTriggerEnd { get { return _isTriggerEnd; } }

    private void Awake()
    {
        _triggeredBlockStack = new Stack<BlockBase_Refact>();
    }

    /// <summary>
    /// 마지막 애니메이션에 호출하는 이벤트 메서드
    /// </summary>
    public void DestroySelf()
    {
    }

    public Stack<BlockBase_Refact> GetTriggerBlockStack()
    {
        return _triggeredBlockStack;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("BlockBorder"))
            return;

        BlockBase_Refact triggeredBlock = collision.GetComponent<BlockBase_Refact>();
        if (triggeredBlock == null)
            return;
        if (_triggeredBlockStack.Count != 0 && _triggeredBlockStack.Contains(triggeredBlock))
            return;

        print("Push Slice Trigger Block");
        _triggeredBlockStack.Push(triggeredBlock);
        _isTriggerEnd = true;
    }
}
