using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform spawner;
    public Transform ground;
    public SpawnBox spawn;
    private float delta;
    private Vector3 velocity = Vector3.zero;
    private Vector3 firstPosition;

    // Start is called before the first frame update
    void Start()
    {
        firstPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (findHighestObject("Piece") != null)
        {
            float high = findHighestObject("Piece").transform.position.y;

            Debug.Log("High : " + high + " | Camera : " + transform.position.y);

            if (high > transform.position.y)
            {
                delta = high+1 - transform.position.y;
                transform.transform.position = Vector3.SmoothDamp(transform.transform.position, new Vector3(transform.position.x, high + 1, transform.position.z), ref velocity, 0.4F);
                //>.transform.position = new Vector3(>.position, new Vector3(>.position.x, high+1, >.position.z);
                spawner.transform.position = Vector3.SmoothDamp(spawner.transform.position, new Vector3(spawner.position.x, spawner.position.y + delta, spawner.position.z), ref velocity, 0.4F);
                //spawner.transform.position = new Vector3(spawner.position.x, spawner.position.y + delta, spawner.position.z);
            }

            if (high < transform.position.y)
            {
                delta = transform.position.y - high;
                Transform spawnPosition = spawn.getLastPiece().transform;
                spawnPosition.position = Vector3.SmoothDamp(spawnPosition.position, new Vector3(spawnPosition.position.x, spawnPosition.position.y - delta, spawnPosition.position.z), ref velocity, 0.4F);
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, high, transform.position.z), ref velocity, 0.4F);
                spawner.transform.position = Vector3.SmoothDamp(spawner.position, new Vector3(spawner.position.x, spawner.position.y - delta, spawner.position.z), ref velocity, 0.4F);
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
