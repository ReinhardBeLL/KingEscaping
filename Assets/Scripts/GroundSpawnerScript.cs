using System.Collections.Generic;
using UnityEngine;

public class GroundSpawnerScript : MonoBehaviour
{
    [SerializeField] GameObject chunckPrefab;
    [SerializeField] CameraSettings cameraSettings;
    [SerializeField] Transform chunckParent;
    [SerializeField] float chuncksSpeedMovement = 10f;
    Vector3 pos;
    Transform cameraTransform;
    float groundLenght = 10;
    int groundAmount = 14;
    List<GameObject> chuncks = new List<GameObject>();
    void Start()
    {
        pos = transform.position;
        cameraTransform = Camera.main.transform;

        chuncks = new List<GameObject>(groundAmount);
        SpawnChuncks();
    }
    void Update()
    {
        ChunckMoving();
    }
    void SpawnChuncks()
    {
        for (int i = 0; i < groundAmount; i++)
        {
            pos = transform.position;
           float spawnZ = i * groundLenght;
           float spawningForward = pos.z + spawnZ;

           Vector3 spawnPos = new Vector3(pos.x, pos.y, spawningForward);
           GameObject chunck = Instantiate(chunckPrefab, spawnPos, Quaternion.identity, chunckParent);
           chuncks.Add(chunck);
        }
    }
    public void ChangeSpeedOnCollision(float speed)
    {
        chuncksSpeedMovement += speed;
        if(chuncksSpeedMovement <= 0)
            chuncksSpeedMovement = 2f;

        var physics = Physics.gravity; 
        physics = new Vector3(physics.x, physics.y, physics.z - speed);
        Physics.gravity = physics;

       cameraSettings.CameraZoomChangeFOV(speed);
    }
    void ChunckMoving()
    {
        for (int i = chuncks.Count - 1; i >= 0; i--)
        {
            GameObject chunck = chuncks[i];
            Transform t = chunck.transform;
            t.Translate(-transform.forward * chuncksSpeedMovement * Time.deltaTime);

            if(t.position.z < cameraTransform.position.z - groundLenght)
            {
                Destroy(chunck);
                chuncks.RemoveAt(i);
                ChunksSpawnAhead();
            }
        }
    }
    void ChunksSpawnAhead()
    {
        pos = transform.position;
        float lastZ = chuncks[chuncks.Count - 1].transform.position.z;
        float chunckSpawnZ = lastZ + groundLenght;
        
        Vector3 spawnChuncks = new Vector3(pos.x, pos.y, chunckSpawnZ);
        GameObject newChunck = Instantiate(chunckPrefab, spawnChuncks, Quaternion.identity, chunckParent);
        chuncks.Add(newChunck);
    }
}
