using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBtn : InteractiveItem
{
    // btnColor 0 : R, 1 : G, 2: B, 3: C, 4: M, 5: Y, 6: W

    public int btnColor = 2;
    public Flashlight flashlight;
    public Material CyanBtn;
    public Material MagentaBtn;
    public Material WhiteBtn;


    public void makeCyan()
    {
        if (btnColor == 2)
        {
            changeColor(CyanBtn);
            btnColor = 3;
        }
    }
    public void makeMagenta()
    {
        if (btnColor == 2)
        {
            changeColor(MagentaBtn);
            btnColor = 4;
        }
    }
    public void makeWhite()
    {
        if (btnColor == 4 || btnColor == 3)
        {
            changeColor(WhiteBtn);
            btnColor = 6;
        }
    }


    public override void onClick()
    {
        press();
    }

    public override void press()
    {
        // 현재 위치를 저장합니다.
        Vector3 currentPosition = transform.position;

        // 목표 위치를 계산합니다.
        Vector3 targetPosition = currentPosition + transform.forward * 0.05f;

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



    public override void lightFlashed(int flashLightColor)
    {

        if (btnColor == 2)
        {
            //Debug.Log("B플라스크에서 후레쉬 색" + flashlight.flashLightColor);

            if (flashlight.flashLightColor == 3)
            {
                makeCyan();
            }
            else if (flashlight.flashLightColor == 2)
            {
                makeMagenta();
            }
        }
        if (btnColor == 4)
        {
            if (flashlight.flashLightColor == 3)
            {
                makeWhite();
            }
        }
        if (btnColor == 3)
        {
            if (flashlight.flashLightColor == 2)
            {
                makeWhite();
            }
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
