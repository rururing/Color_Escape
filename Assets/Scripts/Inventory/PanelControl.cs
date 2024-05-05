using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PanelControl : MonoBehaviour

{
    public GameObject inventoryPanel; // 인벤토리 패널의 GameObject

    void Start()
    {
        // 시작 시에는 인벤토리 패널을 비활성화합니다.
        inventoryPanel.SetActive(false);
    }

    void Update()
    {
        // I 키를 누르면 인벤토리 패널의 활성화/비활성화 상태를 변경합니다.
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventoryPanel();
        }
    }

    void ToggleInventoryPanel()
    {
        // 현재 패널의 활성화 상태를 확인하고 반대로 설정합니다.
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
}
