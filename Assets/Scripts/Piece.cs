using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    
    SpawnBox spawner;
    float gravityScaleBase;
    float lastPositionY;
    bool grounded;
    public Player player;
    public bool allowRotation = true;


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
            // Remove bloc
            // Retire one life
            // 

            spawner.removeAPiece(this.gameObject);
            ground();
            player.removeLife();

            if(spawner.getLastPiece() == this.gameObject)
            {
                spawnNewPiece();
            }

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

    private void ground()
    {
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 10;
        grounded = true;
    }

    private void spawnNewPiece()
    {
        spawner.SpawnNewBox();
    }

    public void removeLastPiece()
    {
        spawner.removeAPiece(this.gameObject);
        ground();
        spawnNewPiece();

        Destroy(this.gameObject);
    }

    public void rotation()
    {
        if (allowRotation)
        {
            Quaternion rotationAmount = Quaternion.Euler(0, 0, 90);
            transform.rotation *= rotationAmount;
        }
    }

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

    public void movePiece(int direction)
    {
        if(direction > 0)
            transform.position = new Vector3(transform.position.x + .5f, transform.position.y, transform.position.z);
        else if(direction < 0)
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



}
