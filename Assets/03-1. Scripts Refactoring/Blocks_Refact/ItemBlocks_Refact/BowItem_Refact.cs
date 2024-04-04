using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowItem_Refact : ItemBlock_Refact, IBlockDestroyerItem
{
    [Header("=== 화살 아이템 ===")]
    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private int _arrowCount;
    [SerializeField] private int _specialItemArrowCount;

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

    public IEnumerator FillDestroyStack(Stack<BlockBase_Refact> stack)
    {
        yield return null;
    }

    protected override void IgniteItemBlock()
    {
        Transform maskImgObj = transform.GetChild(0);
        Image maskImage = maskImgObj.GetComponentInChildren<Image>();

        maskImage.sprite = _ignitedImg;
        _isIgnited = true;
    }
}
