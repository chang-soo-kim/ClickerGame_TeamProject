using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DmgText : MonoBehaviour
{
    private float moveSpeed;
    private float alphaSpeed;
    
    public TextMeshProUGUI text;
    Color alpha;


    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        moveSpeed = 30.0f;
        alphaSpeed = 2.0f;

        alpha = text.color;

        //alpha = text.color;
        text.text = "-" + UIManager.INSTANCE.totaldmg.ToString();

        Destroy(gameObject,2f);
    }

    
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // �ؽ�Ʈ ��ġ
        
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // �ؽ�Ʈ ���İ�
        text.color = alpha;

        //text.color = alpha;
    }


}