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

    void Awake()
    {
        Reset();
        base.AddToMemoryList();
        base.AddToHarmfulBlockList();

        _explodeCountTxt.text = _explodeCount.ToString();
        _heroTeamMgr = _heroTeamMgrObj.GetComponent<HeroTeamMgr>();

        UpdateTxtColor(_explodeCountTxt, _explodeCount);
    }

    public override void DoAction()
    {
        if(++_explodeCount == _dieCount)
        {
            Die();
            return;
        }

        UpdateTxtColor(_explodeCountTxt, _explodeCount);
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
        UpdateTxtColor(_explodeCountTxt, _explodeCount);

        if (_explodeCount == 0)
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

    private void Reset()
    {
        _movSpeed = 1.2f;
        _obstacleType = eObstacleBlockType.Skull;

        _explodeCountTxt = GameObject.Find("SkullBlock_Obstacle").transform.GetChild(0).GetComponent<Text>();
        Text countTxt = Instantiate(_explodeCountTxt, transform.position, Quaternion.identity,
            this.transform) as Text;
        countTxt.transform.localPosition = new Vector3(0.0f, 26.3f, 0.0f);
        _explodeCountTxt = transform.GetChild(0).GetComponent<Text>();
        _explodeEffectTime = 5.5f;
        _explodeCount = 4;
        _dieCount = 4;
        _damagePercentage = 0.15f;

        _heroTeamMgrObj = GameObject.Find("TeamPositionGroup");
    }

    void UpdateTxtColor(Text text, int count)
    {
        if (count == 1)
        {
            text.color = Color.red;
        }
        else
        {
            text.color = Color.white;
        }
    }
}
