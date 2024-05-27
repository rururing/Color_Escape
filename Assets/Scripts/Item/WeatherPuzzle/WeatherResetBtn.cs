using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherResetBtn : InteractiveItem
{

    public SpringBtn springBtn;
    public SummerBtn summerBtn;
    public FallBtn fallBtn;
    public WinterBtn winterBtn;

    public Password password;

    public Material[] newMaterials; // 새로운 Material 배열
    public GameObject[] objectsToUpdate; // Material을 변경할 게임 오브젝트 배열

    private bool isMoving = false;
    public void Start()
    {
        springBtn = FindObjectOfType<SpringBtn>();
        summerBtn = FindObjectOfType<SummerBtn>();
        fallBtn = FindObjectOfType<FallBtn>();
        winterBtn = FindObjectOfType<WinterBtn>();

        if (objectsToUpdate.Length != newMaterials.Length)
        {
            Debug.LogError("게임 오브젝트와 새로운 Material 배열의 길이가 일치하지 않습니다.");
            return;
        }

    }

    public override void onClick()
    {
        press();

        for (int i = 0; i < objectsToUpdate.Length; i++)
        {
            GameObject obj = objectsToUpdate[i];
            Material newMaterial = newMaterials[i];

            // 게임 오브젝트와 Material이 유효한 경우에만 Material 변경
            if (obj != null && newMaterial != null)
            {
                // 게임 오브젝트의 Renderer 컴포넌트 가져오기
                Renderer renderer = obj.GetComponent<Renderer>();

                // Renderer 컴포넌트가 존재하는 경우 Material 변경
                if (renderer != null && password.unlocked == 0)
                {
                    renderer.material = newMaterial;
                }
                else
                {
                    Debug.LogWarning("게임 오브젝트에 Renderer 컴포넌트가 없습니다: " + obj.name);
                }
            }
        }
      
        springBtn.btnColor = 2;
        summerBtn.btnColor = 1;
        fallBtn.btnColor = 0;
        fallBtn.btnId = 3;
        winterBtn.btnColor = 2;

    }
    public override void press()
    {
        if (!isMoving)
        {
            // 현재 위치를 저장합니다.
            Vector3 currentPosition = transform.position;

            // 목표 위치를 계산합니다.
            Vector3 targetPosition = currentPosition + transform.up * 0.05f;

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
}
