using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordItem_Refact : ItemBlock_Refact, IBlockDestroyerItem
{
    [Header("=== Į ������ ===")]
    [SerializeField] private GameObject _swordSliceEffect;
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
            ActivateBlockSeletionUI(false);
            return;
        }

        Destroy(gameObject);
    }

    // ������ ��� ��ȭ
    protected override void IgniteItemBlock()
    {
        Transform maskImgObj = transform.GetChild(0);
        Image maskImage = maskImgObj.GetComponentInChildren<Image>();

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
        switch (_isSpecialItem)
        {
            case true:
                BlockDestroyerEffect[] effects = new BlockDestroyerEffect[2];
                for (int i = 0; i < effects.Length; i++)
                {
                    effects[i] = Instantiate(_swordSliceEffect, transform.position, Quaternion.Euler(0.0f, 0.0f, (-1 + (2 * i)) * 27.425f), effectParent).GetComponent<BlockDestroyerEffect>();
                }

                yield return new WaitUntil(() => effects[1].IsTriggerEnd);
                for (int i = 0; i < effects.Length; i++)
                {
                    TradeStack(effects[i], stack);
                }
                break;

            case false:
                BlockDestroyerEffect effect;

                // Į �������� ��ġ�Ѱ������� �׾����� ������ �ٸ���
                if (_isInUpwardRightSliceZone)
                    effect = Instantiate(_swordSliceEffect, transform.position, Quaternion.Euler(0, 0, 27.425f), effectParent).GetComponent<BlockDestroyerEffect>();
                else
                    effect = Instantiate(_swordSliceEffect, transform.position, Quaternion.Euler(0, 0, -27.425f), effectParent).GetComponent<BlockDestroyerEffect>();

                yield return new WaitUntil(() => effect.IsTriggerEnd == true);
                TradeStack(effect, stack);
                break;
        }

        yield return new WaitForSeconds(_delay);
    }

    private void TradeStack(BlockDestroyerEffect effect, Stack<BlockBase_Refact> stack)
    {
        Stack<BlockBase_Refact> triggeredStack = effect.GetTriggerBlockStack();
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
