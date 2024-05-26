using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLeft : InteractiveItem
{
    private bool isRotated = false;

    public override void onClick()
    {
        open();
        AudioManager.Instance.PlaySFX("Open Lid");
    }

    public override void open()
    {
        Quaternion currentRotation = transform.rotation;

        Quaternion targetRotation;
        if (isRotated)
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            targetRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 145, 0));
        }

        StartCoroutine(RotateObject(currentRotation, targetRotation, 0.5f)); 

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

        transform.rotation = endRot;
    }

}
