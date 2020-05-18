using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform[] spawner;
    public List<Player> playerList;
    public Transform ground;
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
                float deltaObject = objectToFollow - findLowestObject("Piece").transform.position.y;

                if(deltaObject > this.GetComponent<Camera>().orthographicSize)
                {
                    this.GetComponent<Camera>().orthographicSize += 1;
                    transform.transform.position = Vector3.SmoothDamp(transform.transform.position, new Vector3(transform.position.x, findLowestObject("Piece").transform.position.y + deltaObject, transform.position.z), ref velocity, 0.4F);
                    foreach (Transform spawn in spawner)
                    {
                        spawn.position = Vector3.SmoothDamp(spawn.position, new Vector3(spawn.position.x, GetComponent<Camera>().orthographicSize+transform.position.y, spawn.position.z), ref velocity, 0.4F);
                    }
                } else {
                    transform.transform.position = Vector3.SmoothDamp(transform.transform.position, new Vector3(transform.position.x, objectToFollow + 1, transform.position.z), ref velocity, 0.4F);
                    foreach (Transform spawn in spawner)
                    {
                        spawn.position = Vector3.SmoothDamp(spawn.position, new Vector3(spawn.position.x, spawn.position.y + delta, spawn.position.z), ref velocity, 0.4F);
                    }
                }
                
            }

            if (objectToFollow < transform.position.y && transform.position.y > firstPosition.y)
            {
                delta = transform.position.y - objectToFollow;
               /* Transform spawnPosition = spawn.getLastPiece().transform;
                spawnPosition.position = Vector3.SmoothDamp(spawnPosition.position, new Vector3(spawnPosition.position.x, spawnPosition.position.y - delta, spawnPosition.position.z), ref velocity, 0.4F);
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, objectToFollow, transform.position.z), ref velocity, 0.4F);*/
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
        /* GameObject[] pieceList = GameObject.FindGameObjectsWithTag(tag);
         GameObject highestPiece = null;
         float highestPiecePosition = -99999f;
         for (int i = 0; i < pieceList.Length; i++)
         {
             float y = pieceList[i].transform.position.y;
             if  (pieceList[i].GetComponent<Piece>().isGrounded() && y > highestPiecePosition)
             {
                 highestPiecePosition = y;
                 highestPiece = pieceList[i];
             }
         }

         return highestPiece;*/

        GameObject max = null;
        float maxPosition = -9999999f;

        foreach(GameObject obj in findHighestObjectPerPlayer(tag))
        {
            if(maxPosition < obj.transform.position.y)
            {
                max = obj;
                maxPosition = obj.transform.position.y;
            }
        }

        return max;
    }

    GameObject findLowestObject(string tag)
    {
        /* GameObject[] pieceList = GameObject.FindGameObjectsWithTag(tag);
         GameObject highestPiece = null;
         float highestPiecePosition = -99999f;
         for (int i = 0; i < pieceList.Length; i++)
         {
             float y = pieceList[i].transform.position.y;
             if  (pieceList[i].GetComponent<Piece>().isGrounded() && y > highestPiecePosition)
             {
                 highestPiecePosition = y;
                 highestPiece = pieceList[i];
             }
         }

         return highestPiece;*/

        GameObject min = null;
        float minPosition = 9999999f;

        foreach (GameObject obj in findHighestObjectPerPlayer(tag))
        {
            if (minPosition > obj.transform.position.y)
            {
                min = obj;
                minPosition = obj.transform.position.y;
            }
        }

        return min;
    }

    GameObject[] findHighestObjectPerPlayer(string tag)
    {
        GameObject[] pieceList = GameObject.FindGameObjectsWithTag(tag);
        GameObject[] highestPiece = new GameObject[playerList.Count];
        float[] highestPiecePosition = new float[playerList.Count];
        for (int i = 0; i < pieceList.Length; i++)
        {
            float y = pieceList[i].transform.position.y;
            int playerIdx = playerList.IndexOf(pieceList[i].GetComponent<Player>());
            if (pieceList[i].GetComponent<Piece>().isGrounded() && y > highestPiecePosition[playerIdx])
            {
                highestPiecePosition[playerIdx] = y;
                highestPiece[playerIdx] = pieceList[i];
            }
        }

        return highestPiece;
    }
}
