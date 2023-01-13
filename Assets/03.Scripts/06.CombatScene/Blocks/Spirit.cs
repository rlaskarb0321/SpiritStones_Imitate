using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour
{
    private HeroBase _target;
    public float _waitSeconds;

    public bool _isIdleState;
    private float _startMovSpeed;
    public float _idleMovSpeed;
    public float _deltaSpeedValue;

    private float _xDir;
    private float _yDir;
    private Vector2 _dir;

    private void Start()
    {
        _startMovSpeed = _idleMovSpeed;
        _xDir = Random.Range(-1.0f, 1.0f);
        _yDir = Random.Range(-1.0f, 0.0f);
        _dir = Vector2.right * _xDir + Vector2.up * _yDir;
        _isIdleState = true;

        StartCoroutine(GoToHero(_target));
    }

    private void Update()
    {
        if (_isIdleState)
            IdleRun();
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

    IEnumerator GoToHero(HeroBase target)
    {
        yield return new WaitForSeconds(_waitSeconds);

        _isIdleState = false;
        float absorptionMovSpeed = 0.0f;
        while (true)
        {
            if (absorptionMovSpeed <= _startMovSpeed)
                absorptionMovSpeed += _deltaSpeedValue * Time.deltaTime;

            if (Vector2.Distance(this.transform.position, target.transform.position) <= 0.1f)
            {
                target.DevelopLoadedDamage();
                Destroy(gameObject);
                break;
            }
            
            transform.position =
                Vector3.MoveTowards(this.transform.position, target.transform.position, absorptionMovSpeed * 0.8f);

            yield return null;
        }
    }
}
