using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherResetBtn : InteractiveItem
{


    private SpringBtn springBtn;
    private SummerBtn summerBtn;
    private FallBtn fallBtn;
    private WinterBtn winterBtn;

    public Material RedBtn;
    public Material GreenBtn;
    public Material BlueBtn;

    private int springOriginalColor;
    private int summerOriginalColor;
    private int fallOriginalColor;
    private int winterOriginalColor;



    public void Start()
    {
        springBtn = GetComponent<SpringBtn>();
        summerBtn = GetComponent<SummerBtn>();
        fallBtn = GetComponent<FallBtn>();
        winterBtn = GetComponent<WinterBtn>();

        if (springBtn != null)
            springOriginalColor = springBtn.btnColor;

        if (summerBtn != null)
            summerOriginalColor = summerBtn.btnColor;

        if (fallBtn != null)
            fallOriginalColor = fallBtn.btnColor;

        if (winterBtn != null)
            winterOriginalColor = winterBtn.btnColor;

    }

    public override void onClick()
    {
        press();

        // 각 버튼의 색상 변경
        if (springBtn != null)
            springBtn.changeColor(BlueBtn);

        if (summerBtn != null)
            summerBtn.changeColor(GreenBtn);

        if (fallBtn != null)
            fallBtn.changeColor(RedBtn);

        if (winterBtn != null)
            winterBtn.changeColor(BlueBtn);

        // 각 버튼의 초기 색상으로 다시 설정
        if (springBtn != null)
            springBtn.btnColor = springOriginalColor;

        if (summerBtn != null)
            summerBtn.btnColor = summerOriginalColor;

        if (fallBtn != null)
            fallBtn.btnColor = fallOriginalColor;

        if (winterBtn != null)
            winterBtn.btnColor = winterOriginalColor;

    }
    public override void press()
    {

        // 현재 위치를 저장합니다.
        Vector3 currentPosition = transform.position;

        // 목표 위치를 계산합니다.
        Vector3 targetPosition = currentPosition + transform.up * 0.05f;

        // 오브젝트를 이동시키는 코루틴을 시작합니다.
        StartCoroutine(MoveObject(currentPosition, targetPosition, 0.05f));
    }

    // 오브젝트를 이동시키는 코루틴 함수
    private IEnumerator MoveObject(Vector3 startPos, Vector3 endPos, float duration)
    {
        // 이동 중인지 여부를 나타내는 변수
        bool moving = true;

        float elapsedTime = 0;

        while (elapsedTime < duration && moving)
        {
            transform.position = Vector3.Lerp(startPos, endPos, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 이동이 완료되면 다시 원래 위치로 되돌아갑니다.
        elapsedTime = 0;
        while (elapsedTime < duration && moving)
        {
            transform.position = Vector3.Lerp(endPos, startPos, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void changeColor(Material newMaterial)
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            // Assign the new material to the renderer
            renderer.material = newMaterial;
        }
    }
 
}
