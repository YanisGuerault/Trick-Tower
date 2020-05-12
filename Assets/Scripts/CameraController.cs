using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform mainCamera;
    public Transform spawner;
    public SpawnBox spawn;
    private float delta;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (findHighestObject("Piece") != null)
        {
            float high = findHighestObject("Piece").transform.position.y;

            Debug.Log("High : " + high + " | Camera : " + mainCamera.position.y);

            if (high > mainCamera.position.y)
            {
                delta = high+1 - mainCamera.position.y;
                mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, new Vector3(mainCamera.position.x, high + 1, mainCamera.position.z), ref velocity, 0.4F);
                //mainCamera.transform.position = new Vector3(mainCamera.position, new Vector3(mainCamera.position.x, high+1, mainCamera.position.z);
                spawner.transform.position = Vector3.SmoothDamp(spawner.transform.position, new Vector3(spawner.position.x, spawner.position.y + delta, spawner.position.z), ref velocity, 0.4F);
                //spawner.transform.position = new Vector3(spawner.position.x, spawner.position.y + delta, spawner.position.z);
            }

            if (high < mainCamera.position.y)
            {
                delta = mainCamera.position.y - high;
                Transform spawnPosition = spawn.getLastPiece().transform;
                spawnPosition.position = Vector3.SmoothDamp(spawnPosition.position, new Vector3(spawnPosition.position.x, spawnPosition.position.y - delta, spawnPosition.position.z), ref velocity, 0.4F);
                mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.position, new Vector3(mainCamera.position.x, high, mainCamera.position.z), ref velocity, 0.4F);
                spawner.transform.position = Vector3.SmoothDamp(spawner.position, new Vector3(spawner.position.x, spawner.position.y - delta, spawner.position.z), ref velocity, 0.4F);
                /*spawnPosition.position = new Vector3(spawnPosition.position.x, spawnPosition.position.y - delta, spawnPosition.position.z);
                mainCamera.transform.position = new Vector3(mainCamera.position.x, high, mainCamera.position.z);
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
