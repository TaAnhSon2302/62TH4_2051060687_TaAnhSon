using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameStatic;

public partial class GameCalculator
{
    public static (int, int) DamageTake(CellProtection currentCellProtection, int baseCellArmor, int damageIncome, Elements elements = null)
    {
        //Debug.Log("base armor:"+baseCellArmor);
        int armorReduce = 0;
        int damageTaken = 0;
        if(currentCellProtection.shieldPoint>0){
            damageTaken = damageIncome;
        }
        else{
            switch (currentCellProtection.armorType)
            {
                case ArmorType.None:
                    damageTaken = damageIncome;
                    break;
                case ArmorType.Alloy:
                    damageTaken = (int)((float)damageIncome * (float)(1 - DamageReduceByArmorCalculator(currentCellProtection.armorPoint)));
                    break;
                case ArmorType.Bio:
                    armorReduce = damageIncome >= baseCellArmor / 20 ? damageIncome: baseCellArmor/20;
                    damageTaken = damageIncome - currentCellProtection.armorPoint >= damageIncome / 20 ? damageIncome - currentCellProtection.armorPoint : damageIncome / 20;
                    break;
                default:
                    damageTaken = int.MaxValue;
                    break;
            }
        }
        return (damageTaken, armorReduce);
    }
    public static (float, int) CriticalManager(CellGun cellGun)
    {
        int criticalCoeffident = Random.Range(0, 101);
        if(cellGun == null)
            return (1,0);
        // value return is critical multipler and critical tier
        if (cellGun.criticalRate < 100)
        {
            return criticalCoeffident >= cellGun.criticalRate ?
            (1, 0)
            : (((cellGun.criticalMultiple -1) * ((int)cellGun.criticalRate / 100 + 1)+1), (int)cellGun.criticalRate / 100 + 1);
        }
        else if (100 <= cellGun.criticalRate && cellGun.criticalRate < 200)
        {
            return criticalCoeffident >= cellGun.criticalRate % 100 ?
            (((cellGun.criticalMultiple -1) * ((int)cellGun.criticalRate / 100)+1), (int)cellGun.criticalRate / 100)
            : (((cellGun.criticalMultiple -1) * ((int)cellGun.criticalRate / 100 + 1)+1), (int)cellGun.criticalRate / 100 + 1);
        }
        else if (200 <= cellGun.criticalRate && cellGun.criticalRate < 300)
        {
            return criticalCoeffident >= cellGun.criticalRate % 100 ?
            (((cellGun.criticalMultiple -1) * ((int)cellGun.criticalRate / 100)+1), (int)cellGun.criticalRate / 100)
            : (((cellGun.criticalMultiple -1) * ((int)cellGun.criticalRate / 100 + 1)+1), (int)cellGun.criticalRate / 100 + 1);
        }
        else if (300 <= cellGun.criticalRate && cellGun.criticalRate < 400)
        {
            return criticalCoeffident >= cellGun.criticalRate % 100 ?
            (((cellGun.criticalMultiple -1) * ((int)cellGun.criticalRate / 100)+1), (int)cellGun.criticalRate / 100)
            : (((cellGun.criticalMultiple -1) * ((int)cellGun.criticalRate / 100 + 1)+1), (int)cellGun.criticalRate / 100 + 1);
        }
        else if (400 <= cellGun.criticalRate && cellGun.criticalRate < 500)
        {
            return criticalCoeffident >= cellGun.criticalRate % 100 ?
            (((cellGun.criticalMultiple -1) * ((int)cellGun.criticalRate / 100)+1), (int)cellGun.criticalRate / 100)
            : (((cellGun.criticalMultiple -1) * ((int)cellGun.criticalRate / 100 + 1)+1), (int)cellGun.criticalRate / 100 + 1);
        }
        else
        {
            return (((cellGun.criticalMultiple -1) * ((int)cellGun.criticalRate / 100)+1), (int)cellGun.criticalRate / 100);
        }
        // if (criticalCoeffident < cellGun.criticalRate % 100)
        // {
        //     return (((cellGun.criticalMultiple -1) * ((int)cellGun.criticalRate / 100 + 1)+1), (int)cellGun.criticalRate / 100 + 1);
        // }
        // return (cellGun.bulletPrefab.Damage, 0);
    }
        public static float ShieldRechargeCalculator(int maxShield)
    {
        // Debug.Log("Shield recharge rate: " + 5 * Mathf.Sqrt((float)maxShield));
        return 5 * Mathf.Sqrt((float)maxShield);
    }
    public static float ShieldRechargeDelayCalculator(int currentShield)
    {
        if(currentShield>0){
            // Debug.Log("Shield recharge delay in: " +0.1f * Mathf.Sqrt((float)currentShield));
            return 0.1f * Mathf.Sqrt((float)currentShield);
        }
        else{
            // Debug.Log("Shield depleted, recharge in: "+1 + 0.1f * Mathf.Sqrt((float)currentShield));
            return 1 + 0.1f * Mathf.Sqrt((float)currentShield);
        }
    }
    public static float DamageReduceByArmorCalculator(int armor){
        return (float)armor / (armor + ARMOR_COEFFICIENT);
    }
    public static int CalculateFactorial(int n)
    {
        if (n < 0)
        {
            return -1;
        }

        int factorial = 1;
        for (int i = 1; i <= n; i++)
        {
            factorial *= i;
        }

        return factorial;
    }
}
