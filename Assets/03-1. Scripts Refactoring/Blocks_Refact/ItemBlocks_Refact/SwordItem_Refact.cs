using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordItem_Refact : ItemBlock_Refact, IBlockDestroyerItem
{
    [Header("=== Į ������ ===")]
    [SerializeField] private BlockDestroyerEffect _swordSlice;
    [SerializeField] private bool _isInUpwardRightSliceZone;

    // �Ѻױ׸���� ���õǾ�����, ���õ� �� ��ҵǾ����� ���� UI On/Off
    public override void ActivateBlockSeletionUI(bool turnOn)
    {
        _selectionParticle.SetActive(turnOn);
    }

    // ������ ����� ��� ����ϸ� ��ȭ���·� ���
    public override void DoBreakAction()
    {
        if (!_isIgnited)
        {
            IgniteItemBlock();
            return;
        }

        Destroy(gameObject);
    }

    // ������ ��� ��ȭ
    protected override void IgniteItemBlock()
    {
        Image maskImage = transform.GetComponentInChildren<Image>();
        maskImage.sprite = _ignitedImg;
        _isIgnited = true;
    }


    // �����̽� ���� �ٲٴ� ���� ������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Change Slice Dir"))
            return;

        _isInUpwardRightSliceZone = true;
    }

    // �����̽� ���� �ٲٴ� ���� ������
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Change Slice Dir"))
            return;

        _isInUpwardRightSliceZone = false;
    }

    public IEnumerator FillDestroyStack(Stack<BlockBase_Refact> stack)
    {
        Transform effectParent = GameObject.Find("Effect Group").transform;

        // Į �������� ��ġ�Ѱ������� �׾����� ������ �ٸ���
        if (_isSpecialItem)
        {
            for (int i = 0; i < 2; i++)
                Instantiate(_swordSlice, transform.position, Quaternion.Euler(0.0f, 0.0f, (-1 + (2 * i)) * 27.425f), effectParent);
        }

        if (_isInUpwardRightSliceZone)
        {
            Instantiate(_swordSlice, transform.position, Quaternion.Euler(0, 0, 27.425f), effectParent);
        }
        else
        {
            Instantiate(_swordSlice, transform.position, Quaternion.Euler(0, 0, -27.425f), effectParent);
        }

        print("wait until " + _swordSlice.IsTriggerEnd);
        yield return new WaitUntil(() => _swordSlice.IsTriggerEnd == true); // �̰� ��� ó���ؾ��Ѵ�..
        print("Get triggered Stack");
        Stack<BlockBase_Refact> triggeredStack = _swordSlice.GetTriggerBlockStack();
        while (triggeredStack.Count != 0)
        {
            BlockBase_Refact block = triggeredStack.Pop();
            if (!stack.Contains(block))
            {
                stack.Push(block);
            }
        }
    }
}
