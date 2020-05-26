using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform[] spawner;
    public Transform ground;
    public float cameraSmooth = 1.0f;
    private float delta;
    private Vector3 velocity = Vector3.zero;
    private Vector3 firstPosition;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        firstPosition = transform.position;
        spawner = new Transform[GameObject.FindGameObjectsWithTag("Spawn Box").Length];
        int i = 0;
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Spawn Box"))
        {
            spawner[i] = go.transform;
            i++;
        }
        gameManager = FindObjectOfType<GameManager>();
    }

    public Transform target;
    float zc = 0;
    float yc = 0;
    float fov = 0;
    float thetax;
    float y0 = -4;
    float y1;

    private void Awake()
    {
        fov = Camera.main.orthographicSize;
        thetax = transform.eulerAngles.x;
    }

    void FixedUpdate()
    {
        if (findHighestObject("Piece") != null && findLowestObject("Piece") != null)
        {
            y0 = findLowestObject("Piece").transform.position.y - 5;
            y1 = findHighestObject("Piece").transform.position.y + 5;


            // Inconnus zc,yc (c = caméra) 

            zc = (y0 - y1) / (Mathf.Tan(Mathf.Deg2Rad * (thetax - fov / 2)) - Mathf.Tan(Mathf.Deg2Rad * (thetax + fov / 2)));


            if (zc < 5)
                zc = 5;

            yc = y0 + zc * Mathf.Tan(Mathf.Deg2Rad * (thetax + fov / 2)) + 1;

            yc = yc < firstPosition.y-1 ? firstPosition.y : yc+5;

            float newSize = 0;

            if (y0 + Camera.main.orthographicSize < yc)
            {
                newSize = yc - y0;
            }

            if(y1 - Camera.main.orthographicSize > yc && y1 - yc > newSize )
            {
                newSize = y1 - yc;
            }

            float z = newSize <= 12 ? 12 : Mathf.Round(newSize);


            for (int i = 0; i < spawner.Length; i++)
            {
                if (spawner[i] != null)
                {
                    spawner[i].position = new Vector3(spawner[i].position.x, transform.position.y + GetComponent<Camera>().orthographicSize + 3, spawner[i].position.z);
                }
            }
      

            this.GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, z, Time.deltaTime * cameraSmooth);


            // Smoothly move the camera towards that target position 
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, yc, transform.position.z), ref velocity, cameraSmooth);

            foreach (Player p in gameManager.getPlayerList())
            {
                Vector3 lastPosition = p.spawner.getLastPiece().transform.position;
                if (lastPosition.y > transform.position.y + GetComponent<Camera>().orthographicSize)
                {
                    p.spawner.getLastPiece().transform.position = new Vector3(lastPosition.x, transform.position.y + GetComponent<Camera>().orthographicSize, lastPosition.z);
                }
            }
        }


    }

    GameObject findHighestObject(string tag)
    {
        GameObject max = null;
        float maxPosition = -9999999f;

        foreach (GameObject obj in findHighestObjectPerPlayer(tag))
        {
            if (obj == null) { break; }
            if (maxPosition < obj.transform.position.y)
            {
                max = obj;
                maxPosition = obj.transform.position.y;
            }
        }

        return max;
    }

    GameObject findLowestObject(string tag)
    {

        GameObject min = null;
        float minPosition = 9999999f;

        foreach (GameObject obj in findHighestObjectPerPlayer(tag))
        {
            if(obj == null){break;}
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
        GameObject[] highestPiece = new GameObject[gameManager.getPlayerList().Count];
        float[] highestPiecePosition = new float[gameManager.getPlayerList().Count];

        for(int i = 0; i < highestPiecePosition.Length;i++)
        {
            highestPiecePosition[i] = -999999999999f;
        }

        for (int i = 0; i < pieceList.Length; i++)
        {
            float y = pieceList[i].transform.position.y;
            int playerIdx = gameManager.getPlayerList().IndexOf(pieceList[i].GetComponent<Piece>().player);
            if (pieceList[i].GetComponent<Piece>().isGrounded() && y > highestPiecePosition[playerIdx])
            {
                highestPiecePosition[playerIdx] = y;
                highestPiece[playerIdx] = pieceList[i];
            }
        }

        return highestPiece;
    }
}
