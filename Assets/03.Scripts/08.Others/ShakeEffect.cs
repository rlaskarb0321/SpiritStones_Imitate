using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _magnitudePos;

    private RectTransform _rt;

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
    }

    public IEnumerator ShakeTeam()
    {
        float passTime = 0.0f;
        Vector3 originPos = _rt.anchoredPosition;
        while (passTime < _duration)
        {
            // Vector3 shakePos = Random.insideUnitCircle;
            float xAxisShake = Random.insideUnitCircle.x;
            float yAxisShake = Random.insideUnitCircle.y * 0.5f;
            Vector3 shakePos = new Vector3(xAxisShake, yAxisShake, 0.0f);

            transform.localPosition = originPos + (shakePos * _magnitudePos);
            passTime += Time.deltaTime;
            yield return null;
        }

        _rt.anchoredPosition = originPos;
    }
}
