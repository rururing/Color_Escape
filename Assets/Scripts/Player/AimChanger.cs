using UnityEngine;
using UnityEngine.UI;

public class AimChanger: MonoBehaviour
{

    public Image canvasImage; // 캔버스 이미지를 참조하기 위한 변수
    private Sprite originalSprite;

    void Start()
    {
        // 캔버스 이미지의 원래 스프라이트를 저장합니다.
        originalSprite = canvasImage.sprite;
    }
    void Update()
    {

        // 플레이어가 바라보고 있는 방향으로 광선을 쏩니다.
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        // 광선이 어떤 물체와 충돌하는지 체크합니다.
        if (Physics.Raycast(ray, out hitInfo, 50f))
        {
            // 충돌한 물체의 이름을 Debug.Log로 출력합니다.
            GameObject hitObject = hitInfo.collider.gameObject;


            InteractiveItem clickedItem = hitObject.GetComponent<InteractiveItem>();
            if (clickedItem != null)
            {
                // 상호작용 가능한 물체일 경우 canvas 이미지를 변경합니다.
                if (canvasImage != null)
                {
                    Sprite yourNewSprite = Resources.Load<Sprite>("RedAim");

                    if (yourNewSprite != null)
                    {
                        canvasImage.sprite = yourNewSprite;
                    }
                }
                

            }
            else
            {     
                // 상호작용 가능한 물체가 아닐 경우 원래의 이미지로 변경합니다.
                canvasImage.sprite = originalSprite;
            }
        }
    }   
}
