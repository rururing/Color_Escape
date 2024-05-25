using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveItem : MonoBehaviour
{
    public int Id = 0;
    public Item Item;

    public virtual void onClick() { }

    public virtual void pickUp() { }

    public virtual void press() { }

    public virtual void open() { }

    public virtual void place() { }

    public virtual void lightFlashed (int flashLightColor) { }

}
