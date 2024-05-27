using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingObstacle : InteractiveItem
{
    public int requiredFlashlightColor; // 사라지기 위해 필요한 손전등 색깔 인덱스


    void Start()
    {
      
    }

    public void lightFlashed(int flashLightColor)
    {
        if (flashLightColor == requiredFlashlightColor) // 특정 색깔 광선일 때만 페이드 아웃 시작
        {
            gameObject.SetActive(false);
        }
    }

   
}