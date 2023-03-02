using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockChain : MonoBehaviour
{
    private LineRenderer _lr;

    private void Awake()
    {
        _lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        //if (GameManager._instance._breakList.Count == 2)
        //{
        //    _lr.positionCount = 6;

        //    _lr.SetPosition(0, GameManager._instance._breakList[0].transform.position);
        //    _lr.SetPosition(1, GameManager._instance._breakList[1].transform.position);
        //}  <-- 이건 0번째 인덱스를 정하지않아서 그런건가 싶어서 테스트용도

        if (Input.GetMouseButton(0))
        {
            if (GameManager._instance._breakList.Count == 0)
                return;

            // _lr.positionCount = 35; <-- 얘가 문제였음 (포지션카운트를 넉넉하게 잡으면 해당 컴포넌트를 가진 Obj의 (0, 0, 0)에 선을 그림
            _lr.positionCount = GameManager._instance._breakList.Count;
            int index = GameManager._instance._breakList.Count - 1;
            _lr.SetPosition(index, GameManager._instance._breakList[index].transform.position);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _lr.positionCount = 0;
        }
    }
}