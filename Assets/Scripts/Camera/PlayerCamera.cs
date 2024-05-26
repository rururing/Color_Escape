using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCamera : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;
    public GameObject[] panels; // 게임 매니저에서 활성화되는 패널 배열

    float xRotation;
    float yRotation;

    private void Start()
    {
    }

    private void Update()
    {
        bool panelActive = false;

        // 모든 패널을 순회하며 활성화 여부 확인
        foreach (GameObject panel in panels)
        {
            if (panel.activeSelf)
            {
                panelActive = true;
                break;
            }
        }

        // 패널이 활성화되어 있는 경우에는 마우스 입력을 받지 않음
        if (panelActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }
     

        // 마우스 입력 받기
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        // Y 축 회전 계산
        yRotation += mouseX;

        // X 축 회전 계산 및 제한
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -30f, 30f);

        // 카메라 및 방향 전환 회전 적용
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}