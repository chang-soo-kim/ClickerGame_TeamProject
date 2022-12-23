using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemState", menuName = "Scriptable Object/ItemState")]
public class ItemData : ScriptableObject
{
    public string Name;
    public int Cost;
    public string EffectText;
    public int DamageUP;
    public SpriteRenderer Image;
}
