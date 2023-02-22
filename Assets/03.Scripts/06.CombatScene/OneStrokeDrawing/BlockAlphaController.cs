using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockAlphaController
{
    // 블럭의 알파값을 어둡게 함
    public void DarkenBlockAlphaValue(BlockBase block, float lowAlpha)
    {
        Image[] images = block.GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++)
        {
            Color color = images[i].color;
            color.a = lowAlpha;
            images[i].color = color;
        }
    }

    // 리스트 속 블럭들의 알파값을 밝게함
    public void BrightenBlockAlphaValue(List<GameObject> list)
    {
        for (int i = 0; i < list.Capacity; i++)
        {
            if (list[i] == null)
                continue;

            Image[] blockImgs = list[i].GetComponentsInChildren<Image>();
            for (int j = 0; j < blockImgs.Length; j++)
            {
                Color colors = blockImgs[j].color;
                if (colors.a != 1)
                {
                    colors.a = 1;
                    blockImgs[j].color = colors;
                }
            }
        }
    }
}
