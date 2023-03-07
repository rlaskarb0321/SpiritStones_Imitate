using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockChain : MonoBehaviour
{
    private LineRenderer _lr;
    private Color _initMatColor;
    public Material _lineRendererMat;

    private void Awake()
    {
        _lr = GetComponent<LineRenderer>();
        _initMatColor = _lineRendererMat.color;
    }

    private void Update()
    {
        #region 23/03/02 라인렌더러 포지션카운트 값을 미리 설정해서 생긴 문제
        //if (GameManager._instance._breakList.Count == 2)
        //{
        //    _lr.positionCount = 6;

        //    _lr.SetPosition(0, GameManager._instance._breakList[0].transform.position);
        //    _lr.SetPosition(1, GameManager._instance._breakList[1].transform.position);
        //}  <-- 이건 0번째 인덱스를 정하지않아서 그런건가 싶어서 테스트용도
        // 포지션카운트를 넉넉하게 잡으면 해당 컴포넌트를 가진 Obj의 (0, 0, 0)에 선을 그림
        #endregion

        if (Input.GetMouseButton(0))
        {
            if (GameManager._instance._breakList.Count == 0)
                return;

            // 라인렌더러 Mat 색깔조정하기
            BlockBase block = GameManager._instance._breakList[0].GetComponent<BlockBase>();
            Color color;
            switch (block.NormalType)
            {
                case eNormalBlockType.Warrior:
                    ColorUtility.TryParseHtmlString("#FF9494", out color);
                    color.a = _initMatColor.a;
                    _lineRendererMat.color = color;
                    break;
                case eNormalBlockType.Archer:
                    ColorUtility.TryParseHtmlString("#2AFD2A", out color);
                    color.a = _initMatColor.a;
                    _lineRendererMat.color = color;
                    break;
                case eNormalBlockType.Thief:
                    ColorUtility.TryParseHtmlString("#F5FF00", out color);
                    color.a = _initMatColor.a;
                    _lineRendererMat.color = color;
                    break;
                case eNormalBlockType.Magician:
                    ColorUtility.TryParseHtmlString("#1ED6FF", out color);
                    color.a = _initMatColor.a;
                    _lineRendererMat.color = color;
                    break;
            }

            _lr.positionCount = GameManager._instance._breakList.Count;
            int index = GameManager._instance._breakList.Count - 1;
            _lr.SetPosition(index, GameManager._instance._breakList[index].transform.position);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _lr.positionCount = 0; 
            _lineRendererMat.color = _initMatColor;
        }
    }
}