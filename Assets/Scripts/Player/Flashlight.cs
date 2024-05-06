using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Flashlight : MonoBehaviour
{

    Light light;
    public int flashLightColor = 0;


    // 빨간색, 초록색, 파란색을 순서대로 저장할 배열
    Color[] colors = { Color.clear, Color.red, Color.green, Color.blue };

    // 손전등 색 1 : None, 2 : R, 3 : G, 0 : B

    void Start()
    {
        light = GetComponent<Light>();

        ChangeLightColor();
    }

    // 색상을 변경하는 함수
    void ChangeLightColor()
    {
        // 현재 인덱스에 해당하는 색상으로 변경
        light.color = colors[flashLightColor];
        // 다음에 변경될 색상 인덱스 업데이트
        flashLightColor = (flashLightColor + 1) % colors.Length;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeLightColor();
            Debug.Log("토글" + flashLightColor);
            
        }
        
        
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hitInfo;

            // 광선이 어떤 물체와 충돌하는지 체크합니다.
            if (Physics.Raycast(ray, out hitInfo, 5f))
            {
                //Debug.Log("Flashlight Hit object: " + hitInfo.collider.gameObject.name);
                GameObject hitObject = hitInfo.collider.gameObject;
                InteractiveItem flahsedObject = hitObject.GetComponent<InteractiveItem>();

                if (flahsedObject != null)
                {
                    flahsedObject.lightFlashed(flashLightColor);
                }

            }
        
    }
}





