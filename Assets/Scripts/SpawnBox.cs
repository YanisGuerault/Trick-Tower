using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnBox : MonoBehaviour
{
    #region Private functions
    private List<GameObject> pieces = new List<GameObject>();
    #endregion

    #region Scene objects
    public Player player;
    public Transform ground;
    public GameObject[] boxList;
    #endregion

    #region Physicals parameters
    public float linearDrag = 15;
    public float gravityScale = 1.5f;
    #endregion

    #region Manager
    GameManager gameManager;
    #endregion

    #region Spawn piece functions
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
    #endregion

    #region Get Pieces functions

    public GameObject getLastPiece()
    {
        return pieces[pieces.Count-1];
    }

    public Piece getLastPieceComponent()
    {
        return getLastPiece().GetComponent<Piece>();
    }

    public GameObject getLastPieceGrounded()
    {
        return pieces[pieces.Count - 2];
    }

    #endregion

    #region Others functions

    public void removeAPiece(GameObject piece)
    {
        pieces.Remove(piece);
    }

    //Permet d'arrêter la simulation des pièces, lors d'une pause par exemple
    public void changeSimulate(bool condition)
    {
        foreach(GameObject piece in pieces)
        {
            piece.GetComponent<Rigidbody2D>().simulated = condition;
        }
    }

    #endregion
}
