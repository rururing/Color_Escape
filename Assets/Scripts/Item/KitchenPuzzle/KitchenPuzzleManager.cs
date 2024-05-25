using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenPuzzleManager : MonoBehaviour
{
    public int requiredObjectCount = 4; // 필요한 오브젝트 개수
    public int currentObjectCount = 0; // 현재 생성된 오브젝트 개수
    private int isUnlocked = 0; // 해금 여부를 확인하는 변수

    public GameObject[] objectsToReset;

    public Text failText;
    public Text escapeText;

    private int[] correctSequence = { 0, 1, 2, 3 };

    // 현재까지 입력된 버튼들의 ID를 저장하는 배열
    private int[] inputSequence = new int[4];
    private int currentIndex = 0; // 현재 입력된 버튼의 인덱스

    public void Start()
    {
        // 시작 시에 텍스트를 비활성화
        failText.gameObject.SetActive(false);
        escapeText.gameObject.SetActive(false);

    }

    private void HideText()
    {
        failText.gameObject.SetActive(false);
        escapeText.gameObject.SetActive(false);
    }


    public void OnFoodPlaced(int foodOrder)
    {
        // 현재 입력된 버튼의 ID를 배열에 저장
        if (isUnlocked == 0)
        {
            inputSequence[currentIndex] = foodOrder;
            Debug.Log(foodOrder);
            currentIndex++;


            if (currentIndex == 4)
            {
                if (IsInputSequenceCorrect())
                {
                    isUnlocked = 1;
                    escape();
                    return;
                }
                // 입력이 잘못되었을 때는 시퀀스 초기화
                currentIndex = 0;
                Debug.Log("set 0");
                failText.gameObject.SetActive(true);
                // 2초 후에 비활성화되도록 Invoke() 호출
                Invoke("HideText", 2.0f);
               
                foreach (GameObject obj in objectsToReset)
                {
                    // 물체들 삭제하기

                }
                // 인벤토리에 사라졌던 물체들 다시 넣기


            }
        }
    }

    // 입력된 시퀀스가 올바른 비밀번호와 일치하는지 확인하는 함수
    private bool IsInputSequenceCorrect()
    {
        for (int i = 0; i < correctSequence.Length; i++)
        {
            if (inputSequence[i] != correctSequence[i])
            {
                return false;
            }
        }
        return true;
    }

  

    void escape()
    {
        escapeText.gameObject.SetActive(true);
        Invoke("HideText", 4.0f);
        // 추가적인 해금 로직을 여기에 추가할 수 있습니다.
        Debug.Log("All objects are created. Object unlocked!");
    }
}