using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombExplosion : MonoBehaviour
{
    private BlockBreaker _blockBreaker;
    public bool _isExplosionAnimEnd;

    private void Start()
    {
        _blockBreaker = GameObject.Find("Canvas").GetComponent<BlockBreaker>();
        StartCoroutine(StartExplosionAnim());
    }

    public void OnExplosionAnimEnd()
    {
        _isExplosionAnimEnd = true;
    }

    IEnumerator StartExplosionAnim()
    {
        yield return new WaitUntil(() => _isExplosionAnimEnd);
        yield return new WaitForSeconds(0.15f);

        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BlockBase blockBase = collision.gameObject.GetComponent<BlockBase>();
        if (blockBase == null)
            return;
        else
        {
            _blockBreaker.PushItemActionBlock(GameManager._instance._breakList, blockBase);
        }
    }
}
