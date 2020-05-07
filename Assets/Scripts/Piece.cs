using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    SpawnBox spawner;
    float gravityScaleBase;
    float lastPositionY;
    bool grounded;


    private void Start()
    {
        this.gravityScaleBase = this.GetComponent<Rigidbody2D>().gravityScale;
        spawner = GameObject.FindGameObjectWithTag("Spawn Box").GetComponent<SpawnBox>();
    }
    private void Update()
    {
        if (!grounded)
        {

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

            lastPositionY = (this.transform.position.y);
        }

        if ((this.transform.position.y - GameObject.FindGameObjectWithTag("Ground").transform.position.y) < 0)
        {
            // Remove bloc
            // Retire one life
            // 

            Destroy(this.gameObject);
            ground();

            if(spawner.getLastPiece() == this.gameObject)
            {
                spawnNewPiece();
            }

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
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 2;
        grounded = true;
    }

    private void spawnNewPiece()
    {
        spawner.SpawnNewBox();
    }

    public bool isGrounded()
    {
        return grounded;
    }

}
