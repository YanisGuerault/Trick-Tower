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


    private void Start()
    {
        this.gravityScaleBase = this.GetComponent<Rigidbody2D>().gravityScale;
    }
    private void Update()
    {
        if (!grounded)
        {
            keyControl();
            lastPositionY = (this.transform.position.y);
        }

        if ((this.transform.position.y - GameObject.FindGameObjectWithTag("Ground").transform.position.y) < 0)
        {
            // Remove bloc
            // Retire one life
            // 

            Destroy(this.gameObject);
            ground();
            player.removeLife();

            if(spawner.getLastPiece() == this.gameObject)
            {
                spawnNewPiece();
            }

        }
    }

    private void keyControl()
    {
        switch (player.controls)
        {
            case Player.Controls.Arrow:
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    rotation();
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    this.GetComponent<Rigidbody2D>().gravityScale += .1f;
                }
                else
                {
                    this.GetComponent<Rigidbody2D>().gravityScale = gravityScaleBase;
                }

                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    transform.position = new Vector3(transform.position.x + .5f, transform.position.y, transform.position.z);
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    transform.position = new Vector3(transform.position.x - .5f, transform.position.y, transform.position.z);
                }
                break;
            default:
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    rotation();
                }

                if (Input.GetKey(KeyCode.S))
                {
                    this.GetComponent<Rigidbody2D>().gravityScale += .1f;
                }
                else
                {
                    this.GetComponent<Rigidbody2D>().gravityScale = gravityScaleBase;
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    transform.position = new Vector3(transform.position.x + .5f, transform.position.y, transform.position.z);
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    transform.position = new Vector3(transform.position.x - .5f, transform.position.y, transform.position.z);
                }
                break;
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

    private void rotation()
    {
        Quaternion rotationAmount = Quaternion.Euler(90, 0, 0);
        transform.rotation *= rotationAmount;
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



}
