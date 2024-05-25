using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ApplePlate : InteractiveItem
{
    public GameObject newObject;
    public GameObject destroyedObject;
    public KitchenPuzzleManager puzzle;
    public Vector3 newPosition;
    private bool hasInstantiated = false;
    private bool targetDestroyed = false;
    public Text wrongText;

    public int foodOrder = 3;


    public void Start()
    {
        // 시작 시에 텍스트를 비활성화
        wrongText.gameObject.SetActive(false);
        puzzle = FindObjectOfType<KitchenPuzzleManager>();

    }
    void Update()
    {
        // 타겟 오브젝트가 파괴되었는지 체크
        if (destroyedObject == null)
        {
            targetDestroyed = true;
        }

       
    }
    public override void onClick()
    {
        if (1==1) // 소지하고 있는 아이템이 사과라면
        {
            place(); //접시위에 올린다

            if (puzzle != null)
            {
                puzzle.OnFoodPlaced(foodOrder);
            }
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
        if (!hasInstantiated && targetDestroyed)
        {
            Instantiate(newObject, newPosition, Quaternion.identity);
            puzzle.currentObjectCount++;
            foodOrder = 0;
            Debug.Log("count" + foodOrder);
            hasInstantiated = true;
        }
    }
}
