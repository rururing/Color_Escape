using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBtn : InteractiveItem
{
    public BananaPlate bp;
    public CarrotPlate cp;
    public SalmonPlate sp;

    public KitchenPuzzleManager password;

    public Material BluePlate; 



    public void Start()
    {
        bp = FindObjectOfType<BananaPlate>();
        cp = FindObjectOfType<CarrotPlate>();
        sp = FindObjectOfType<SalmonPlate>();

    }

    public override void onClick()
    {
        press();

        // BananaPlate의 재질 변경
        if (bp != null)
        {
            bp.GetComponent<Renderer>().material = BluePlate;
        }

        // CarrotPlate의 재질 변경
        if (cp != null)
        {
            cp.GetComponent<Renderer>().material = BluePlate;
        }

        // SalmonPlate의 재질 변경
        if (sp != null)
        {
            sp.GetComponent<Renderer>().material = BluePlate;
        }

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
}
