using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestLid : InteractiveItem
{
    public Password check;
    private bool isOpen = false;
    private bool isAnimating = false; // 애니메이션이 실행 중인지 여부를 나타내는 플래그
    private float duration = 2.0f; // 열리는 시간 (초)


    // 상자를 열지 못할 때 표시할 텍스트 UI 요소
    public Text lockedText;

    public void Start()
    {
        // 시작 시에 텍스트를 비활성화
        lockedText.gameObject.SetActive(false);
    }

    public override void onClick()
    {
        // 다른 스테이지에서 조건 해금이 이루어진경우에만 실행
        if (!isAnimating && check.unlocked == 1)
        {
            StartCoroutine(AnimateOpen());
            return;
        }
        else if (check.unlocked == 0)
        {
            // 텍스트를 활성화하여 상자를 열지 못한다는 메시지를 표시
            lockedText.gameObject.SetActive(true);
            // 2초 후에 비활성화되도록 Invoke() 호출
            Invoke("HideText", 2.0f);
        }
    }

    // 텍스트를 숨기는 메서드
    private void HideText()
    {
        lockedText.gameObject.SetActive(false);
    }

    private IEnumerator AnimateOpen()
    {
        isAnimating = true; // 애니메이션이 실행 중임을 표시
        float timer = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        if (!isOpen)
        {
            endRotation = Quaternion.Euler(60, 0, 0) * startRotation;
        }
        else
        {
            endRotation = Quaternion.Euler(-60, 0, 0) * startRotation;
        }

        while (timer < duration)
        {
            float progress = timer / duration;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, progress);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
        isOpen = !isOpen;
        isAnimating = false; // 애니메이션이 종료되었음을 표시
    }
}