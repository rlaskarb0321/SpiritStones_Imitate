using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroyerEffect : MonoBehaviour
{
    // ������ ����� �ı� ȿ���� ���ߵ� ��ϵ��� �����ϴ� ����
    private Stack<BlockBase_Refact> _triggeredBlockStack;

    private void Awake()
    {
        _triggeredBlockStack = new Stack<BlockBase_Refact>();
    }

    /// <summary>
    /// ������ �ִϸ��̼ǿ� ȣ���ϴ� �̺�Ʈ �޼���
    /// </summary>
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public Stack<BlockBase_Refact> GetTriggerBlockStack()
    {
        return _triggeredBlockStack;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("BlockBorder"))
        {
            print(LayerMask.LayerToName(collision.gameObject.layer));
            return;
        }

        BlockBase_Refact triggeredBlock = collision.GetComponent<BlockBase_Refact>();
        if (triggeredBlock == null)
        {
            print("triggeredBlock is null");
            return;
        }
        if (_triggeredBlockStack.Count != 0 && _triggeredBlockStack.Contains(triggeredBlock))
        {
            print(_triggeredBlockStack.Count + " Contain? " + _triggeredBlockStack.Contains(triggeredBlock));
            return;
        }

        _triggeredBlockStack.Push(triggeredBlock);
    }
}
