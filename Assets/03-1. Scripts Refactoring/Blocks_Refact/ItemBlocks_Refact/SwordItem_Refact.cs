using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordItem_Refact : ItemBlock_Refact, IBlockDestroyerItem
{
    [Header("=== 칼 아이템 ===")]
    [SerializeField] private GameObject _swordSliceEffect;
    [SerializeField] private bool _isInUpwardRightSliceZone;

    // 한붓그리기로 선택되었을때, 선택된 후 취소되었을때 관련 UI On/Off
    public override void ActivateBlockSeletionUI(bool turnOn)
    {
        _selectionParticle.SetActive(turnOn);
    }

    // 아이템 블록의 경우 사용하면 점화상태로 대기
    public override void DoBreakAction(HeroTeamMgr_Refact heroTeam)
    {
        if (!_isIgnited)
        {
            IgniteItemBlock();
            ActivateBlockSeletionUI(false);
            return;
        }

        Destroy(gameObject);
    }

    // 아이템 블록 점화
    protected override void IgniteItemBlock()
    {
        Transform maskImgObj = transform.GetChild(0);
        Image maskImage = maskImgObj.GetComponentInChildren<Image>();

        maskImage.sprite = _ignitedImg;
        _isIgnited = true;
    }


    // 슬라이스 방향 바꾸는 영역 감지용
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Change Slice Dir"))
            return;

        _isInUpwardRightSliceZone = true;
    }

    // 슬라이스 방향 바꾸는 영역 감지용
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Change Slice Dir"))
            return;

        _isInUpwardRightSliceZone = false;
    }

    public IEnumerator FillDestroyStack(Stack<BlockBase_Refact> stack, DamageLoadState damageLoader)
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

                // 칼 아이템이 위치한곳에따라 그어지는 방향을 다르게
                if (_isInUpwardRightSliceZone)
                    effect = Instantiate(_swordSliceEffect, transform.position, Quaternion.Euler(0, 0, 27.425f), effectParent).GetComponent<BlockDestroyerEffect>();
                else
                    effect = Instantiate(_swordSliceEffect, transform.position, Quaternion.Euler(0, 0, -27.425f), effectParent).GetComponent<BlockDestroyerEffect>();

                yield return new WaitUntil(() => effect.IsTriggerEnd == true);
                TradeStack(effect, stack);
                break;
        }

        damageLoader.ItemCount--;
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
