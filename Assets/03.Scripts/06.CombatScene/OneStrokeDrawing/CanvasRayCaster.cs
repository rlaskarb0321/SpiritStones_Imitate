using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Graphic RayCaster가 있는 게임Obj(Canvas)에 부착해야 함
public class CanvasRayCaster : MonoBehaviour
{
    private GraphicRaycaster _graphicRaycaster;
    private PointerEventData _pointerEventData;
    private List<RaycastResult> _rrList;

    void Start()
    {
        _graphicRaycaster = GetComponent<GraphicRaycaster>();
        _pointerEventData = new PointerEventData(EventSystem.current);
        _rrList = new List<RaycastResult>();
    }

    // 캔버스에 레이캐스트를 쏴 _rrList에 저장
    public void CanvasRaycast()
    {
        _rrList.Clear();

        _pointerEventData.position = Input.mousePosition;
        _graphicRaycaster.Raycast(_pointerEventData, _rrList);
    }

    // _rrList에 저장된 맨 첫번째 요소를 반환
    public GameObject ReturnRayResult()
    {
        if (_rrList.Count >= 1 && _rrList[0].isValid)
        {
            GameObject topObj = _rrList[0].gameObject;
            _rrList.Clear();
            return topObj;
        }
        else
        {
            return null;
        }
    }

    public GameObject ReturnRayResult_Refact()
    {
        _rrList.Clear();
        _pointerEventData.position = Input.mousePosition;
        _graphicRaycaster.Raycast(_pointerEventData, _rrList);

        if (_rrList.Count < 1 || !_rrList[0].isValid)
            return null;

        GameObject topObj = _rrList[0].gameObject;
        _rrList.Clear();
        return topObj;
    }
}
