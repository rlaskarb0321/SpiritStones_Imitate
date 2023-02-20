using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    public bool _fire;
    public float _fireSpeed;
    public float _lifeTime;
    private Vector3 _targetPos;
    private BlockBase _targetBlock;
    private BlockBreaker _blockBreaker;

    private void Awake()
    {
        _blockBreaker = GameObject.Find("Canvas").GetComponent<BlockBreaker>();
    }

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

            float angle = 
                Mathf.Atan2((_targetBlock.transform.position.y - this.transform.position.y),
                           (_targetBlock.transform.position.x - this.transform.position.x)) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _targetBlock.gameObject)
        {
            BlockBase blockBase = collision.GetComponent<BlockBase>();
            _blockBreaker.PushItemActionBlock(GameManager._instance._breakList, blockBase);

            Destroy(gameObject, _lifeTime);
        }
    }
}
