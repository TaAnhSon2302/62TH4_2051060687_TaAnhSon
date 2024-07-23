using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameCalculator : MonoBehaviour
{
    public static StatusState ElementReact(StatusState currentStatus, StatusState incomeStatus){
        StatusState resultStatus = new();
        switch(currentStatus.primaryElement){
            case PrimaryElement.Fire:
                switch (incomeStatus.primaryElement){
                    case PrimaryElement.Fire:
                        resultStatus = incomeStatus;
                    break;
                    case PrimaryElement.Toxin:
                        resultStatus = new StatusStateHellBurn(currentStatus.enemyCell,currentStatus.damagePerTick,currentStatus.stack);
                        break;
                    case PrimaryElement.Ice:
                        resultStatus = new StatusStateBlast(currentStatus.enemyCell, currentStatus.damagePerTick, currentStatus.stack);
                        break;
                }
            break;
            case PrimaryElement.Ice:
                switch (incomeStatus.primaryElement){
                    case PrimaryElement.Ice:
                        resultStatus = incomeStatus;
                    break;
                    case PrimaryElement.Fire:
                        resultStatus = new StatusStateShattering(currentStatus.enemyCell,currentStatus.stack);
                    break;
                    case PrimaryElement.Toxin:
                        resultStatus = new StatusStateWeak(currentStatus.enemyCell,currentStatus.stack);
                    break;
                }
            break;
            case PrimaryElement.Electric:
                switch (incomeStatus.primaryElement){
                    case PrimaryElement.Electric:
                        resultStatus = incomeStatus;
                    break;
                    case PrimaryElement.Toxin:
                        resultStatus = new StatusStateCorrosive(currentStatus.enemyCell,currentStatus.stack);
                    break;
                    case PrimaryElement.Ice:
                        // Debug.Log("Super conductive require condition matched");
                        resultStatus = new StatusStateSuperConductive(currentStatus.enemyCell,currentStatus.damagePerTick,currentStatus.stack);
                    break;
                }
            break;
            case PrimaryElement.Toxin:
                switch (incomeStatus.primaryElement){
                    case PrimaryElement.Toxin:
                        resultStatus = incomeStatus;
                    break;
                    case PrimaryElement.Fire:
                        resultStatus = new StatusStateBlast(currentStatus.enemyCell,currentStatus.damagePerTick,currentStatus.stack);
                    break;
                    case PrimaryElement.Ice:
                        resultStatus = new StatusStateWeak(currentStatus.enemyCell,currentStatus.stack);
                    break;
                    case PrimaryElement.Electric:
                        resultStatus = new StatusStateCorrosive(currentStatus.enemyCell,currentStatus.stack);
                    break;
                }
            break;
            case PrimaryElement.None:
                resultStatus = incomeStatus;
            break;
            default:
                resultStatus = incomeStatus;
            break;
        }
        return resultStatus;
    }
}
