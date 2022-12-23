using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData[] itemData;


    // ���� ����

    // ���õ� ��ư�� ������ 1���� ��
    public void OnSelectItem1()
    {
        Debug.Log("1�� ���� ����");
        GetItemData(0);
        // soundController.PlaySound("Click"); // ���� ���
    }

    // ���õ� ��ư�� ������ 2���� ��
    public void OnSelectItem2()
    {
        Debug.Log("2�� ���� ����");
        GetItemData(1);
        // soundController.PlaySound("Click"); // ���� ���
    }
    void GetItemData(int num)
    {
        UIManager.INSTANCE.ItemNum = num;
        UIManager.INSTANCE.ItemCost = itemData[num].Cost; 
        
    }
    
}
