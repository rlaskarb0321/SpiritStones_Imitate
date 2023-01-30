using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkullBlock : ObstacleBlock
{
    [Header("=== SkullBlock ===")]
    public Text _explodeCountTxt;
    public float _explodeEffectTime;
    public int _explodeCount;
    public int _dieCount;
    public float _damagePercentage;
    private bool _isEgnited;

    [Header("=== Target ===")]
    public GameObject _heroTeamMgrObj;
    private HeroTeamMgr _heroTeamMgr;

    void Start()
    {
        base.AddToMemoryList();
        base.AddToHarmfulBlockList();

        _heroTeamMgrObj = GameObject.Find("TeamPositionGroup");
        _explodeCountTxt.text = _explodeCount.ToString();
        _heroTeamMgr = _heroTeamMgrObj.GetComponent<HeroTeamMgr>();
    }

    public override void DoAction()
    {
        if(++_explodeCount == _dieCount)
        {
            Die();
            return;
        }
        _explodeCountTxt.text = _explodeCount.ToString();
        _isEgnited = true;
    }

    public override void DoHarmfulAction()
    {
        if (_isEgnited)
        {
            _isEgnited = false;
            return;
        }

        _explodeCount--;
        _explodeCountTxt.text = _explodeCount.ToString();

        if (_explodeCount == 1)
        {
            _explodeCountTxt.color = Color.red;
        }
        else if(_explodeCount == 0)
        {
            _explodeCountTxt.text = _explodeCount.ToString();
            Explode();
        }
    }

    void Explode()
    {
        // ¾Ç¸¶½º·´°Ô ¿ô´Â ¸ñ¼Ò¸® 2.0ÃÊ°£ Àç»ýÈÄ Æã
        while (_explodeEffectTime >= 0.0f)
        {
            _explodeEffectTime -= Time.deltaTime; 
        }

        float damage = _heroTeamMgr._totalHp * _damagePercentage;
        _heroTeamMgr.DecreaseHp(damage);

        base.RemoveFromMemoryList();
        base.RemoveFromHarmfulBlockList();
        Destroy(gameObject);
    }

    void Die()
    {
        base.RemoveFromMemoryList();
        base.RemoveFromHarmfulBlockList();
        Destroy(gameObject);
    }
}
