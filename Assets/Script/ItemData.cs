using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemState", menuName = "Scriptable Object Asset/ItemState")]
public class ItemData : MonoBehaviour
{
    public string Name;
    public int Cost;
    public string EffectText;
    public int DamageUP;
}
