using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Object/Weapon")]
public class Weapon : ScriptableObject
{
    public SpriteRenderer WeaponImg;
    
    public int WeaponLevel;
    public int WeaponDmg;
}
