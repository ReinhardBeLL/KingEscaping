using System.Collections.Generic;
using UnityEngine;

public class GroundSpawnerScript : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] CameraSettings cameraSettings;
    [SerializeField] GameObject chunckPrefab;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] Transform chunckParent;


    [Header("Tuning")]
    [SerializeField] float chuncksSpeedMovement = 10f;
    [SerializeField] float minchuncksSpeedMovement = 2f;
    [SerializeField] float maxchuncksSpeedMovement = 35f;
    [SerializeField] float minGravityVelocityZ = -25f;
    [SerializeField] float maxGravityVelocityZ = -2f;
    
    [Header("Runtime")]
    Transform cameraTransform;
    Vector3 physicsGravity;
    Vector3 pos;
    List<GameObject> chuncks = new List<GameObject>();

    
    float groundLenght = 10f;
    int groundAmount = 14;
    void Start()
    {
        pos = transform.position;
        physicsGravity = Physics.gravity;
        cameraTransform = Camera.main.transform;

        chuncks = new List<GameObject>(groundAmount);
        SpawnChuncks();
    }
    void Update() =>
        ChunckMoving();
    void SpawnChuncks()
    {
        for (int i = 0; i < groundAmount; i++)
        {
            pos = transform.position;
           float spawnZ = i * groundLenght;
           float spawningForward = pos.z + spawnZ;

           Vector3 spawnPos = new Vector3(pos.x, pos.y, spawningForward);
           GameObject chunck = Instantiate(chunckPrefab, spawnPos, Quaternion.identity, chunckParent);
           Chunck chunkScript = chunck.GetComponent<Chunck>();
           chunkScript.Init(this, scoreManager);
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
        Chunck chunkScript = newChunck.GetComponent<Chunck>();
        chunkScript.Init(this, scoreManager);
        chuncks.Add(newChunck);
    }
}
