using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    void Update()
    {
        // E 키를 눌렀을 때에 레이캐스트를 발사합니다.
        if (Input.GetKeyDown(KeyCode.E))
        {
            // 플레이어가 바라보고 있는 방향으로 광선을 쏩니다.
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hitInfo;

            // 광선이 어떤 물체와 충돌하는지 체크합니다.
            if (Physics.Raycast(ray, out hitInfo, 50f))
            {
                // 충돌한 물체의 이름을 Debug.Log로 출력합니다.
                Debug.Log("Hit object: " + hitInfo.collider.gameObject.name);
                GameObject hitObject = hitInfo.collider.gameObject;

                // 아이템 획득 가능한 오브젝트인지 확인합니다.
                InteractiveItem clickedItem = hitObject.GetComponent<InteractiveItem>();
                if (clickedItem != null)
                {
                    clickedItem.onClick();
                }

            }
        }
    }
}