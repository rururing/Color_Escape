using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BananaPlate : InteractiveItem
{
    // Start is called before the first frame update
    public GameObject newObject;
    public GameObject destroyedObject;
    public KitchenPuzzleManager number;
    public Vector3 newPosition;
    public Vector3 newRotation;
    private bool hasInstantiated = false;
    private bool targetDestroyed = false;
    public Text wrongText;

    public void Start()
    {
        // 시작 시에 텍스트를 비활성화
        wrongText.gameObject.SetActive(false);

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
        if (1 == 1) // 소지하고 있는 아이템이 사과라면
        {
            place(); //접시위에 올린다
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
            Instantiate(newObject, newPosition, Quaternion.Euler(newRotation));
            number.currentObjectCount++;
            Debug.Log("count");
            hasInstantiated = true;
        }
    }
}
