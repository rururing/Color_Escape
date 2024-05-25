using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Banana : InteractiveItem
{
    public Text lockedText;

    void Start()
    {
        lockedText.gameObject.SetActive(false);
    }
    public override void onClick()
    {

        lockedText.gameObject.SetActive(true);
        StartCoroutine(DisableTextAfterDelay(1.0f));


    }
    private IEnumerator DisableTextAfterDelay(float delay)
    {

        yield return new WaitForSeconds(delay);

        lockedText.gameObject.SetActive(false);
        pickUp();
        // pickUp 호출

    }

    public override void pickUp()
    {
        // 인벤토리에 데이터 넣고 
        Destroy(this.gameObject);

    }
}
