using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ApplePlate : InteractiveItem
{
    public GameObject newObject;
    public GameObject disabledObject;
    public KitchenPuzzleManager puzzle;
    public Vector3 newPosition;
    public Text wrongText;
    private bool hasInstantiated = false;
    public int foodOrder = 3;
    public int neededItemId;


    public void Start()
    {
        // 시작 시에 텍스트를 비활성화
        wrongText.gameObject.SetActive(false);
        puzzle = FindObjectOfType<KitchenPuzzleManager>();

    }
    void Update()
    {


    }
    public override void onClick()
    {
        if (InventoryManager.Instance.HasItem(neededItemId)) // 소지하고 있는 아이템이 사과라면
        {
            place(); //접시위에 올린다
          
            if (puzzle != null)
            {
                puzzle.OnFoodPlaced(foodOrder);
            }

            InventoryManager.Instance.RemoveItem(neededItemId);
        }
        else
        {
            wrongText.gameObject.SetActive(true);
            Invoke("HideText", 2.0f);
        }

    }
    private void HideText()
    {
        wrongText.gameObject.SetActive(false);
    }

    public override void place()
    {
        GameObject newObjectInstance = Instantiate(newObject, newPosition, Quaternion.identity);

        // 생성된 오브젝트에 "PlaceableObject" 태그 부여
        newObjectInstance.tag = "PlaceableObject";

        puzzle.currentObjectCount++;
        foodOrder = 0;
        Debug.Log("count" + foodOrder);
        hasInstantiated = true;
    }
}
