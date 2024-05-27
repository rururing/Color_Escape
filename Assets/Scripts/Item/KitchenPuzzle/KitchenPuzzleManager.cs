using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KitchenPuzzleManager : MonoBehaviour
{
    public int requiredObjectCount = 4; // 필요한 오브젝트 개수
    public int currentObjectCount = 0; // 현재 생성된 오브젝트 개수
    private int isUnlocked = 0; // 해금 여부를 확인하는 변수

    public string tagToDestroy = "PlaceableObject";

    public Text failText;
    public Text escapeText;

    public GameObject EndPanel;

    private int[] correctSequence = { 0, 1, 2, 3 };

    // 현재까지 입력된 버튼들의 ID를 저장하는 배열
    private int[] inputSequence = new int[4];
    private int currentIndex = 0; // 현재 입력된 버튼의 인덱스

    public void Start()
    {
        // 시작 시에 텍스트를 비활성화
        failText.gameObject.SetActive(false);
        escapeText.gameObject.SetActive(false);
        EndPanel.SetActive(false);

    }

    private void HideText()
    {
        failText.gameObject.SetActive(false);
        escapeText.gameObject.SetActive(false);
    }


    // place 메소드를 통해 생성된 모든 게임 오브젝트 제거
    public void DestroyObjects()
    {
        // 지정된 태그를 가진 모든 게임 오브젝트를 찾아서 파괴
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tagToDestroy);
        foreach (GameObject obj in objectsWithTag)
        {
            Destroy(obj);
        }
        // 현재 생성된 오브젝트 수 초기화
        currentObjectCount = 0;
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
        StartCoroutine(DisableTextAfterDelay(3.0f));

        // 추가적인 해금 로직을 여기에 추가할 수 있습니다.
    }
    private IEnumerator DisableTextAfterDelay(float delay)
    {
        // delay 초 동안 대기
        yield return new WaitForSeconds(delay);
        escapeText.gameObject.SetActive(false);
        AudioManager.Instance.PlaySFX("Button");
        EndPanel.SetActive(true);

    }

    public void CloseEndPanel()
    {
        // 도움말 창을 닫음
        AudioManager.Instance.PlaySFX("Close");
        SceneManager.LoadScene("StartScene");
    }


}