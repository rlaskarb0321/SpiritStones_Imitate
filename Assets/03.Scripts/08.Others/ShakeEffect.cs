using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    private RectTransform _rt;
    private Transform _tr;
    private Vector3 _originPos;

    public float _duration;
    public float _magnitudePos;

    private void Awake()
    {
        _tr = GetComponent<Transform>();
        _rt = GetComponent<RectTransform>();
        _originPos = _rt.anchoredPosition;
    }

    public IEnumerator ShakeTeam()
    {
        float passTime = 0.0f;
        while (passTime < _duration)
        {
            // Vector3 shakePos = Random.insideUnitCircle;
            float xAxisShake = Random.insideUnitCircle.x;
            float yAxisShake = Random.insideUnitCircle.y * 0.5f;
            Vector3 shakePos = new Vector3(xAxisShake, yAxisShake, 0.0f);

            _tr.localPosition = _originPos + (shakePos * _magnitudePos);
            passTime += Time.deltaTime;
            yield return null;
        }

        _rt.anchoredPosition = _originPos;
    }
}
