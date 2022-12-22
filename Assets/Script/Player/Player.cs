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

        UIManager.INSTANCE.weponNameText.text = "weponNameText";
        UIManager.INSTANCE.weponLevelText.text = "weponLevelText";
        UIManager.INSTANCE.weponPower.text = "weponPower";
        //UIManager.instance.weponNameText.text = weapons[UIManager.instance.WeaponUpgradeNum].WeaponName;
        //UIManager.instance.weponLevelText.text = weapons[UIManager.instance.WeaponUpgradeNum].WeaponDmg.ToString();
        //UIManager.instance.weponPower.text = weapons[UIManager.instance.WeaponUpgradeNum].ToString();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(MousePos, transform.forward, MaxDistance);
            if (hit == false) return;

            Debug.DrawRay(MousePos, transform.forward * 10, Color.red, 0.3f);
            if (hit.collider.CompareTag("Monster") == true)
            {
                if (UIManager.instance.isBuff)
                {
                   // monster.HitToMonster(weapons[UIManager.instance.WeaponUpgradeNum].WeaponDmg * 2 + UIManager.instance.CurRan);
                }
                else
                {
                   // monster.HitToMonster(weapons[UIManager.instance.WeaponUpgradeNum].WeaponDmg + UIManager.instance.CurRan);
                }
                if (hit)
                {
                    hit.transform.GetComponent<SpriteRenderer>().color = Color.red;
                    if (Input.GetMouseButtonUp(0))
                    {
                        hit.transform.GetComponent<SpriteRenderer>().color = Color.clear;
                    }
                }
            }

        }
    }


}
