using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionResetBtn : InteractiveItem 
{
    public RedFlask redFlask;
    public BlueFlask blueFlask;

    public Material[] newMaterials; // 새로운 Material 배열
    public GameObject[] objectsToUpdate; // Material을 변경할 게임 오브젝트 배열

    void Start()
    {

        redFlask= FindObjectOfType<RedFlask>();
        blueFlask = FindObjectOfType<BlueFlask>();

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
                if (renderer != null)
                {
                    renderer.material = newMaterial;
                }
                else
                {
                    Debug.LogWarning("게임 오브젝트에 Renderer 컴포넌트가 없습니다: " + obj.name);
                }
            }
        }

        redFlask.potionColor = 0;
        blueFlask.potionColor = 1;
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
