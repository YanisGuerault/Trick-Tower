using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform[] spawner;
    public Transform ground;
    public SpawnBox spawn;
    private float delta;
    private Vector3 velocity = Vector3.zero;
    private Vector3 firstPosition;

    // Start is called before the first frame update
    void Start()
    {
        firstPosition = transform.position;
        spawner = new Transform[10];
        int i = 0;
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Spawn Box"))
        {
            spawner[i] = go.transform;
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (findHighestObject("Piece") != null)
        {

            //Debug.Log("High : " + high + " | Camera : " + transform.position.y);

            float objectToFollow = findHighestObject("Piece").transform.position.y;

            /*if(spawn.getLastPiece().transform.position.y < transform.position.y)
            {
                objectToFollow = spawn.getLastPiece().transform.position.y;
            }
            else
            {
                objectToFollow = findHighestObject("Piece").transform.position.y;
            }*/

            if (objectToFollow > transform.position.y)
            {
                delta = objectToFollow + 1 - transform.position.y;
                transform.transform.position = Vector3.SmoothDamp(transform.transform.position, new Vector3(transform.position.x, objectToFollow + 1, transform.position.z), ref velocity, 0.4F);
                //>.transform.position = new Vector3(>.position, new Vector3(>.position.x, high+1, >.position.z);
                foreach (Transform spawn in spawner)
                {
                    spawn.position = Vector3.SmoothDamp(spawn.position, new Vector3(spawn.position.x, spawn.position.y + delta, spawn.position.z), ref velocity, 0.4F);
                }
                //spawner.transform.position = new Vector3(spawner.position.x, spawner.position.y + delta, spawner.position.z);
            }

            if (objectToFollow < transform.position.y && transform.position.y > firstPosition.y)
            {
                delta = transform.position.y - objectToFollow;
                Transform spawnPosition = spawn.getLastPiece().transform;
                spawnPosition.position = Vector3.SmoothDamp(spawnPosition.position, new Vector3(spawnPosition.position.x, spawnPosition.position.y - delta, spawnPosition.position.z), ref velocity, 0.4F);
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, objectToFollow, transform.position.z), ref velocity, 0.4F);
                foreach (Transform spawn in spawner)
                {
                    spawn.position = Vector3.SmoothDamp(spawn.position, new Vector3(spawn.position.x, spawn.position.y - delta, spawn.position.z), ref velocity, 0.4F);
                }
                /*spawnPosition.position = new Vector3(spawnPosition.position.x, spawnPosition.position.y - delta, spawnPosition.position.z);
                >.transform.position = new Vector3(>.position.x, high, >.position.z);
                spawner.transform.position = new Vector3(spawner.position.x, spawner.position.y - delta, spawner.position.z);*/
            }
        }
    }

    GameObject findHighestObject(string tag)
    {
        GameObject[] pieceList = GameObject.FindGameObjectsWithTag(tag);
        GameObject highestPiece = null;
        float highestPiecePosition = -99999f; //start with a value that could never be higher than all your objects
        for (int i = 0; i < pieceList.Length; i++)
        {
            float y = pieceList[i].transform.position.y; //cache this, because calculating it twice is also slower than need be
            if  (pieceList[i].GetComponent<Piece>().isGrounded() && y > highestPiecePosition)
            {
                highestPiecePosition = y;
                highestPiece = pieceList[i];
            }
        }

        return highestPiece;
    }
}
