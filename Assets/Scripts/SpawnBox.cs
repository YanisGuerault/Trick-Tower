using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnBox : MonoBehaviour
{

    public Player player;

    public GameObject[] boxList;

    private List<GameObject> pieces = new List<GameObject>();

    void Start()
    {
        SpawnNewBox();
    }

    public void SpawnNewBox()
    {
        int i = Random.Range(0, boxList.Length);
        pieces.Add(Instantiate(boxList[i], transform.position, Quaternion.identity));
        getLastPiece().GetComponent<Piece>().setPlayer(player);
        getLastPiece().GetComponent<Piece>().setSpawner(this);
    }

    public GameObject getLastPiece()
    {
        return pieces[pieces.Count-1];
    }

    public Piece getLastPieceComponent()
    {
        return getLastPiece().GetComponent<Piece>();
    }
}
