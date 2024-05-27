using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PenguinResetBtn : InteractiveItem
{
    private bool isMoving = false;
    private bool PenguinIsMoving = false;

    private Rigidbody targetRb; // 다른 게임 오브젝트의 Rigidbody를 참조할 변수
    private Vector3 startPosition; // 시작 위치를 저장하는 변수
    private Quaternion startRotation; // 시작 각도를 저장하는 변수

    void Start()
    {
        // 오브젝트를 이름이나 태그로 찾아서 참조
        GameObject targetObject = GameObject.Find("PENGUIN"); // 또는 GameObject.FindWithTag("TargetTag");
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        if (targetObject != null)
        {
            targetRb = targetObject.GetComponent<Rigidbody>(); // 참조한 오브젝트의 Rigidbody 컴포넌트
            startPosition = targetRb.position; // 시작 위치 저장
            startRotation = targetRb.rotation; // 시작 각도 저장
        }
    }

    public override void onClick()
    {
        press();
        GameOver();
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

    void GameOver()
    {
        // 게임오버 메시지 출력
        Debug.Log("Game Over!");

        List<GameObject> obstacles = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        GameObject[] rootObjects = scene.GetRootGameObjects();

        foreach (GameObject rootObject in rootObjects)
        {
            obstacles.AddRange(FindGameObjectsWithTagInChildren(rootObject, "Obstacle"));
        }

        foreach (GameObject obstacle in obstacles)
        {
            obstacle.SetActive(true);
        }

        if (targetRb != null)
        {
            // 참조한 오브젝트를 시작 위치와 각도로 되돌리기
            targetRb.position = startPosition;
            targetRb.rotation = startRotation;
            targetRb.velocity = Vector3.zero; // 오브젝트의 속도를 초기화
            targetRb.angularVelocity = Vector3.zero; // 오브젝트의 각속도를 초기화
        }

        // 이동 상태 초기화
        PenguinIsMoving = false;
    }

    private IEnumerable<GameObject> FindGameObjectsWithTagInChildren(GameObject parent, string tag)
    {
        List<GameObject> taggedObjects = new List<GameObject>();

        if (parent.CompareTag(tag))
        {
            taggedObjects.Add(parent);
        }

        foreach (Transform child in parent.transform)
        {
            taggedObjects.AddRange(FindGameObjectsWithTagInChildren(child.gameObject, tag));
        }

        return taggedObjects;
    }
}

