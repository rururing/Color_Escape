using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


enum state
{
    none, // 아무것도없음
    one_selected, // 하나선택
    two_selected, // 서로다른_둘_선택
    one_doubleclicked, // 같은하나_두번클릭
}


public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<ItemSO> Items = new List<ItemSO>();
    public List<Vector3Int> combineTable;
    public List<ItemSO> CombinedResultItems;
    public Transform ItemContent;
    public GameObject InventoryItem; // 프리팹

    public Text lockedText;


    public void Start()
    {
        // 시작 시에 텍스트를 비활성화
        lockedText.gameObject.SetActive(false);
    }

    private void Awake()
    {
        Instance = this;
    }
    public void Add(ItemSO item)
    {
        Items.Add(item);
    }
    public void Remove(ItemSO item)
    {
        Items.Remove(item);
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            obj.GetComponent<Item>().thisItem = item;

            var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemDescription = obj.transform.Find("ItemDescription").GetComponent<Text>();

            itemName.text = item.itemName;
            itemDescription.text = item.itemDescription;
            itemIcon.sprite = item.icon;

        }
    }

    state nowState = state.none;

    ItemSO rememberADDItemSO;

    int inhand = -1;  // -1 == 손에 아무것도 없음 

    public void SelectItem(ItemSO itemS0)
    {
        Debug.Log(nowState);

        //아직 선택한 물건이 없을 때
        if (nowState == state.none)
        {
            nowState = state.one_selected;
            rememberADDItemSO = itemS0;
        }

        //선택한 물건이 있을 때
        else if (nowState == state.one_selected)
        {
            if (itemS0.id == rememberADDItemSO.id)
            {
                nowState = state.one_doubleclicked;
                // TODO 아이템을 손에 들고있게 한 후 
                inhand = itemS0.id;

                nowState = state.none;
                Debug.Log(nowState);

            }
            else
            {
                nowState = state.two_selected;
                Debug.Log(nowState);

                int combine = checkCombine(rememberADDItemSO.id, itemS0.id); // -1 이라면 조합 불가능 이라고 하자!
                                                                    // Table 에 가서 rememberAdd + itemS0.ID 조합식이 있나?
                if(combine == -1)
                {
                    Debug.Log("조합불가능");
                    //조합할 수 없는 아이템입니다 팝업창
                    lockedText.gameObject.SetActive(true);
                    // 2초 후에 비활성화되도록 Invoke() 호출
                    Invoke("HideText", 2.0f);
                }
                else if (combine != -1) // 조합이 가능할 때
                {
                    // 손에 들고있던 물건 빠지는 경우의 수 ((손에들고있는물건 == rememberAdd || 손에들고있는물건 == itemS0.ID) && 이둘은조합가능(rememberAdd, itemS0.ID))
                    if (inhand != -1)
                    {
                        if (inhand == rememberADDItemSO.id || inhand == itemS0.id)
                        {
                            // 손에서 빼준다.
                            inhand = -1;
                        }
                    }
                    // 조합에서 사용한 아이템 삭제
                    Items.Remove(rememberADDItemSO);
                    Items.Remove(itemS0);
                    rememberADDItemSO = null;

                    // 조합된 아이템 추가
                    Items.Add(getItemByIndex(combine));
                    ListItems();

                }

                nowState = state.none;

            }
        }


        int checkCombine(int item1Id, int item2Id)
        {
            for (int i = 0; i < combineTable.Count; i++)
            {
                if (combineTable[i].x == item1Id && combineTable[i].y == item2Id)
                {
                    return combineTable[i].z;
                }
            }
            return -1;

        }

        ItemSO getItemByIndex(int itemIndex)
        {
            foreach(ItemSO itemSO in CombinedResultItems)
            {
                if(itemSO.id == itemIndex)
                {
                    return itemSO;
                }
            }
            return null;
        }

    }
    private void HideText()
    {
        lockedText.gameObject.SetActive(false);
    }
}
