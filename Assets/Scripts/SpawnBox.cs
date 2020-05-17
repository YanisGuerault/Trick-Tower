using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnBox : MonoBehaviour
{

    public Player player;

    public GameObject[] boxList;

    private GameObject lastPiece;

    void Start()
    {
        SpawnNewBox();
    }

    public void SpawnNewBox()
    {
        int i = Random.Range(0, boxList.Length);
        lastPiece = Instantiate(boxList[i], transform.position, Quaternion.identity);
        lastPiece.GetComponent<Piece>().setPlayer(player);
        lastPiece.GetComponent<Piece>().setSpawner(this);
    }

    public GameObject getLastPiece()
    {
        return lastPiece;
    }

    public Piece getLastPieceComponent()
    {
        return lastPiece.GetComponent<Piece>();
    }
}
