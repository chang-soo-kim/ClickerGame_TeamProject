using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int ClickCount=0;
    // 플레이어가 클릭시 데미지 전달

    // 클릭안해도 데미지 전달
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;

    SpriteRenderer sr;
    public GameObject go;

    private void Awake()
    {
        sr = go.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
            
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.gameObject);
                if (hit.transform.gameObject.tag == "Monster")
                {
                    ClickCount++;
                    sr.material.color = Color.red;
                    // 충돌한 객체 색상 변경
                }
            }
        }
        
    }
}
