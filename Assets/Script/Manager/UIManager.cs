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

    [Header("플레이어 정보")]
    public TextMeshProUGUI AutoDamageText = null;
    public TextMeshProUGUI AttackDamageText = null;
    public TextMeshProUGUI PlayerGold;

    [Header("타이머")]
    public TextMeshProUGUI TimerText = null;    

    [Header("몬스터 정보")]
    public TextMeshProUGUI MonsterName;
    public TextMeshProUGUI MonsterStage;

    [Header("Window UI")]
    public GameObject GameOverUI = null;
    public GameObject GameClearUI = null;
    public GameObject SettingUI = null;

    public Slider monsterHpSlider;
    public TextMeshProUGUI monsterHpText=null;

    [Header("무기/강화 정보")]
    // Wepon
    public Image weponimg = null;
    public TextMeshProUGUI weponNameText = null;
    public TextMeshProUGUI weponLevelText = null;
    public TextMeshProUGUI weponPower = null;

    public TextMeshProUGUI upgradCost = null;
    public TextMeshProUGUI upgradePer = null;

    // public TextMeshProUGUI goldText = null;
    public Button UpgradeButton = null;
    public Button ShopButton = null;
    public Image ShopWindow = null;

    [Header("설정 정보")]
    // Setting
    public Image settingWindow = null;
    public Button settingButton = null;
    // public Button gameEndButton = null;

    public Slider SoundBGMSlider = null;
    public Slider effectBGMSlider = null;

    // timer
    float limitTime = 0f;
    float time = 30;
    float timer;

    public int Gold = 0;
    int WUpgradeGold = 10;
    int WSUpgradeGold = 100;
    public int WeaponUpgradeNum = 0;

    public int enforceNum = 0;
    public bool isBuff;

    // monster
    Monster monster;
    Player player;
   

    private void Awake()
    {
        EmptyText();
        UIClear();

        monster = FindObjectOfType<Monster>();
        player = FindObjectOfType<Player>();
        Gold = 1000;
    }
    private void Start()
    {
        monsterHpText.text = "123";
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
                
        Debug.Log("hp" + monster.monsterCurHP / monster.monsterMaxHP);

        MonsterName.text = monster.monsterName.ToString();
        MonsterStage.text = monster.monsterStage.ToString();
        PlayerGold.text = Gold.ToString() + " G"; // 보상을 얻었을 때만 업데이트 되는 정보
    }

    void UpdateTimer()
    {

        TimerText.text = monster.monsterLimitTime.ToString();

        time -= Time.deltaTime;
        timer = Mathf.Floor(time);

        TimerText.text =  timer.ToString();
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

        if (Gold > WUpgradeGold)
        {
            Gold -= WUpgradeGold;
            PlayerGold.text = Gold.ToString() + " G";
            if (Random.Range(0, 10) >= enforceNum)
            {
                Debug.Log("강화 성공" + enforceNum);

                WUpgradeGold = (int)(WUpgradeGold * 1.1f);
                upgradCost.text ="강화 비용 : " + WUpgradeGold.ToString() + " G";
                upgradePer.text = "성공 확률 : " + ((10 - enforceNum) * 10).ToString() + " %";
                ++enforceNum;
                if (enforceNum == 10)
                {
                    ++WeaponUpgradeNum;
                    player.WeponLvUP();
                    enforceNum = 0;
                }
            }
            else
            {
                Debug.Log("강화 실패");
            }

            weponimg.sprite = player.WeaponImg;
            weponNameText.text = "장비명 : " + player.WeaponName;
            weponLevelText.text = "강화 단계 : " + enforceNum.ToString();
            weponPower.text = "공격력 : " + (player.WeaponDmg + enforceNum).ToString();

        }
        else
        {
            Debug.Log("골드가 부족합니다.");
        }
    }

    public void OnClickSpecialWeaponUpgrade()
    {
        if (Gold > WSUpgradeGold && isBuff == false)
        {
            Gold -= WSUpgradeGold;
            PlayerGold.text = Gold.ToString()+ " G";
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

    public void OnClickShopButton() 
    {
        ShopWindow.gameObject.SetActive(true);
    }
    public void OffClickShopButton()
    {
        ShopWindow.gameObject.SetActive(false);
    }

    public void UIClear() 
    {
        GameOverUI.gameObject.SetActive(false);
        GameClearUI.gameObject.SetActive(false);
        SettingUI.gameObject.SetActive(false);
    }

    public void HitToMonster()
    {
        if (monster.monsterCurHP <= 0) return;
        

        monster.monsterCurHP -= player.WeaponDmg + enforceNum;

        AttackDamageText.text = (player.WeaponDmg + enforceNum).ToString();

        if (monster.monsterCurHP <= 0)
        {
            monster.monsterCurHP = 0;   
            GameClearUI.gameObject.SetActive(true); // 게임 클리어 시, 게임 멈춤

            // UI & Monster Clear
            UIClear();

            // 플레이어에게 GiveGold 만큼 전달
            Gold += monster.monsterGiveGold;

            // 3초 뒤에 새로운 몬스터 생성
            Invoke("NextMonster", 10f);

        }
    }
}
