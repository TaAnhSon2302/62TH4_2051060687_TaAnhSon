using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpdateManager : Singleton<UpdateManager>
{
    [SerializeField] private EnemyCell[] transformsPool;
    public int poolIndex = 0;
    public int maximumPool = 1000;
    public int enemiesCount = 0;
    public int transformPoolCount = 0;
    private void Start() {
        if(transformsPool == null){
             transformsPool = new EnemyCell[maximumPool];
        }
    }
    private void Update() {
        for (int i = 0; i < transformPoolCount; i++)
        {
            if (transformsPool[i] == null) continue;
            transformsPool[i].CellUpdate();
        }
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < maximumPool; i++)
        {
            if (transformsPool[i] == null) continue;
            transformsPool[i].CellFixedUpdate();
        }
    }
    public void AddCellToPool(EnemyCell enemyCell)
    {
        for (int i = 0; i < enemiesCount+1; i++)
        {
            if (transformsPool[i] == null)
            {
                transformsPool[i] = enemyCell;
                poolIndex = i;
                transformPoolCount++;
                enemiesCount++;
                break;
            }
        }
    }
    public void RemoveCellFromPool(int index){
        transformsPool[index] = null;
        transformPoolCount--;
        enemiesCount--;
    }
    public void DestroyAllCell(){
        for(int i = 0 ;i<transformsPool.Length;i++){
            if (transformsPool[i] == null) continue;
            transformsPool[i].stateMachine.ChangeState(new EnemyStateDestroy(transformsPool[i]));
        }
    }
}
