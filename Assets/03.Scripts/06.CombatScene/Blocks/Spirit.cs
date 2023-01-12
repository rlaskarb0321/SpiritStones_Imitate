using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour
{
    private HeroBase _target;

    private float _startMovSpeed;
    public float _idleMovSpeed;
    public float _deltaSpeedValue;
    [SerializeField] private float _absorptionMovSpeed;

    private float _xDir;
    private float _yDir;
    private Vector2 _dir;

    private void Start()
    {
        _startMovSpeed = _idleMovSpeed;
        _xDir = Random.Range(-1.0f, 1.0f);
        _yDir = Random.Range(-1.0f, 0.0f);
        _dir = Vector2.right * _xDir + Vector2.up * _yDir;
    }

    private void FixedUpdate()
    {
        if (_idleMovSpeed > 0.0f)
        {
            IdleRun();
        }
        else
        {
            GoToHero(_target);
        }
    }

    public void SetTarget(HeroBase target)
    {
        _target = target;
    }

    void IdleRun()
    {
        transform.Translate(_dir.normalized * _idleMovSpeed * Time.deltaTime);
        _idleMovSpeed -= _deltaSpeedValue * Time.deltaTime;
    }

    public void GoToHero(HeroBase target)
    {
        if (Vector2.Distance(this.transform.position, target.transform.position) <= 0.001f)
            Destroy(gameObject, 0.2f);

        if (_absorptionMovSpeed <= _startMovSpeed)
            _absorptionMovSpeed += _deltaSpeedValue * Time.deltaTime;

        transform.position = 
            Vector3.MoveTowards(this.transform.position, target.transform.position, _absorptionMovSpeed * 0.8f);
    }
}
