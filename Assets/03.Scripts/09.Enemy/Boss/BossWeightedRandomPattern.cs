using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeightedRandomPattern : MonoBehaviour
{
    [System.Serializable]
    public struct Pattern { public string patternName; public float patternPercentage; }
    [SerializeField] private Pattern[] _patternSetting;
    private float _randomPicker;

    // 가중치 랜덤 계산하기
    public void SetWeightData()
    {
        float totalWeight = CalcTotalWeight(_patternSetting);
        for (int i = 0; i < _patternSetting.Length; i++)
        {
            _patternSetting[i].patternPercentage = CalcWeighPercentage(_patternSetting[i].patternPercentage, totalWeight);
        }

        SortCandidateAscending(_patternSetting);
    }

    // 가중치 랜덤으로 지정된 결과를 반환
    public string ReturnRandomPattern()
    {
        float accWeight = 0.0f;
        _randomPicker = Random.Range(0.0f, 1.0f);
        for (int i = 0; i < _patternSetting.Length; i++)
        {
            accWeight += _patternSetting[i].patternPercentage;
            if (_randomPicker - accWeight <= 0.0f)
            {
                // Debug.Log("randomPicker : " + randomPicker + ", pattern : " + _patternSetting[i].patternName);
                return _patternSetting[i].patternName;
            }
        }

        return _patternSetting[_patternSetting.Length - 1].patternName;
    }

    // 가중치 랜덤 요소들의 총 무게를 계산
    private float CalcTotalWeight(Pattern[] candidates)
    {
        float result = 0.0f;
        for (int i = 0; i < candidates.Length; i++)
        {
            result += candidates[i].patternPercentage;
        }

        return result;
    }

    // 가중치 요소들의 총 무게속 자신의 비율을 반환
    private float CalcWeighPercentage(float value, float total)
    {
        return value / total;
    }

    // 가중치 랜덤의 요소들을 가중치를 기준으로 오름차순 정렬
    private void SortCandidateAscending(Pattern[] candidates)
    {
        float tempFloat = 0.0f;
        string tempStr = "";
        for (int i = 0; i < candidates.Length - 1; i++)
        {
            for (int j = i + 1; j < candidates.Length; j++)
            {
                if (candidates[i].patternPercentage > candidates[j].patternPercentage)
                {
                    tempFloat = candidates[i].patternPercentage;
                    tempStr = candidates[i].patternName;

                    candidates[i].patternPercentage = candidates[j].patternPercentage;
                    candidates[i].patternName = candidates[j].patternName;

                    candidates[j].patternPercentage = tempFloat;
                    candidates[j].patternName = tempStr;
                }
            }
        }
    }
}
