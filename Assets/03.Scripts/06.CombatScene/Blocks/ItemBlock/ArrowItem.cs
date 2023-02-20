using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowItem : ItemBlock
{
    public GameObject _arrowPrefabs;
    [SerializeField] private int _arrowCount;
    public override eSpecialBlockType SpecialType
    {
        get { return _specialType; }
        set { _specialType = value; }
    }
    public override float MovSpeed { get { return _movSpeed; } set { _movSpeed = value; } }
    public override bool IsDocked { get { return _isDocked; } set { _isDocked = value; } }
    private WaitForSeconds _ws;
    private BlockBreaker _blockBreaker;

    private void Start()
    {
        base.AddToMemoryList();
        _thisImg = GetComponent<Image>();
        SpecialType = eSpecialBlockType.Arrow_Archer;
        _blockBreaker = GameObject.Find("Canvas").GetComponent<BlockBreaker>();
        _ws = new WaitForSeconds(0.5f);
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

        GameObject arrowProjectileGroup = GameObject.Find("Effect Group");
        for (int i = 0; i < _arrowCount; i++)
        {
            GameObject arrow = 
                Instantiate(_arrowPrefabs, transform.position, Quaternion.identity, arrowProjectileGroup.transform) 
                as GameObject;
            int randomVal = Random.Range(0, GameManager._instance._blockMgrList.Capacity);

            ArrowProjectile arrowProjectile = arrow.GetComponent<ArrowProjectile>();
            BlockBase targetBlock = GameManager._instance._blockMgrList[randomVal].GetComponent<BlockBase>();
            arrowProjectile.GetTarget(targetBlock);
        }

        yield return new WaitUntil(() => arrowProjectileGroup.transform.childCount == 0);
        yield return _ws;

        _blockBreaker.BreakBlock(GameManager._instance._breakList);
        base.RemoveFromMemoryList();
        Destroy(gameObject);
    }
}