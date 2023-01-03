using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    public bool _fire;
    public float _fireSpeed;
    private Vector3 _targetPos;
    private BlockBase _targetBlock;
    private BlockBreaker _blockBreaker = new BlockBreaker();

    public void GetTarget(BlockBase targetBlock)
    {
        _targetBlock = targetBlock;
        _targetPos = targetBlock.transform.position;
        _fire = true;
    }

    private void Update()
    {
        if (_fire)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPos, _fireSpeed * Time.deltaTime);

            Quaternion targetRot = Quaternion.LookRotation(_targetPos);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * 5);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _targetBlock.gameObject)
        {
            BlockBase blockBase = collision.GetComponent<BlockBase>();
            _blockBreaker.PushItemActionBlock(GameManager._instance._breakList, blockBase);

            Destroy(gameObject, 0.1f);
        }
    }
}
