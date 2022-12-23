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


    // ������ ��� �ؽ�Ʈ - ���� ��ġ�� ������ �� ���

    [Header("�÷��̾� ����")]
    public TextMeshProUGUI AutoDamageText = null;
    public TextMeshProUGUI AttackDamageText = null;
    public TextMeshProUGUI PlayerGold;

    [Header("Ÿ�̸�")]
    public TextMeshProUGUI TimerText = null;

    [Header("���� ����")]
    public Button Monster = null;
    public TextMeshProUGUI MonsterName = null;
    public TextMeshProUGUI MonsterStage = null;
    public Image att_eff = null;

    [Header("Window UI")]
    public GameObject GameOverUI = null;
    public GameObject GameClearUI = null;
    public GameObject SettingUI = null;

    public Slider monsterHpSlider;
    public TextMeshProUGUI monsterHpText = null;

    [Header("����/��ȭ ����")]
    // Wepon
    public GameObject middleUI = null;
    public Button middleButton = null;

    public Image weponimg = null;
    public TextMeshProUGUI weponNameText = null;
    public TextMeshProUGUI weponLevelText = null;
    public TextMeshProUGUI weponPower = null;

    public Image upgradeSuccess = null;
    public Image upgradeFail = null;
    public Image noGold = null;

    //
    public TextMeshProUGUI upgradeCost = null;
    public TextMeshProUGUI upgradePer = null;
    //

    [Header("����")]
    public int ItemNum;
    public int ItemCost;
    public int ItemDamageUP = 1;
    public Button[] PotionItem;

    public Button UpgradeButton = null;
    public Button ShopButton = null;
    public Image ShopWindow = null;

    [Header("���� ����")]
    // Setting
    public Image settingWindow = null;
    public Button settingButton = null;

    public Button BGM_ON_Button = null;
    public Button BGM_OFF_Button = null;
    public Button SE_ON_Button = null;
    public Button SE_OFF_Button = null;

    public Slider SoundBGMSlider = null;
    public Slider effectBGMSlider = null;


    public Button RestartButton = null;
    //������
    public TextMeshProUGUI DMGText = null;
    public GameObject DmageUI = null;

    public Button nextStage = null;
    public Button prevStage = null;

    // timer
    float limitTime = 0f;
    public float time = 30;
    float timer;

    public int Gold = 0;
    int WUpgradeGold = 10;
    
    public int WeaponUpgradeNum = 0;

    public int enforceNum = 0;
    public int bestMonsterNum;

    public float SetOffUpgradeUITime;
    public float UITime = 0f;
    bool OnMiddle = false;

    public bool[] buyPotion = new bool[2] {false,false};
    

    // monster
    Monster monster;
    //
    Player player;

    SoundEffectController soundController;


    private void Awake()
    {

        

        monster = FindObjectOfType<Monster>();
        player = FindObjectOfType<Player>();
        soundController = FindObjectOfType<SoundEffectController>();
        Gold = 1000;
    }
    private void Start()
    {
        monsterHpText.text = monster.monsterCurHP.ToString() + " / " + monster.monsterMaxHP.ToString();
        monsterHpSlider.value = monster.monsterCurHP / monster.monsterMaxHP;
        MonsterName.text = monster.monsterName.ToString();
        MonsterStage.text = monster.monsterStage.ToString();

        TimerText.text = monster.monsterLimitTime.ToString();
        PlayerGold.text = Gold.ToString();

        LiveMonster();
    }

    public int totaldmg;
    private void Update()
    {
       
        totaldmg = (player.WeaponDmg + enforceNum) * ItemDamageUP;
        weponimg.sprite = player.WeaponImg;
        weponNameText.text = "���� : " + player.WeaponName;
        weponLevelText.text = "��ȭ �ܰ� : " + enforceNum.ToString();
        //weponPower.text = "���ݷ� : " + totaldmg.ToString();
        weponPower.text = "������ݷ� : " + player.WeaponDmg + enforceNum + "\n�׸������� : " + ItemDamageUP;

        MiddleMove();
        UpdateUIData();
        UpdateTimer();
        OnupgradeUI();
    }

    void UpdateUIData()
    {
        monsterHpText.text = monster.monsterCurHP.ToString() + " / " + monster.monsterMaxHP.ToString();
        monsterHpSlider.value = (float)monster.monsterCurHP / (float)monster.monsterMaxHP;


        MonsterName.text = monster.monsterName.ToString();
        MonsterStage.text = monster.monsterStage.ToString();
        PlayerGold.text = Gold.ToString() + " G"; // ������ ����� ���� ������Ʈ �Ǵ� ����
    }

    public void UpdateTimer()
    {
        if (monster.IsDead) return;


        TimerText.text = monster.monsterLimitTime.ToString();


        time -= Time.deltaTime;
        timer = Mathf.Floor(time);

        TimerText.text = timer.ToString();
        if (timer <= limitTime)
        {
            DeadPlayer();
            Invoke("RESTART", 1f);
        }
    }

    private void RESTART()
    {
        Time.timeScale = 1;
        soundController.PlaySound("Click"); // ���� ���
        monster.IsDead = false;
        // Ÿ�̸Ӱ� 0�� �Ǹ� ������ ü���� �ִ�ü������ �����Ѵ�.
        monster.monsterCurHP = monster.monsterMaxHP;
    }





    //��ȭ Ŭ���� �ϸ� �������׷��̵� ���� �ö�
    public void OnClickWeaponUpgrade()
    {

        if (Gold >= WUpgradeGold)
        {
            Gold -= WUpgradeGold;
            PlayerGold.text = Gold.ToString() + " G";
            if (Random.Range(0, 10) >= enforceNum)
            {
                SetOffUpgradeUITime = 0;

                upgradeSuccess.gameObject.SetActive(true);
                upgradeFail.gameObject.SetActive(false);
                noGold.gameObject.SetActive(false)
                    ;
                soundController.PlaySound("UpgradeSuccess"); // ���� ���

                WUpgradeGold = (int)(WUpgradeGold * 1.1f);
                //
                upgradeCost.text = "��ȭ ��� : " + WUpgradeGold.ToString() + " G";
                upgradePer.text = "���� Ȯ�� : " + ((10 - enforceNum) * 10).ToString() + " %";
                //
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
                SetOffUpgradeUITime = 0;
                upgradeSuccess.gameObject.SetActive(false);
                upgradeFail.gameObject.SetActive(true);
                noGold.gameObject.SetActive(false);
                soundController.PlaySound("UpgradeFail"); // ���� ���
            }

        }
        else
        {
            SetOffUpgradeUITime = 0;
            upgradeSuccess.gameObject.SetActive(false);
            upgradeFail.gameObject.SetActive(false);
            noGold.gameObject.SetActive(true);
        }
    }

    void OnupgradeUI()
    {
        SetOffUpgradeUITime += Time.deltaTime;
        if (SetOffUpgradeUITime >= 0.5f)
        {
            upgradeSuccess.gameObject.SetActive(false);
            upgradeFail.gameObject.SetActive(false);
            noGold.gameObject.SetActive(false);
            SetOffUpgradeUITime = 0f;
        }

    }

    public void OnClickSettinButton()
    {
        Time.timeScale = 0;
        settingWindow.gameObject.SetActive(true);
        soundController.PlaySound("Click");
    }
    public void OffClickSettinButton()
    {
        Time.timeScale = 1;
        settingWindow.gameObject.SetActive(false);
        soundController.PlaySound("Click");
    }

    public void OnClickShopButton()
    {
        Time.timeScale = 0;
        ShopWindow.gameObject.SetActive(true);
        soundController.PlaySound("Click");
    }
    public void OffClickShopButton()
    {
        Time.timeScale = 1;
        ShopWindow.gameObject.SetActive(false);
        soundController.PlaySound("Click");
    }
    public void OnBuyItemButton()
    {
        
        if (Gold < ItemCost) return; // ��� ���� ǥ��
        if (buyPotion[ItemNum]) return;
        
        Debug.Log("���� ���� ����");


        // �������
        Gold -= ItemCost;
        ItemDamageUP *= 2;

        
        PotionItem[ItemNum].interactable = false; // ������ ��ư ��Ȱ��ȭ - 1���� ����ǰ� ���� ��ư ��Ȱ��ȭ �ȵ�
        buyPotion[ItemNum] = true;
        soundController.PlaySound("Buy"); // ���� ���

    }

    public void UIClear()
    {
        GameOverUI.gameObject.SetActive(false);
        GameClearUI.gameObject.SetActive(true);
    }



    void LiveMonster()
    {
        Monster.interactable = true;
        Time.timeScale = 1;
    }
    public void DeadMonster()
    {
        Monster.interactable = false;
        soundController.PlaySound("MonsterDie"); // ���� ���
    }

    void DeadPlayer()
    {
        Time.timeScale = 0;
        GameOverUI.gameObject.SetActive(true);

    }

    public void HitToMonster()
    {
        monster.rectTransform.localScale = new Vector2(0.9f, 0.9f);

        if (monster.IsDead) return;

        soundController.PlaySound("Attack"); // ���� ���

        monster.monsterCurHP -= totaldmg;

        Instantiate<Image>(att_eff, Input.mousePosition, Quaternion.identity, Monster.transform);
        Instantiate<TextMeshProUGUI>(DMGText, Input.mousePosition, Quaternion.identity, Monster.transform);




        if (monster.monsterCurHP <= 0)
        {
            monster.IsDead = true;
            monster.monsterCurHP = 0;
            GameClearUI.gameObject.SetActive(true); // ���� Ŭ���� ��, ���� ����
            RestartButton.gameObject.SetActive(true);
            // UI & Monster Clear
            UIClear();
            DeadMonster();

            // �÷��̾�� GiveGold ��ŭ ����
            Gold += monster.monsterGiveGold;
            nextStage.gameObject.SetActive(true);
            if(monster.curMonsterNum>=4)
            {
                nextStage.gameObject.SetActive(false);
            }

            if (bestMonsterNum <= monster.curMonsterNum)
            {
                ++bestMonsterNum;
            }

        }
    }

    public void OnClickMiddle()
    {
        if (OnMiddle)
        {
            OnMiddle = false;
        }
        else
        {
            OnMiddle = true;
        }
    }

    void MiddleMove()
    {

        if (OnMiddle)
        {
            if (middleUI.transform.position.x >= 1070f) return;

            middleUI.transform.position = Vector2.Lerp(middleUI.transform.position, new Vector2(1070f, 540f), Time.deltaTime * 2f);
            UITime = 0f;
        }
        else
        {
            if (middleUI.transform.position.x < 450f) return;

            middleUI.transform.position = Vector2.Lerp(middleUI.transform.position, new Vector2(450f, 540f), Time.deltaTime * 2f);
            UITime = 0f;
        }
    }


    public void OnClickRestart()
    {
        Time.timeScale = 1;
        Monster.interactable = true;
        monster.IsDead = false;
        monster.monsterCurHP = monster.monsterMaxHP;
        time = 30;
        GameOverUI.gameObject.SetActive(false);
        GameClearUI.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);


    }



}
