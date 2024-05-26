using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Salmon : InteractiveItem
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
        // pickUp »£√‚

    }

    public override void pickUp()
    {
        InventoryManager.Instance.Add(Item);
        Destroy(this.gameObject);
        Debug.Log("add inventory");

    }
}
