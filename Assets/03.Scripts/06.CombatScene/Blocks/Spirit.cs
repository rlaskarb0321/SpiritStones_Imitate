using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour
{
    public float _movSpeed;
    public float _breakTime;

    private float _xDir;
    private float _yDir;
    private Vector2 _dir;

    private void Start()
    {
        _xDir = Random.Range(-1.0f, 1.0f);
        _yDir = Random.Range(-1.0f, 0.0f);
        _dir = Vector2.right * _xDir + Vector2.up * _yDir;
    }

    private void Update()
    {
        if (_breakTime > 0.0f)
        {
            IdleRun();
        }
        else
        {
            GoToHero();
        }
    }

    void IdleRun()
    {
        transform.Translate(_dir.normalized * (_movSpeed * _breakTime) * Time.deltaTime);
        _breakTime -= Time.deltaTime;
    }

    void GoToHero() // 매개변수에 GameObject target을 추가하고 Hero스크립트에서 this를 넣을 예정
    {
        _breakTime = 0.0f;
        // Debug.Log("알맞는 영웅에게 날라간 후 파괴됩니다");

        // 영웅에게 흡수되는걸 일단을 이렇게 표현
        Destroy(gameObject, 1.0f);
    }
}
