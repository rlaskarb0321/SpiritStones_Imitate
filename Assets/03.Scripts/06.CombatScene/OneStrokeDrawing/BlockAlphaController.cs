using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockAlphaController
{
    // 블럭의 알파값을 어둡게 함
    public void DarkenBlockAlphaValue(BlockBase block, float lowAlpha)
    {
        Image image = block.GetComponent<Image>();
        Color color = image.color;
        color.a = lowAlpha;
        image.color = color;
    }

    // 리스트 속 블럭들의 알파값을 밝게함
    public void BrightenBlockAlphaValue(List<GameObject> list)
    {
        for (int i = 0; i < list.Capacity; i++)
        {
            if (list[i] == null)
                continue;

            Image blockImage = list[i].GetComponent<Image>();
            Color color = blockImage.color;
            if (color.a != 1)
            {
                color.a = 1;
                blockImage.color = color;
            }
        }
    }
}
