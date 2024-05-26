using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sheep : InteractiveItem
{
    public Text lockedText;

    public void Start()
    {
        // 시작 시에 텍스트를 비활성화
        lockedText.gameObject.SetActive(false);
    }

    public override void onClick()
    {
        // lockedText를 활성화
        lockedText.gameObject.SetActive(true);

        // 1초 대기
        StartCoroutine(DisableTextAfterDelay(1.0f));
    }

    private IEnumerator DisableTextAfterDelay(float delay)
    {
        // delay 초 동안 대기
        yield return new WaitForSeconds(delay);
        // 1초가 지난 후에 lockedText를 비활성화
        lockedText.gameObject.SetActive(false);
        pickUp();
        // pickUp 호출

    }
    public override void pickUp()
    {
        InventoryManager.Instance.Add(Item);
        Destroy(this.gameObject);
        Debug.Log("add inventory");

    }

}



    