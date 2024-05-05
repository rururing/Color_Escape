using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissors : InteractiveItem
{
    public override void onClick()
    {
        pickUp();
    }
    public override void pickUp()
    {
        //InventoryManager.Instance.Add(Item);
        Destroy(this.gameObject);
    }
}
