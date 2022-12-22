using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    Monster monster;
    public MonsterData[] monsterData = null;

    public string monsterName { get; set; }
    public int monsterStage { get; set; }
    public int monsterCurHP { get; set; }
    public int monsterMaxHP { get; set; }
    public int monsterGiveGold { get; set; }
    public float monsterLimitTime { get; set; }
    public SpriteRenderer monsterImage { get; set; }
    public bool IsDead { get; set; }

    int autoDamage { get; set; }

    int curMonsterNum;
    int prevMonsterNum;
    int nextMonsterNum;

    bool isNewMonster { get; set; }

    private void Awake()
    {
        monsterImage = GetComponent<SpriteRenderer>();

        curMonsterNum = 0;


        monsterName = monsterData[curMonsterNum].Name;

        monsterCurHP = monsterData[curMonsterNum].HP;
        monsterMaxHP = monsterData[curMonsterNum].HP;

        monsterStage = monsterData[curMonsterNum].Stage;

        monsterGiveGold = monsterData[curMonsterNum].GiveGold;

        monsterLimitTime = monsterData[curMonsterNum].LimitTime;

        monsterImage.sprite = monsterData[curMonsterNum].Image;

        autoDamage = 1;

        IsDead = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        isNewMonster = false;

    }
    float time = 0f;

    // Update is called once per frame
    void Update()
    {
        // 1초마다 1씩 공격
        time += Time.deltaTime;
        if (time >= 1f)
        {
            monsterCurHP -= autoDamage;
            UIManager.INSTANCE.AutoDamageText.text = autoDamage.ToString();
            time = 0f;
        }

    }

    public void HitToMonster(int damage)
    {
        Debug.Log(damage);

        monsterCurHP -= damage;

        UIManager.INSTANCE.AttackDamageText.text = damage.ToString();

        if (monsterCurHP <= 0)
        {
            UIManager.INSTANCE.GameClearUI.gameObject.SetActive(true); // 게임 클리어 시, 게임 멈춤

            // UI & Monster Clear
            ClearUI();

            // 플레이어에게 GiveGold 만큼 전달
            UIManager.INSTANCE.Gold += monsterGiveGold;

            // 3초 뒤에 새로운 몬스터 생성
            Invoke("NextMonster", 10f);

        }
    }

    
    // 몬스터 죽이고 나면 UI 창 남아있고, 몬스터 색상 안바뀐채로 다시 나옴. 이전 몬스터 안 사라짐
    void ClearUI()
    {
        UIManager.INSTANCE.UIClear();
    }
    


    public void CurMonster()
    {
        isNewMonster = false;
        NewMonster(curMonsterNum);
    }
    public void NextMonster()
    {
        curMonsterNum += 1;
        isNewMonster = true;
        NewMonster(curMonsterNum);
    }

    public void PrevMonster()
    {
        curMonsterNum -= 1;
        isNewMonster = true;
        NewMonster(curMonsterNum);
    }

    void NewMonster(int num)
    {
        if (num <= 0)
        {
            num = 0;
        }
        if (num >= 4)
        {
            num = 4;
        }
        Debug.Log("num" + num);
        Debug.Log("Monster HP : " + monsterCurHP);

        monsterName = monsterData[num].Name;

        monsterCurHP = monsterData[num].HP;

        monsterStage = monsterData[num].Stage;

        monsterGiveGold = monsterData[num].GiveGold;

        monsterLimitTime = monsterData[num].LimitTime;

        monsterImage.sprite = monsterData[num].Image;
    }

}
