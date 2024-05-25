using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveAbleItem : MonoBehaviour
{
    public float moveSpeed = 1f; // 이동 속도
    private bool isMoving = false; // 이동 여부를 확인하는 플래그
    private Rigidbody rb; // Rigidbody 컴포넌트를 참조하기 위한 변수


    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 참조
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
    }
}
