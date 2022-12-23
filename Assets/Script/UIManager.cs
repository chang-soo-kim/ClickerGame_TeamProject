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

    public System.Action action;

    // ������ ��� �ؽ�Ʈ - ���� ��ġ�� ������ �� ���

    public TextMeshProUGUI AutoDamageText = null;
    public TextMeshProUGUI AttackDamageText= null;
    public TextMeshProUGUI TimteText= null;

    public TextMeshProUGUI weponNameText = null;
    public TextMeshProUGUI weponLevelText = null;
    public TextMeshProUGUI weponPower = null;


    public GameObject GameOverUI = null;
    public GameObject GameClearUI = null;

    public Slider monsterHpSlider = null;
    public TextMeshProUGUI monsterHpText = null;

    public TextMeshProUGUI goldText = null;
    public Button enforceButton = null;
    public Button smithy = null;

    public Button settingButton = null;
    public Button gameEndButton = null;
    public Image settingWindow = null;

    public Slider backGroundBGMSlider = null;
    public Slider effBGMSlider = null;
    

    // timer
    float limitTime = 0f;
    float time = 31;
    float timer;
    int Gold = 1000;
    int WUpgradeGold = 10;
    int WSUpgradeGold = 100;
    public int WeaponUpgradeNum = 0;
    public int CurRan = 0;
    public bool isBuff = false;

    // monster
    Monster monster;

    private void Awake()
    {
        EmptyText();
        GameOverUI.gameObject.SetActive(false);
        GameClearUI.gameObject.SetActive(false);
        goldText.text = Gold.ToString();
        monster = FindObjectOfType<Monster>();
    }
    private void Start()
    {
        TimteText.text = limitTime.ToString();
        monsterHpSlider.value = monster.monsterMaxHP;
        monsterHpText.text = monster.monsterHP.ToString()+ " / " + monster.monsterMaxHP.ToString();       
        
    }

    // �ּ�
    private void Update()
    {

        Invoke("EmptyText", 0.2f);

        time -= Time.deltaTime;
        timer = Mathf.Floor(time);

        if (time >= 10)
        {
            TimteText.text = "00" + " : " + timer.ToString();
        }
        else if (time < 10)
        {
            TimteText.text = "00" + " : " + "0" + timer.ToString();
        }
        if (timer <= limitTime) 
        {
            Time.timeScale = 0;
            GameOverUI.gameObject.SetActive(true);

            // Ÿ�̸Ӱ� 0�� �Ǹ� ������ ü���� �ִ�ü������ �����Ѵ�.
            monster.monsterHP = monster.monsterMaxHP;
        }
    }

    private void FixedUpdate()
    {
        monsterHpSlider.value = monster.monsterHP;
    }

    void EmptyText()
    {
        AttackDamageText.text = "";
        AutoDamageText.text = "";
    }




    public void OnClickWeaponUpgrade()
    {
  
        if(Gold > WUpgradeGold)
        {
            Gold -= WUpgradeGold;
            goldText.text = Gold.ToString();
            if (Random.Range(0, 10) >= CurRan)
            {
                Debug.Log("��ȭ ����" + CurRan);
                
                WUpgradeGold = (int)(WUpgradeGold * 1.1f);
                ++CurRan;
                if (CurRan == 10)
                {
                    ++WeaponUpgradeNum;
                    action();

                    CurRan = 0;
                }
            }
            else
            {
                Debug.Log("��ȭ ����");
            }
            
        }
        else
        {
            Debug.Log("��尡 �����մϴ�.");
        }
    }

    public void OnClickSpecialWeaponUpgrade()
    {
        if(Gold > WSUpgradeGold && isBuff == false)
        {
            Gold -= WSUpgradeGold;
            goldText.text = Gold.ToString();
            isBuff = true;
            //��ư�ı�
        }
    }


}
