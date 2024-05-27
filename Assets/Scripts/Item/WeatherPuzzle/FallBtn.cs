using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallBtn : InteractiveItem
{
    // btnColor 0 : R, 1 : G, 2: B, 3: C, 4: M, 5: Y, 6: W

    public int btnId = 3;
    public int btnColor = 0;
    public WeatherPuzzleManager puzzle;
    public Flashlight flashlight;
    public Material YellowBtn;
    public Material MagentaBtn;
    public Material WhiteBtn;
    private Password password;
    public Text lockedText1;
    public Text lockedText2;

    private bool isMoving = false;

    public void Start()
    {
        // 시작 시에 텍스트를 비활성화
        lockedText1.gameObject.SetActive(false);
        lockedText2.gameObject.SetActive(false);
        password = FindObjectOfType<Password>();
    }
   
    public void makeYellow()
    {
        if (btnColor == 0)
        {
            changeColor(YellowBtn);
            btnColor = 5;
     
        }

    }
    public void makeMagenta()
    {
        if (btnColor == 0)
        {
            changeColor(MagentaBtn);
            btnColor = 4;
            
        }
    }
    public void makeWhite()
    {
        if (btnColor == 4 || btnColor == 5)
        {
            changeColor(WhiteBtn);
            btnColor = 6;
            
        }
    }

    public override void onClick()
    {
        if (puzzle.unlocked == 1)
        {
            press();

            if (password != null)
            {
                password.OnButtonPressed(btnId); // btnId는 버튼의 ID입니다. 이를 사용하여 버튼의 ID를 전달합니다.
            }

        }
        else
        {
            // 텍스트를 활성화하여 상자를 열지 못한다는 메시지를 표시
            lockedText1.gameObject.SetActive(true);
            // 2초 후에 비활성화되도록 Invoke() 호출
            Invoke("HideText", 2.0f);
        }
    }
    private void HideText()
    {
        lockedText1.gameObject.SetActive(false);
        lockedText2.gameObject.SetActive(false);
    }
    public override void press()
    {
        if (!isMoving)
        {
            // 현재 위치를 저장합니다.
            Vector3 currentPosition = transform.position;

            // 목표 위치를 계산합니다.
            Vector3 targetPosition = currentPosition + transform.forward * 0.05f;

            // 오브젝트를 이동시키는 코루틴을 시작합니다.
            StartCoroutine(MoveObject(currentPosition, targetPosition, 0.05f));
        }
    }

    // 오브젝트를 이동시키는 코루틴 함수
    private IEnumerator MoveObject(Vector3 startPos, Vector3 endPos, float duration)
    {
        isMoving = true;  // 이동 시작

        float elapsedTime = 0;

        // 목표 위치로 이동
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // 목표 위치에 정확히 도달하도록 설정
        transform.position = endPos;

        // 원래 위치로 돌아가기 전에 잠시 대기 (원하는 경우)
        yield return new WaitForSeconds(0.1f);

        elapsedTime = 0;

        // 원래 위치로 이동
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(endPos, startPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // 원래 위치에 정확히 도달하도록 설정
        transform.position = startPos;

        isMoving = false;  // 이동 종료
    }

    public override void lightFlashed(int flashLightColor)
    {

        if (puzzle.unlocked == 1 && password.unlocked == 0)
        {
            if (btnColor == 0)
            {
                //Debug.Log("R플라스크에서 후레쉬 색" + flashlight.flashLightColor);

                if (flashlight.flashLightColor == 3)
                {
                    makeYellow();
                    btnId = 0;
                }
                else if (flashlight.flashLightColor == 0)
                {
                    makeMagenta();
                    btnId = 0;
                }
            }
            if (btnColor == 4)
            {
                if (flashlight.flashLightColor == 3)
                {
                    makeWhite();
                    btnId = 0;
                }
            }
            if (btnColor == 5)
            {
                if (flashlight.flashLightColor == 0)
                {
                    makeWhite();
                    btnId = 0;
                }
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
