using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordItem : ItemBlock
{
    public override eSpecialBlockType SpecialType
    {
        get { return _specialType; }
        set { _specialType = value; }
    }
    public override float MovSpeed { get { return _movSpeed; } set { _movSpeed = value; } }
    public override bool IsDocked { get { return _isDocked; } set { _isDocked = value; } }

    [HideInInspector] public bool _isIn_YequalX_Zone;
    public GameObject _sliceEffect;
    private WaitForSeconds _ws;
    private BlockBreaker _blockBreaker;

    private void Start()
    {
        base.AddToMemoryList();
        _thisImg = GetComponent<Image>();
        SpecialType = eSpecialBlockType.Sword_Warrior;
        _ws = new WaitForSeconds(0.5f);
        _blockBreaker = new BlockBreaker();
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
        _thisImg.sprite = _ignitedImg;
    }

    protected override IEnumerator ItemAction()
    {
        yield return _ws;
        yield return new WaitUntil(() => GameManager._instance._dockedCount == 63);
        yield return _ws;

        GameObject parent = GameObject.Find("Effect Group");
        GameObject sliceEffect = null;
        if (gameObject.name.Contains("Dual"))
        {
            for (int i = 0; i < 2; i++)
            {
                sliceEffect = Instantiate(_sliceEffect, transform.position, 
                    Quaternion.Euler(0, 0, (-1 + (2 * i)) * 27.425f), parent.transform);
            }
        }
        else
        {
            if (_isIn_YequalX_Zone)
            {
                sliceEffect
                    = Instantiate(_sliceEffect, transform.position, Quaternion.Euler(0, 0, 27.425f), parent.transform);
            }
            else
            {
                sliceEffect
                    = Instantiate(_sliceEffect, transform.position, Quaternion.Euler(0, 0, -27.425f), parent.transform);
            }

        }

        yield return new WaitUntil(() => sliceEffect.activeSelf == false);
        Destroy(sliceEffect);
        yield return _ws;

        _blockBreaker.BreakBlock(GameManager._instance._breakList);
        base.RemoveFromMemoryList();
        Destroy(gameObject);
    }
}
