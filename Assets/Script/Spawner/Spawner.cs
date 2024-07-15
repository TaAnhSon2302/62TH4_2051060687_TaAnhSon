using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Singleton<Spawner>
{
    [SerializeField] private float respawnDistance = 20f;
    [SerializeField] private GameObject effectHolder;
    protected Transform spawnPos;
    protected virtual void Start(){
        EffectManager.Instance.effectHolder = effectHolder;
    }
    protected virtual void SpawnEnemies(){

    }
    protected Vector3 SetTargetCyclePos(float spawnRadius,Vector3 playerPos)
    {
        float randomAngle = Random.value;
        float angleInDegrees = randomAngle * 360;
        float angleInRadians = Mathf.Deg2Rad * angleInDegrees;
        float spawnX = playerPos.x + spawnRadius * Mathf.Cos(angleInRadians);
        float spawnY = playerPos.y + spawnRadius * Mathf.Sin(angleInRadians);
        Vector3 position = new Vector3(spawnX, spawnY, 0);
        return position;
    }
    public void Reposition(Transform transform){
        Vector3 cameraLeftBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 cameraRightTop = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
        float cameraLeft = cameraLeftBottom.x;
        float cameraBottom = cameraLeftBottom.y;
        float cameraRight = cameraRightTop.x;
        float cameraTop = cameraRightTop.y;
        float padding = cameraRight-Camera.main.transform.position.x-(cameraTop-Camera.main.transform.position.y);
        if (transform.position.x < cameraLeft - respawnDistance)
        {
            transform.position = new Vector2(cameraRight + respawnDistance / 2, transform.position.y);
        }
        if (transform.position.x > cameraRight + respawnDistance)
        {
            transform.position = new Vector2(cameraLeft - respawnDistance / 2, transform.position.y);
        }
        if (transform.position.y < cameraBottom - respawnDistance - padding)
        {
            transform.position = new Vector2(transform.position.x, cameraTop + respawnDistance / 2);
        }
        if (transform.position.y > cameraTop + respawnDistance + padding)
        {
            transform.position = new Vector2(transform.position.x, cameraBottom - respawnDistance / 2);
        }
    }
}
