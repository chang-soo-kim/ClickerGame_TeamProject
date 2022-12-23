using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData[] itemData;


    // 물약 선택

    // 선택된 버튼의 정보가 1번일 때
    public void OnSelectItem1()
    {
        Debug.Log("1번 물약 선택");
        GetItemData(0);
        // soundController.PlaySound("Click"); // 사운드 출력
    }

    // 선택된 버튼의 정보가 2번일 때
    public void OnSelectItem2()
    {
        Debug.Log("2번 물약 선택");
        GetItemData(1);
        // soundController.PlaySound("Click"); // 사운드 출력
    }
    void GetItemData(int num)
    {
        UIManager.INSTANCE.ItemNum = num;
        UIManager.INSTANCE.ItemCost = itemData[num].Cost;
    }
    
}
