using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum TypeOfBuffs
{
    Damage,
    Health,
    Speed
}

[CreateAssetMenu(fileName = "Buffs", menuName = "Scriptable Objects/Buffs")]
public class Buffs : ScriptableObject
{
    public TypeOfBuffs typeOfBuff;
    public int amountOfBuff;
}
