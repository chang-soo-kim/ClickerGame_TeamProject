using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Monster monster;
    [SerializeField]
    Weapon[] weapons;

    // 레이캐스트 최대거리
    float MaxDistance = 1000f;
    // 내 마우스
    Vector3 MousePos;
    // 내 카메라 
    Camera cam;

    void Start()
    {
        cam = Camera.main;
        monster = FindObjectOfType<Monster>();

        //UIManager.INSTANCE.weponNameText.text = "weponNameText";
        //UIManager.INSTANCE.weponLevelText.text = "weponLevelText";
        //UIManager.INSTANCE.weponPower.text = "weponPower";
        UIManager.instance.weponNameText.text = weapons[UIManager.instance.WeaponUpgradeNum].WeaponName;
        UIManager.instance.weponLevelText.text = weapons[UIManager.instance.WeaponUpgradeNum].WeaponDmg.ToString();
        UIManager.instance.weponPower.text = weapons[UIManager.instance.WeaponUpgradeNum].ToString();
    }

    void Update()
    {

    }
    public void OnAttack()
    {
        // monsterCurHP -= damage;
        UIManager.instance.weponPower.text = weapons[UIManager.instance.WeaponUpgradeNum].ToString();
    }



}
