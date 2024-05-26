using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherPuzzleManager : MonoBehaviour
{
    public int unlocked = 0;
    public int neededItemId;

    public Text lockedText;
    private bool isTextShown = false; // 텍스트가 한 번 나타났는지 확인하기 위한 변수

    private void Start()
    {
        lockedText.gameObject.SetActive(false);
    }

    private void Update()
    {
        // 인벤토리에 필요한 아이템이 있는지 확인
        CheckFor();
    }

    public void CheckFor()
    {
        // InventoryManager의 Instance를 통해 인벤토리에 열쇠가 있는지 확인
        if (InventoryManager.Instance.HasItem(neededItemId) && unlocked == 0)
        {
            unlocked = 1;
            ShowText();
        }
    }

    private void ShowText()
    {
        if (!isTextShown)
        {
            lockedText.gameObject.SetActive(true);
            isTextShown = true;
            Invoke("HideText", 2.0f);
        }
    }

    private void HideText()
    {
        lockedText.gameObject.SetActive(false);
    }
}
