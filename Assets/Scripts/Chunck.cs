using System.Collections.Generic;
using UnityEngine;

public class Chunck : MonoBehaviour
{
    [Header("Objects prefab")]
    [SerializeField] GameObject barrierPrefab;
    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject coinPrefab;
    [Header("Item spawn chance")]
    [SerializeField] float chanceSpawnApple = .4f;
    [SerializeField] float chanceSpawnCoin = .6f;
    GroundSpawnerScript gScript;
    float[] barrierLanes = {-4.17f, -1.62f, 1.08f, 3.88f};
    List<int> roadIndex = new List<int> {0, 1, 2, 3};
    float chunckLenght = 5f;
    float seperationChunckLEnght = 2f;
    Vector3 pos;

    void Start()
    {
        pos = transform.position;
        SpawnBarrier();
        SpawnApples();
        SpawnCoins();
    }
    public void Init(GroundSpawnerScript gScript)
    {
        this.gScript = gScript;
    }
    void SpawnBarrier()
    {
        int randomIndexSpawning = Random.Range(0, barrierLanes.Length);
        for (int i = 0; i < randomIndexSpawning; i++)
        {
            if(roadIndex.Count <= 0) break;
            int selectRoad = ChooseLaneToSpawn();
            pos = transform.position;
            Vector3 posSpawn = new Vector3(barrierLanes[selectRoad], pos.y, pos.z);
            Instantiate(barrierPrefab, posSpawn, Quaternion.identity, this.transform);
        }
    }
    void SpawnApples()
    {
        if(Random.value >= chanceSpawnApple || roadIndex.Count <= 0) return;
        int selectRoad = ChooseLaneToSpawn();
        pos = transform.position;
        Vector3 posSpawn = new Vector3(barrierLanes[selectRoad], 0.4f, pos.z);
        GameObject appleGO = Instantiate(applePrefab, posSpawn, Quaternion.identity, this.transform);
        PickUp pickUp = appleGO.GetComponent<PickUp>();
        pickUp.Init(gScript);
    }
    void SpawnCoins()
    {
        if(Random.value >= chanceSpawnCoin || roadIndex.Count <= 0) return;
        pos = transform.position;
        int selectRoad = ChooseLaneToSpawn();
        int randomSpawIndex = Random.Range(1, 7);
        float topOfZ = pos.z + chunckLenght;
        for (int i = 0; i < randomSpawIndex; i++)
        {
            float spawnBack = topOfZ - (i * seperationChunckLEnght);
            Vector3 posSpawn = new Vector3(barrierLanes[selectRoad], 0.4f, spawnBack);
            GameObject coinGO = Instantiate(coinPrefab, posSpawn, Quaternion.identity, this.transform);
            PickUp pickUp = coinGO.GetComponent<PickUp>();
            pickUp.Init(gScript);
        }
    }
    int ChooseLaneToSpawn()
    {
        int randomChooseRoad = Random.Range(0, roadIndex.Count);
        int selectRoad = roadIndex[randomChooseRoad];
        roadIndex.RemoveAt(randomChooseRoad);
        return selectRoad;
    }
}
