using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSliceEffect : MonoBehaviour
{
    private BlockBreaker _blockBreaker;
    public bool _isSliceAnimEnd;

    private void Start()
    {
        _blockBreaker = new BlockBreaker();
        StartCoroutine(StartSliceAnim());
    }
    
    public void OnSliceAnimEnd()
    {
        _isSliceAnimEnd = true;
    }

    IEnumerator StartSliceAnim()
    {
        yield return new WaitUntil(() => _isSliceAnimEnd);
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
