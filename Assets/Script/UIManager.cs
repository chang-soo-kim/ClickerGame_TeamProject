using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //
    #region SingleTon

    static public UIManager instance;
    static public UIManager INSTANCE
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
                if (instance == null)
                {
                    instance = new GameObject("UIManager").AddComponent<UIManager>();
                }

            }
            return instance;
        }
    }
    #endregion
    //


    // 데미지 출력 텍스트 - 몬스터 위치에 데미지 값 출력

    public TextMeshProUGUI AutoDamageText;
    public TextMeshProUGUI AttackDamageText;
    public TextMeshProUGUI TimteText;
    public GameObject GameOverUI;
    public GameObject GameClearUI;

    // timer
    float limitTime = 0f;
    float time = 30;
    float timer;


    private void Awake()
    {
        EmptyText();
        GameOverUI.gameObject.SetActive(false);
        GameClearUI.gameObject.SetActive(false);
    }
    private void Start()
    {
        TimteText.text = limitTime.ToString();
    }


    private void Update()
    {
        Invoke("EmptyText", 0.2f);

        time -= Time.deltaTime;
        timer = Mathf.Floor(time);
        TimteText.text = timer.ToString();
        if (timer <= limitTime)
        {
            Time.timeScale = 0;
            GameOverUI.gameObject.SetActive(true);
        }

    }
    void EmptyText()
    {
        AttackDamageText.text = "";
        AutoDamageText.text = "";
    }

}
