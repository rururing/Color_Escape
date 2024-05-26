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
        // pickUp 호출

    }

    public override void pickUp()
    {
        InventoryManager.Instance.Add(Item);

        // Renderer 컴포넌트를 비활성화하여 오브젝트를 보이지 않게 만듦
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }

        // Collider 컴포넌트를 비활성화하여 충돌을 방지
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        Debug.Log("add inventory");
 

    }
}
