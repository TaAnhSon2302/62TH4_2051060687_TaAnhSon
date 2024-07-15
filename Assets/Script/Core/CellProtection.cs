using System;
using System.Diagnostics.Tracing;
using UnityEngine;

[Serializable]
public class CellProtection
{
    public CellProtection(){}
    public CellProtection(CellProtection cellProtection){
        armorType = cellProtection.armorType;
        shieldType = cellProtection.shieldType;
        armorPoint = cellProtection.armorPoint;
        shieldPoint = cellProtection.shieldPoint;
    }
    public ArmorType armorType = ArmorType.None;
    public ShieldType shieldType = ShieldType.None;
    public int armorPoint = 0;
    public int shieldPoint = 0;
}
public enum ArmorType{
    None,
    Alloy,
    Bio,
}
public enum ShieldType{
    None,
    Proto,
    Pulse

}