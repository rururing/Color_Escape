using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimChanger: MonoBehaviour
{

    public Image canvasImage; // 캔버스 이미지를 참조하기 위한 변수
    private Sprite originalSprite;
    public Text text; // "E"키를 눌러서 상호작용할 수 있다는 내용
    public Text lightText; // "F"키를 눌러서 상호작용할 수 있다는 내용
    public Text resetText; // "E"키를 눌러서 리셋할 수 있다는 내용
    public Text unlockText; // 아직 상호작용을 할 수 없다는 내용
    public WeatherPuzzleManager puzzle;

    void Start()
    {
        // 캔버스 이미지의 원래 스프라이트를 저장합니다.
        originalSprite = canvasImage.sprite;
        text.enabled = false;
        lightText.enabled = false;
        resetText.enabled = false;
        unlockText.enabled = false;

    }
    void Update()
    {

        // 플레이어가 바라보고 있는 방향으로 광선을 쏩니다.
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        // 광선이 어떤 물체와 충돌하는지 체크합니다.
        if (Physics.Raycast(ray, out hitInfo, 10f))
        {
         
            GameObject hitObject = hitInfo.collider.gameObject;
            InteractiveItem hoveredItem = hitObject.GetComponent<InteractiveItem>();

            if (hoveredItem != null)
            {    
                if (canvasImage != null)
                {

                    if (hitObject.name == "Blue Potion" || hitObject.name == "Red Potion")
                    {
                        lightText.enabled = true;
                    }
                    else if (hitObject.name == "ResetBtn")
                    {
                        resetText.enabled = true;
                    }
                    else if (hitObject.name == "SpringBtn" || hitObject.name == "SummerBtn" || hitObject.name == "FallBtn" || hitObject.name == "WinterBtn")
                    {
                        if(puzzle.unlocked == 0)
                        {
                            unlockText.enabled = true;
                        }
                        else
                        {
                            lightText.enabled = true;
                        }
                    }
                    else
                    {
                        text.enabled = true;
                    }
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
                text.enabled = false;
                unlockText.enabled = false;
                lightText.enabled = false;
                resetText.enabled = false;

            }
        }
    }   
}
