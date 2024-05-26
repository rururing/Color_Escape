using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemSO thisItem;

    public void OnClickItem()
    {
        Debug.Log("selectItem");
        InventoryManager.Instance.SelectItem(thisItem);
        Debug.Log("selectItem");
    }
}
