using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region Camera Parameters
    public float cameraSmooth = 1.0f;
    private float delta;
    private Vector3 velocity = Vector3.zero;
    private Vector3 firstPosition;
    float fov = 0;
    float thetax;
    #endregion

    #region Scene objects
    Transform[] spawner;
    #endregion

    #region Manager
    GameManager gameManager;
    HudManager hudManager;
    #endregion

    #region Camera Functions
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
        hudManager = FindObjectOfType<HudManager>();
    }

    private void Awake()
    {
        fov = Camera.main.orthographicSize;
        thetax = transform.eulerAngles.x;
    }

    //Fonction permettant le recalibrage de la caméra pour que les pièces de chaque joueur soit toujours visible
    // Une partie du code à été fait en relation avec le groupe Petiot/Taupin, même si il n'est pas exactement similaire
    void FixedUpdate()
    {
        if (findHighestObject("Piece") != null && findLowestObject("Piece") != null)
        {
            float yc = 0;
            float y0 = -4;
            float y1;
            float zc = 0;

            //Calcul de la position yc de la caméra et de z la taille orthographique de la caméra
            y0 = findLowestObject("Piece").transform.position.y - 5;
            y1 = findHighestObject("Piece").transform.position.y + 5;

            zc = (y0 - y1) / (Mathf.Tan(Mathf.Deg2Rad * (thetax - fov / 2)) - Mathf.Tan(Mathf.Deg2Rad * (thetax + fov / 2)));


            if (zc < 5)
                zc = 5;

            yc = y0 + zc * Mathf.Tan(Mathf.Deg2Rad * (thetax + fov / 2)) + 1;

            yc = yc < firstPosition.y-5 ? firstPosition.y : yc+5;


            float newSize = 0;

            if (y0 + Camera.main.orthographicSize < yc)
            {
                newSize = yc - y0;
            }

            if(y1 - Camera.main.orthographicSize > yc && y1 - yc > newSize )
            {
                newSize = y1 - yc;
            }

            float z = newSize <= 18 ? 18 : Mathf.Round(newSize);

            //Assignation à chaque composant, les spawners sont placés de tel sorte à être toujours au dessus de la caméra
            // La caméra de façon à être centré, et bougeant de façon linéaire (à l'aide de la fonction SmoothDamp)
            // Le canvas suit également le mouvement
            // Les pièces sont également ramenés vers le bas en cas de chute de la tour la plus haute (ramenés au bord de la caméra pour toujours être visible)

            for (int i = 0; i < spawner.Length; i++)
            {
                if (spawner[i] != null)
                {
                    spawner[i].position = new Vector3(spawner[i].position.x, transform.position.y + GetComponent<Camera>().orthographicSize + 3, spawner[i].position.z);
                }
            }
      

            this.GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, z, Time.deltaTime * cameraSmooth);


            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, yc, transform.position.z), ref velocity, cameraSmooth);
            hudManager.GetCanvas().transform.position = Vector3.SmoothDamp(hudManager.GetCanvas().transform.position, new Vector3(hudManager.GetCanvas().transform.position.x, 10, hudManager.GetCanvas().transform.position.z),ref velocity, cameraSmooth);
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

    #endregion

    #region Tools Functions

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

    #endregion
}
