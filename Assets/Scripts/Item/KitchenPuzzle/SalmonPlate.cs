using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SalmonPlate : InteractiveItem
{
    public GameObject newObject;
    public GameObject destroyedObject;
    public KitchenPuzzleManager puzzle;
    public Vector3 newPosition;
    public Text wrongText;
    private bool hasInstantiated = false;

    public Flashlight flashlight;
    public Material Red_Plate;

    public int foodOrder = 0;
    public int neededItemId;

    public void Start()
    {
        // 시작 시에 텍스트를 비활성화
        wrongText.gameObject.SetActive(false);


    }
    void Update()
    {


    }
    public override void onClick()
    {
        if (InventoryManager.Instance.HasItem(neededItemId)) // 연어가 인벤토리에 있다면
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
        foodOrder = 2;
        Debug.Log("count" + foodOrder);
        hasInstantiated = true;
    }

    public override void lightFlashed(int flashLightColor)
    {
        if (flashlight.flashLightColor == 3) // 초록색
        {
            changeColor(Red_Plate);
        }

    }

    public void changeColor(Material newMaterial)
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            renderer.material = newMaterial;
        }
    }

}
