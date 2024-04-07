using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionItem_Refact : ItemBlock_Refact, IBlockNoneDestroyerItem
{
    [Header("=== 포션 아이템 ===")]
    [SerializeField] private float _normalHealPercentage;
    [SerializeField] private float _specialHealPercentage;

    public override void ActivateBlockSeletionUI(bool turnOn)
    {
        _selectionParticle.SetActive(turnOn);
    }

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

    protected override void IgniteItemBlock()
    {
        Transform maskImgObj = transform.GetChild(0);
        Image maskImage = maskImgObj.GetComponentInChildren<Image>();

        maskImage.sprite = _ignitedImg;
        _isIgnited = true;
    }
}
