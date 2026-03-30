using System.Collections.Generic;
using UnityEngine;

public class GroundSpawnerScript : MonoBehaviour
{
    List<GameObject> chuncks = new List<GameObject>();
    [SerializeField] CameraSettings cameraSettings;
    [SerializeField] GameObject chunckPrefab;
    [SerializeField] Transform chunckParent;
    Vector3 pos;
    Transform cameraTransform;
    [SerializeField] float chuncksSpeedMovement = 10f;
    [SerializeField] float minchuncksSpeedMovement = 2f;
    [SerializeField] float maxchuncksSpeedMovement = 35f;
    float groundLenght = 10;
    int groundAmount = 14;

    Vector3 physicsGravity;
    [SerializeField] float minGravityVelocityZ = -25f;
    [SerializeField] float maxGravityVelocityZ = -2f;
    void Start()
    {
        pos = transform.position;
        physicsGravity = Physics.gravity;
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
        chuncksSpeedMovement = Mathf.Clamp(chuncksSpeedMovement + speed, 
                                            minchuncksSpeedMovement, 
                                            maxchuncksSpeedMovement); 

        float gravityPhysicsZ = Mathf.Clamp(physicsGravity.z - speed, 
                                            minGravityVelocityZ, 
                                            maxGravityVelocityZ);
        physicsGravity = new Vector3(physicsGravity.x, physicsGravity.y, gravityPhysicsZ);
        Physics.gravity = physicsGravity;

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
