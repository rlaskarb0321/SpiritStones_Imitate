using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombItem : ItemBlock
{
    public override eSpecialBlockType SpecialType { get { return _specialType; } set { _specialType = value; } }
    public override float MovSpeed { get { return _movSpeed; } set { _movSpeed = value; } }
    public override bool IsDocked { get { return _isDocked; } set { _isDocked = value; } }

    private void Start()
    {
        base.AddToMemoryList();
        MoveBlock(this.gameObject);
        _thisImg = GetComponent<Image>();
        SpecialType = eSpecialBlockType.Bomb_Thief;
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
        yield return new WaitUntil(() => GameManager._instance._dockedCount == 63);

        Debug.Log("Bomb Item Action");
        base.RemoveFromMemoryList();
        Destroy(gameObject);
    }
}
