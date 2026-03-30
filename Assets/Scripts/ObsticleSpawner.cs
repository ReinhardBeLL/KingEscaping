using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefab;
    [SerializeField] Transform obstacleParent;
    void Start()
    {
        StartCoroutine(DelaySpawning());
    }
    void SpawningObstacle()
    {
        if(obstaclePrefab == null || obstaclePrefab.Length == 0) return;
        Vector3 pos = transform.position;
        int randomIndex = Random.Range(0, obstaclePrefab.Length);
        Instantiate(obstaclePrefab[randomIndex], 
                    new Vector3(Random.Range(-5, 5), pos.y, pos.z), 
                    Random.rotation, 
                    obstacleParent);
    }
    IEnumerator DelaySpawning()
    {
        while(true)
        {
            yield return new WaitForSeconds(2f);
            SpawningObstacle();
        }
    }
}
