using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneStrokeDrawer_Refact : MonoBehaviour
{
    CanvasRayCaster _canvasRayCaster;

    private void Awake()
    {
        _canvasRayCaster = GetComponent<CanvasRayCaster>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            GameObject rayResult = _canvasRayCaster.ReturnRayResult_Refact();
            if (rayResult == null)
                return;

            IBreakableBlock breakableBlock = rayResult.GetComponent<IBreakableBlock>();
            if (breakableBlock == null)
                return;

            breakableBlock.DoBlockBreak();
        }

        if (Input.GetMouseButtonUp(0))
        {

        }
    }
}
