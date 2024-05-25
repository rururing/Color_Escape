using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedFlask : InteractiveItem
{
    // potionColor 0 : R, 1 : B, 2: Y, 3: C, 4: M, 5: W

    public int potionColor = 0;
    public Flashlight flashlight;
    public Material Potion_Yellow;
    public Material Potion_Magenta;
    public Material Potion_White;



    public Text lockedText;
    public void Start()
    {
        // 시작 시에 텍스트를 비활성화
        lockedText.gameObject.SetActive(false);
    }
    public void makeYellow()
    {
        if (potionColor == 0)
        {
            changeColor(Potion_Yellow);
            potionColor = 2;
        }
        
    }
    public void makeMagenta()
    {
        if (potionColor == 0)
        {
            changeColor(Potion_Magenta);
            potionColor = 4;
        }
    }
    public void makeWhite()
    {
        if (potionColor == 4 || potionColor == 2)
        {
            changeColor(Potion_White);
            potionColor = 5;
        }
    }

    public override void onClick()
    {
        if (potionColor == 2)
        {
            lockedText.gameObject.SetActive(true);
            StartCoroutine(DisableTextAfterDelay(1.0f));
        }
    
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
        // 인벤토리에 데이터 넣고 
       Destroy(this.gameObject);
 
    }

    public override void lightFlashed(int flashLightColor)
    {

        if(potionColor == 0) 
        {
            //Debug.Log("R플라스크에서 후레쉬 색" + flashlight.flashLightColor);
            
            if (flashlight.flashLightColor == 3)
            {
                makeYellow();
            }
            else if (flashlight.flashLightColor == 0)
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
        if (potionColor == 2)
        {
            if (flashlight.flashLightColor == 0)
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
            renderer.material = newMaterial;
        }
    }
}
