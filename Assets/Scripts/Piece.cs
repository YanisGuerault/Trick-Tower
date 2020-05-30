using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{

    #region Scenes objects
    SpawnBox spawner;
    public Player player;
    #endregion

    #region Physics controls
    float gravityScaleBase = 1.5f;
    float lastPositionY;
    bool grounded;
    public bool allowRotation = true;
    #endregion

    #region Controls falling functions
    private void Start()
    {
        this.gravityScaleBase = this.GetComponent<Rigidbody2D>().gravityScale;
    }

    private void Update()
    {
        if (!grounded)
        {
            lastPositionY = (this.transform.position.y);
        }

        if ((this.transform.position.y - GameObject.FindGameObjectWithTag("Ground").transform.position.y) < 0)
        {

            if(spawner.getLastPiece() == this.gameObject)
            {
                spawner.removeAPiece(this.gameObject);
                spawnNewPiece();
            }
            else
            {
                spawner.removeAPiece(this.gameObject);
            }
  
            ground();
            player.removeLife();

            Destroy(this.gameObject);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!grounded && (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Piece"))
        {
            ground();
            spawnNewPiece();
        }
    }

    //Permet de dire que la pièce à bien atteri
    private void ground()
    {
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 10;
        grounded = true;
    }

    private void spawnNewPiece()
    {
        spawner.SpawnNewBox();
    }

    #endregion

    #region Move pieces functions
    public void movePiece(int direction)
    {
        if (direction > 0 && (transform.position.x + .5f) < (spawner.ground.position.x + spawner.ground.localScale.x + 2))
            transform.position = new Vector3(transform.position.x + .5f, transform.position.y, transform.position.z);
        else if (direction < 0 && (transform.position.x - .5f) > (spawner.ground.position.x - spawner.ground.localScale.x - 2))
            transform.position = new Vector3(transform.position.x - .5f, transform.position.y, transform.position.z);
    }

    public void fastDrop()
    {
        this.GetComponent<Rigidbody2D>().gravityScale += .1f;
    }

    public void resetDrop()
    {
        this.GetComponent<Rigidbody2D>().gravityScale = gravityScaleBase;
    }

    public void rotation()
    {
        if (allowRotation)
        {
            Quaternion rotationAmount = Quaternion.Euler(0, 0, 90);
            transform.rotation *= rotationAmount;
        }
    }

    #endregion

    #region Remove pieces functions

    //Retire la dernière pièce du jeu et en assigne une nouvelle
    public void removeLastPiece()
    {
        spawner.removeAPiece(this.gameObject);
        ground();
        spawnNewPiece();

        Destroy(this.gameObject);
    }

    public void removeLastPieceGrounded()
    {
        GameObject piece = spawner.getLastPieceGrounded();
        spawner.removeAPiece(piece);
        Destroy(piece);
    }

    #endregion

    #region Get/Set functions

    public bool isGrounded()
    {
        return grounded;
    }

    public void setPlayer(Player player)
    {
        this.player = player;
    }

    public void setSpawner(SpawnBox spawner)
    {
        this.spawner = spawner;
    }

    public SpawnBox getSpawner()
    {
        return spawner;
    }

    #endregion

}
