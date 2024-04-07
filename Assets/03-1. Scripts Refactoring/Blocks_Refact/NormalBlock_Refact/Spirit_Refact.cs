using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit_Refact : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] [Range(0.0f, 1.0f)] private float _idleTime;
    [SerializeField] [Range(0.0f, 0.3f)] private float _lerpMoveValue;

    private HeroBase_Refact _target;
    private Rigidbody2D _rbody2D;
    private float _currIdleTime;
    private Vector2 _randomDir;

    private void Awake()
    {
        _rbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _currIdleTime = _idleTime;
        _randomDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 0.0f)).normalized;
    }

    private void FixedUpdate()
    {
        if (_target == null)
            return;

        if (_currIdleTime > 0.0f)
        {
            Vector2 explosionForce = _randomDir * _speed * _currIdleTime;

            _rbody2D.MovePosition(_rbody2D.position + explosionForce * Time.deltaTime);
            _currIdleTime -= Time.deltaTime;
        }
        else
        {
            if (Vector2.Distance(transform.position, _target.transform.position) < 0.1f)
            {
                // 영웅의 공격 데미지 축적시키기
                _target.AccumulatedDamage += _target.Damage;
                Destroy(gameObject);
                return;
            }

            Vector2 newPosition = Vector2.Lerp(_rbody2D.position, _target.transform.position, _lerpMoveValue);
            _rbody2D.MovePosition(newPosition);
        }
    }

    public void SetTarget(HeroBase_Refact target)
    {
        _target = target;
    }
}
