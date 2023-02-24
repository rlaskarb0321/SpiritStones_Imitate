using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionItem : ItemBlock
{
    public override eSpecialBlockType SpecialType
    {
        get { return _specialType; }
        set { _specialType = value; }
    }
    public override float MovSpeed { get { return _movSpeed; } set { _movSpeed = value; } }
    public override bool IsDocked { get { return _isDocked; } set { _isDocked = value; } }
    private WaitForSeconds _ws;
    public GameObject _heroTeamObj;
    private HeroTeamMgr _heroTeam;
    private AudioSource _audio;
    public AudioClip _audioClip;

    private void Start()
    {
        base.AddToMemoryList();
        _childImg = transform.GetChild(0).GetComponent<Image>();
        _audio = GetComponent<AudioSource>();
        SpecialType = eSpecialBlockType.Potion_Magician;
        _ws = new WaitForSeconds(0.5f);

        _heroTeamObj = GameObject.Find("TeamPositionGroup");
        _heroTeam = _heroTeamObj.GetComponent<HeroTeamMgr>();
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

        switch (_specialType)
        {
            case eSpecialBlockType.Potion_Magician:
                _heroTeam.GetComponent<HeroTeamUI>().IncreaseHp(_heroTeam._heroTeamUI._totalHp * 0.2f);
                _audio.PlayOneShot(_audioClip, 1.0f);
                break;

            case eSpecialBlockType.Elixir_Magician:
                _heroTeam.GetComponent<HeroTeamUI>().IncreaseHp(_heroTeam._heroTeamUI._totalHp * 0.45f);
                _audio.PlayOneShot(_audioClip, 1.0f);
                break;
        }

        yield return _ws;

        base.RemoveFromMemoryList();
        Destroy(gameObject);
    }
}
