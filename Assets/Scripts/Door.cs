using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Door : InteractiveItem
{
    public Text lockedText;
    public DoorManager door;
    public void Start()
    {
        // 시작 시에 텍스트를 비활성화
        lockedText.gameObject.SetActive(false); 
    }
    public override void onClick()
    {
        if(door.keyobtained == 1) // 키를 가지고있으면
        {
            SceneManager.LoadScene("Stage2");
            // 인벤토리에서 열쇠 파괴
        }
        else
        {
            lockedText.gameObject.SetActive(true);
            Invoke("HideText", 2.0f);
        }
    }

    private void HideText()
    {
        lockedText.gameObject.SetActive(false);
    }

}
