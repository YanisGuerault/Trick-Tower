using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnBox : MonoBehaviour
{

    public Player player;
    public Transform ground;

    public GameObject[] boxList;

    private List<GameObject> pieces = new List<GameObject>();

    public float linearDrag = 15;
    public float gravityScale = 1.5f;

    GameManager gameManager;
    void Start()
    {
        SpawnNewBox();
    }

    public void SpawnNewBox()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager.retireAPiece(player))
        {
            int i = Random.Range(0, boxList.Length);
            pieces.Add(Instantiate(boxList[i], transform.position, Quaternion.identity));
            getLastPiece().GetComponent<Piece>().setPlayer(player);
            getLastPiece().GetComponent<Piece>().setSpawner(this);
            getLastPiece().GetComponent<Rigidbody2D>().drag = linearDrag;
            getLastPiece().GetComponent<Rigidbody2D>().gravityScale = gravityScale;

        }
    }

    public GameObject getLastPiece()
    {
        return pieces[pieces.Count-1];
    }

    public Piece getLastPieceComponent()
    {
        return getLastPiece().GetComponent<Piece>();
    }

    public void removeAPiece(GameObject piece)
    {
        pieces.Remove(piece);
    }

    public void changeSimulate(bool condition)
    {
        foreach(GameObject piece in pieces)
        {
            piece.GetComponent<Rigidbody2D>().simulated = condition;
        }
    }
}
