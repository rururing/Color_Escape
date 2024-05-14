using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SecretPassword : MonoBehaviour
{
    public Text lockedText1;
    public Text lockedText2;
    public Text lockedText3;
    private WeatherPuzzleManager weatherPuzzleManager;
    public int unlocked = 0;

    // 올바른 순서 배열
    private int[] correctSequence = {1, 2, 3, 4};

    // 현재까지 입력된 버튼들의 ID를 저장하는 배열
    private int[] inputSequence = new int[4];
    private int currentIndex = 0; // 현재 입력된 버튼의 인덱스


    public void Start()
    {
        // 시작 시에 텍스트를 비활성화
        lockedText1.gameObject.SetActive(false);
        lockedText2.gameObject.SetActive(false);
        lockedText3.gameObject.SetActive(false);
        weatherPuzzleManager = FindObjectOfType<WeatherPuzzleManager>();

    }

    private void HideText()
    {
        lockedText1.gameObject.SetActive(false);
        lockedText2.gameObject.SetActive(false);
        lockedText3.gameObject.SetActive(false);
    }

    // 버튼이 눌렸을 때 호출되는 함수
    public void OnButtonPressed(int btnId)
    {
        // 현재 입력된 버튼의 ID를 배열에 저장
        if (unlocked == 0)
        {
            inputSequence[currentIndex] = btnId;
            currentIndex++;


            if (currentIndex == 4)
            {
                if (IsInputSequenceCorrect())
                {
                    Unlock();
                    return;
                }
                // 입력이 잘못되었을 때는 시퀀스 초기화
                currentIndex = 0;
                Debug.Log("set 0");
                // 텍스트를 활성화하여 상자를 열지 못한다는 메시지를 표시
                lockedText1.gameObject.SetActive(true);
                // 2초 후에 비활성화되도록 Invoke() 호출
                Invoke("HideText", 2.0f);
            }
        }
        else if (unlocked == 1)
        {
            // 텍스트를 활성화하여 상자를 열지 못한다는 메시지를 표시
            lockedText3.gameObject.SetActive(true);
            // 2초 후에 비활성화되도록 Invoke() 호출
            Invoke("HideText", 2.0f);
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

    // 비밀번호를 해금하는 함수
    private void Unlock()
    {
        Debug.Log("Password unlocked!");
        lockedText2.gameObject.SetActive(true);
        Invoke("HideText", 2.0f);
        unlocked = 1;
    }
}