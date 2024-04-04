using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombItem_Refact : ItemBlock_Refact, IBlockDestroyerItem
{
    [Header("=== ∆¯≈∫ æ∆¿Ã≈€ ===")]
    [SerializeField] private GameObject _normalExplosionEffect;
    [SerializeField] private GameObject _specialExplosionEffect;

    public override void ActivateBlockSeletionUI(bool turnOn)
    {
        _selectionParticle.SetActive(turnOn);
    }

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

    public IEnumerator FillDestroyStack(Stack<BlockBase_Refact> stack, DamageLoadState damageLoader)
    {
        Transform effectParent = GameObject.Find("Effect Group").transform;
        BlockDestroyerEffect effect = null;
        if (_isSpecialItem)
            effect = Instantiate(_specialExplosionEffect, transform.position, Quaternion.identity, effectParent).GetComponent<BlockDestroyerEffect>();
        else
            effect = Instantiate(_normalExplosionEffect, transform.position, Quaternion.identity, effectParent).GetComponent<BlockDestroyerEffect>();

        yield return new WaitUntil(() => effect.IsTriggerEnd == true);
        TradeStack(effect, stack);
        damageLoader.ItemCount--;
        yield return new WaitForSeconds(_delay);
    }

    protected override void IgniteItemBlock()
    {
        Transform maskImgObj = transform.GetChild(0);
        Image maskImage = maskImgObj.GetComponentInChildren<Image>();

        maskImage.sprite = _ignitedImg;
        _isIgnited = true;
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
