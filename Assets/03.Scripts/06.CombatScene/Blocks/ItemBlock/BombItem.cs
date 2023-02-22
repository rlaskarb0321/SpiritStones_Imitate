using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombItem : ItemBlock
{
    public override eSpecialBlockType SpecialType { get { return _specialType; } set { _specialType = value; } }
    public override float MovSpeed { get { return _movSpeed; } set { _movSpeed = value; } }
    public override bool IsDocked { get { return _isDocked; } set { _isDocked = value; } }
    public GameObject _explosionEffect;
    private WaitForSeconds _ws;
    private BlockBreaker _blockBreaker;

    private void Start()
    {
        base.AddToMemoryList();
        _childImg = transform.GetChild(0).GetComponent<Image>();
        SpecialType = eSpecialBlockType.Bomb_Thief;
        _ws = new WaitForSeconds(0.5f);
        _blockBreaker = GameObject.Find("Canvas").GetComponent<BlockBreaker>();
    }

    public override void DoAction()
    {
        if (!_isIgnited)
        {
            ChangeImage();
            _isIgnited = true;
            StartCoroutine(ItemAction());
        }
    }

    protected override void ChangeImage()
    {
        _childImg.sprite = _ignitedImg;
    }

    protected override IEnumerator ItemAction()
    {
        yield return _ws;
        yield return new WaitUntil(() => GameManager._instance._dockedCount == 63);
        yield return _ws;

        GameObject parent = GameObject.Find("Effect Group");
        GameObject explosionEffect = Instantiate(_explosionEffect, transform.position, Quaternion.identity, parent.transform);
        yield return new WaitUntil(() => explosionEffect.activeSelf == false);
        Destroy(explosionEffect);
        yield return _ws;

        _blockBreaker.BreakBlock(GameManager._instance._breakList);
        base.RemoveFromMemoryList();
        Destroy(gameObject);
    }
}
