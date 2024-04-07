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

    public int TargetHitCount
    {
        get { return _targetHitCount; }
        set
        {
            if (value > _launchCount)
                return;

            _targetHitCount = value;
        }
    }

    private int _launchCount;
    private int _targetHitCount;

    private void Awake()
    {
        _launchCount = _isSpecialItem ? _specialItemArrowCount : _arrowCount;
    }

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

    public IEnumerator FillDestroyStack(Stack<BlockBase_Refact> stack, DamageLoadState damageLoader)
    {
        Transform effectParent = GameObject.Find("Effect Group").transform;
        BlockContainer blockContainer = GameObject.FindWithTag("BlockContainer").GetComponent<BlockContainer>();
        for (int i = 0; i < _launchCount; i++)
        {
            GameObject randomBlock = blockContainer.GetRandomBlock();
            ArrowProjectile_Refact arrow = Instantiate(_arrowPrefab, transform.position, Quaternion.identity, effectParent).GetComponent< ArrowProjectile_Refact>();

            arrow.SetTargetBlock(randomBlock, this);
            if (!randomBlock.Equals(this))
                stack.Push(randomBlock.GetComponent<BlockBase_Refact>());
        }

        yield return new WaitUntil(() => _targetHitCount == _launchCount);
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
}
