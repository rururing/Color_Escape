using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingObstacle : MonoBehaviour
{
    public int requiredFlashlightColor; // 사라지기 위해 필요한 손전등 색깔 인덱스
    public float fadeDuration = 2f; // 방해물이 사라지는 데 걸리는 시간 (초)
    private Renderer objectRenderer; // 오브젝트의 렌더러
    private Color originalColor; // 원래 색상
    private bool isFading = false; // 페이드 중인지 여부

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
    }

    public void lightFlashed(int flashLightColor)
    {
        if (flashLightColor == requiredFlashlightColor && !isFading) // 특정 색깔 광선일 때만 페이드 아웃 시작
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        isFading = true;
        float elapsedTime = 0f;
        Color fadeColor = originalColor;

        // Renderer가 여러 개의 Material을 가질 수 있으므로 모든 Material에 대해 알파값 조정
        Material[] materials = objectRenderer.materials;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            
            //점점 흐려지면서 사라지도록 메쉬렌더러의 알파값 조정
            foreach (Material mat in materials)
            {
                fadeColor = mat.color;
                fadeColor.a = alpha;
                mat.color = fadeColor;
            }
            yield return null;
        }

        Destroy(gameObject); // 오브젝트 삭제
    }
}