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
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI PlayerGold;
    public TextMeshProUGUI MonsterName;
    public TextMeshProUGUI MonsterStage;

    public GameObject GameOverUI;
    public GameObject GameClearUI;
    public GameObject SettingUI;

    public Slider monsterHpSlider;
    public TextMeshProUGUI monsterHpText;

    // timer
    float limitTime = 0f;
    float time = 30;
    float timer;

    public int Gold = 0;
    int WUpgradeGold = 10;
    int WSUpgradeGold = 100;
    public int WeaponUpgrade = 0;
    int CurRan = 0;
    public bool isBuff;

    // monster
    Monster monster;
   

    private void Awake()
    {
        EmptyText();
        UIClear();

        monster = FindObjectOfType<Monster>();
        Gold = 1000;
    }
    private void Start()
    {
        monsterHpText.text = monster.monsterCurHP.ToString()+ " / " + monster.monsterMaxHP.ToString();       
        monsterHpSlider.value = monster.monsterCurHP/monster.monsterMaxHP;
        MonsterName.text = monster.monsterName.ToString();
        MonsterStage.text = monster.monsterStage.ToString();

        TimerText.text = monster.monsterLimitTime.ToString();
        PlayerGold.text = Gold.ToString();
    }

    // 주석
    private void Update()
    {

        Invoke("EmptyText", 1f);
        UpdateUIData();
        UpdateTimer();
    }

    void UpdateUIData() 
    {
        monsterHpText.text = monster.monsterCurHP.ToString() + " / " + monster.monsterMaxHP.ToString();
        monsterHpSlider.value = (float)monster.monsterCurHP / (float)monster.monsterMaxHP;

        
        //Debug.Log("hp" + monster.monsterCurHP / monster.monsterMaxHP);


        MonsterName.text = monster.monsterName.ToString();
        MonsterStage.text = monster.monsterStage.ToString();
        PlayerGold.text = Gold.ToString(); // 보상을 얻었을 때만 업데이트 되는 정보
    }

    void UpdateTimer()
    {

        TimerText.text = monster.monsterLimitTime.ToString();

        time -= Time.deltaTime;
        timer = Mathf.Floor(time);

        if (time >= 10)
        {
            TimerText.text = "00" + " : " + timer.ToString();
        }
        else if (time < 10)
        {
            TimerText.text = "00" + " : " + "0" + timer.ToString();
        }
        if (timer <= limitTime)
        {
            Time.timeScale = 0;
            GameOverUI.gameObject.SetActive(true);

            // 타이머가 0이 되면 몬스터의 체력을 최대체력으로 리셋한다.
            monster.monsterCurHP = monster.monsterCurHP;
        }


    }

    void EmptyText()
    {
        AttackDamageText.text = "";
        AutoDamageText.text = "";
    }

    //강화 클릭을하면 웨폰업그레이드 값이 올라감


    public void OnClickWeaponUpgrade()
    {
  
        if(Gold > WUpgradeGold)
        {
            Gold -= WUpgradeGold;
            if (Random.Range(0, 10) >= CurRan)
            {
            ++WeaponUpgrade;
            WUpgradeGold = (int)(WUpgradeGold * 1.1f);
                ++CurRan;
                if (CurRan == 5)
                {
                    CurRan = 0;
                }
            }
            //골드값 빼주기
            
        }
    }

    public void OnClickSpecialWeaponUpgrade()
    {
        if(Gold > WSUpgradeGold)
        {
            Gold -= WSUpgradeGold;
            isBuff = true;
            //버튼파괴
        }
    }

    public void OnClickSettinButton() 
    {
        SettingUI.gameObject.SetActive(true);
    }
    public void OffClickSettinButton()
    {
        SettingUI.gameObject.SetActive(false);
    }

    public void UIClear() 
    {
        GameOverUI.gameObject.SetActive(false);
        GameClearUI.gameObject.SetActive(false);
        SettingUI.gameObject.SetActive(false);
    }

}
