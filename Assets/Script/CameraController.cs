using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Monster monster;

    float MaxDistance = 1000f;
    Vector3 MousePos;
    Camera cam;
    int attackPower = 10;

    private void Awake()
    {

    }

    void Start()
    {
        cam = GetComponent<Camera>();
        monster = FindObjectOfType<Monster>();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            MousePos = Input.mousePosition;
            MousePos = cam.ScreenToWorldPoint(MousePos);

            RaycastHit2D hit = Physics2D.Raycast(MousePos, transform.forward, MaxDistance);
            Debug.DrawRay(MousePos, transform.forward * 10, Color.red, 0.3f);

            monster.HitToMonster(attackPower);

            if (hit)
            {
                hit.transform.GetComponent<SpriteRenderer>().color = Color.red;
                if (Input.GetMouseButtonUp(0))
                {
                    hit.transform.GetComponent<SpriteRenderer>().color = Color.clear;
                }
            }

        }
        /*else if (Input.GetMouseButtonUp(0))
        {
            hit.transform.GetComponent<SpriteRenderer>().color = Color.clear;
        }*/
    }
}
