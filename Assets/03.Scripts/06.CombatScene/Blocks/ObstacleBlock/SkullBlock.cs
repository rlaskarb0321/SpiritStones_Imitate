using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkullBlock : ObstacleBlock
{
    [Header("=== SkullBlock ===")]
    public Text _explodeCountTxt;
    public int _explodeCount;
    public int _dieCount;
    public float _damagePercentage;

    [Header("=== Target ===")]
    public GameObject _heroTeamMgrObj;
    private HeroTeamMgr _heroTeamMgr;

    void Start()
    {
        base.AddToMemoryList();
        base.AddToHarmfulBlockList();

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
    }

    public override void DoHarmfulAction()
    {
        _explodeCount--;
        _explodeCountTxt.text = _explodeCount.ToString();

        if (_explodeCount == 1)
        {
            _explodeCountTxt.color = Color.red;
        }
        else if(_explodeCount == 0)
        {
            _explodeCountTxt.text = _explodeCount.ToString();
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        // 악마스럽게 웃는 목소리 2.0초간 재생후 펑

        yield return new WaitForSeconds(2.0f);

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
