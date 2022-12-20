using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    [SerializeField] Monster monsterPrefab;
    Monster monster;
    int monsterHP { get; set; }

    int monsterMaxHP = 100;

    int monsterCurCount = 0;
    int monsterMaxCount = 1;

    int autoDamage { get; set; }

    private void Awake()
    {
        monsterHP = monsterMaxHP;

        monsterCurCount = monsterMaxCount;
        
        autoDamage =1;
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        monsterHP -= autoDamage;
        UIManager.INSTANCE.AutoDamageText.text = autoDamage.ToString();
    }



    public void HitToMonster(int damage)
    {
        Debug.Log(damage);

        monsterHP -= damage;

        UIManager.INSTANCE.AttackDamageText.text = damage.ToString();

        if (monsterHP <= 0)
        {
            Destroy(this.gameObject);

            UIManager.INSTANCE.GameClearUI.gameObject.SetActive(true); // °ÔÀÓ Å¬¸®¾î ½Ã, °ÔÀÓ ¸ØÃã
        }
    }

}
