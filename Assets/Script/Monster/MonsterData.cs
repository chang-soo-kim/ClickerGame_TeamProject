using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "MonsterStat", menuName = "Scriptable Object Asset/MonsterStat")]
public class MonsterData : ScriptableObject
{
    public string Name;
    public int HP;
    public int Stage;
    public int GiveGold;
    public float LimitTime;
    public Sprite Image;


}
