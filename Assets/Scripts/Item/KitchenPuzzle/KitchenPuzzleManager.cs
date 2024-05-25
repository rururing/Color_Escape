using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenPuzzleManager : MonoBehaviour
{
    public int requiredObjectCount = 4; // 필요한 오브젝트 개수
    public int currentObjectCount = 0; // 현재 생성된 오브젝트 개수
    private bool isUnlocked = false; // 해금 여부를 확인하는 변수

    private void Update()
    {
            if (currentObjectCount >= requiredObjectCount && !isUnlocked)
            {
                escape();
            }     
    }

    void escape()
    {
        isUnlocked = true;
        
        // 추가적인 해금 로직을 여기에 추가할 수 있습니다.
        Debug.Log("All objects are created. Object unlocked!");
    }
}