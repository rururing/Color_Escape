using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenRight : InteractiveItem
{

    private bool isRotated = false;
 
    public override void onClick()
    {
        open();
        AudioManager.Instance.PlaySFX("Open Lid");
    }
    public override void open()
    {
        // 현재 회전을 저장합니다.
        Quaternion currentRotation = transform.rotation;

        // 목표 회전을 계산합니다. 회전 상태에 따라 다르게 설정합니다.
        Quaternion targetRotation;
        if (isRotated)
        {
            // 현재 회전된 상태라면 0도로 되돌립니다.
            targetRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            // 현재 회전되지 않은 상태라면 회전시킵니다.
            targetRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, -145, 0));
        }

        // 오브젝트를 회전시키는 코루틴을 시작합니다.
        StartCoroutine(RotateObject(currentRotation, targetRotation, 0.5f)); // 회전 시간은 0.5초로 설정

        // 회전 상태를 반대로 전환합니다.
        isRotated = !isRotated;
    }

    // 오브젝트를 회전시키는 코루틴 함수
    private IEnumerator RotateObject(Quaternion startRot, Quaternion endRot, float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(startRot, endRot, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 마지막 위치를 정확히 맞추기 위해 루프가 끝난 후 최종 위치를 설정
        transform.rotation = endRot;
    }
}
