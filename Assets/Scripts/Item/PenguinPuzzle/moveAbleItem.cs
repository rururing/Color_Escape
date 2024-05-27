using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveAbleItem : InteractiveItem
{
    public float moveSpeed = 1f; // 이동 속도
    private bool isMoving = false; // 이동 여부를 확인하는 플래그
    private Rigidbody rb; // Rigidbody 컴포넌트를 참조하기 위한 변수
    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 참조
        startPosition = rb.position; // 시작 위치 저장
        startRotation = rb.rotation;
    }



    // 손전등의 빛에 반응하는 함수
    public void lightFlashed(int flashLightColor)
    {
        if (flashLightColor == 3) //초록색 빛이면 이동
        {
            isMoving = true;
        }
        else if (flashLightColor == 2) //빨간색 빛이면 멈춤
        {
            isMoving = false;
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            MoveForward();
        }
    }

    void MoveForward()
    {
        // Rigidbody를 사용하여 이동
        Vector3 newPosition = rb.position + transform.forward * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    // 충돌 감지 함수
    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 "CheckPoint" 레이어에 속해 있는지 확인
        if (collision.gameObject.layer == LayerMask.NameToLayer("CheckPoint"))
        {
            // 왼쪽으로 90도 회전
            transform.Rotate(0, -90, 0);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("impediments"))
        {
            // 방해물과 충돌 시 게임오버 처리
            GameOver();
        }

    }
    void GameOver()
    {
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

        if (rb != null)
        {
            // 참조한 오브젝트를 시작 위치와 각도로 되돌리기
            rb.position = startPosition;
            rb.rotation = startRotation;
            rb.velocity = Vector3.zero; // 오브젝트의 속도를 초기화
            rb.angularVelocity = Vector3.zero; // 오브젝트의 각속도를 초기화
        }

        // 이동 상태 초기화
        isMoving = false;
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

