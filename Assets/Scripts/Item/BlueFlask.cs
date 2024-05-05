using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFlask : InteractiveItem
{
    // potionColor 0 : R, 1 : B, 2: Y, 3: C, 4: M, 5: W

    public int potionColor = 1;
    public Flashlight flashlight;
    public Material Potion_Cyan;
    public Material Potion_Magenta;
    public Material Potion_White;


    public void makeCyan()
    {
        if (potionColor == 1)
        {
            changeColor(Potion_Cyan);
            potionColor = 3;
        }
    }
    public void makeMagenta()
    {
        if (potionColor == 1)
        {
            changeColor(Potion_Magenta);
            potionColor = 4;
        }
    }
    public void makeWhite()
    {
        if (potionColor == 4 || potionColor == 3)
        {
            changeColor(Potion_White);
            potionColor = 5;
        }
    }


    public override void onClick()
    {
        if (potionColor == 3)
            pickUp();
    }

    public override void pickUp()
    {
        // 인벤토리에 데이터 넣고 
        Destroy(this.gameObject);
    }

    public override void lightFlashed(int flashLightColor)
    {

        if (potionColor == 1)
        {
            //Debug.Log("B플라스크에서 후레쉬 색" + flashlight.flashLightColor);

            if (flashlight.flashLightColor == 3)
            {
                makeCyan();
            }
            else if (flashlight.flashLightColor == 2)
            {
                makeMagenta();
            }
        }
        if (potionColor == 4)
        {
            if (flashlight.flashLightColor == 3)
            {
                makeWhite();
            }
        }
        if (potionColor == 3)
        {
            if (flashlight.flashLightColor == 2)
            {
                makeWhite();
            }
        }

    }

    public void changeColor(Material newMaterial)
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            // Assign the new material to the renderer
            renderer.material = newMaterial;
        }
    }
}
