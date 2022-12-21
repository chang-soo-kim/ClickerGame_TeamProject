using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] MonsterData[] monsterData;


    [SerializeField] Monster monsterPrefab;
    Monster monster;
    public int monsterHP { get; set; }

    public int monsterMaxHP = 100;

    public int monsterGiveGold;

    int autoDamage { get; set; }

    private void Awake()
    {
        monsterHP = monsterMaxHP;

        autoDamage = 1;

        monsterGiveGold = 10;
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    float time = 0f;

    // Update is called once per frame
    void Update()
    {
        // 1초마다 1씩 공격
        time += Time.deltaTime;
        if (time >= 1f)
        {
            monsterHP -= autoDamage;
            UIManager.INSTANCE.AutoDamageText.text = autoDamage.ToString();
            time = 0f;
        }

    }

    public void HitToMonster(int damage)
    {
        Debug.Log(damage);

        monsterHP -= damage;

        UIManager.INSTANCE.AttackDamageText.text = damage.ToString();

        if (monsterHP <= 0)
        {
            Destroy(this.gameObject);

            UIManager.INSTANCE.GameClearUI.gameObject.SetActive(true); // 게임 클리어 시, 게임 멈춤

            // 플레이어에게 GiveGold 만큼 전달
        }
    }

}
