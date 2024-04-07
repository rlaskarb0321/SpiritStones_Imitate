using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit_Refact : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] [Range(0.0f, 1.0f)] private float _idleTime;
    [SerializeField] [Range(0.0f, 0.3f)] private float _lerpMoveValue;

    private Rigidbody2D _rbody2D;
    private float _currIdleTime;

    private void Awake()
    {
        _rbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _currIdleTime = _idleTime;
    }

    public IEnumerator SetTarget(GameObject target)
    {
        Vector2 randomDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 0.0f)).normalized;
        while (_currIdleTime >= 0.0f)
        {
            Vector2 explosionForce = randomDir * _speed * _currIdleTime;

            _rbody2D.MovePosition(_rbody2D.position + explosionForce * Time.deltaTime);
            _currIdleTime -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        while (Vector2.Distance(transform.position, target.transform.position) >= 0.1f)
        {
            Vector2 newPosition = Vector2.Lerp(_rbody2D.position, target.transform.position, _lerpMoveValue);

            _rbody2D.MovePosition(newPosition);
            yield return new WaitForFixedUpdate();
        }
    }
}
