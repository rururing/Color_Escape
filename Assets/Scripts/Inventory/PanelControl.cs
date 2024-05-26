using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



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
            AudioManager.Instance.PlaySFX("inventory");
            inventoryPanel.SetActive(true);
            InventoryManager.Instance.ListItems();
        }
    }

    public void CloseInventory()
    {
        // 도움말 창을 닫음
        AudioManager.Instance.PlaySFX("Close");
        inventoryPanel.SetActive(false);
    }



}
