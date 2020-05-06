using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    float gravityScaleBase;
    float lastPositionY;
    bool grounded;

    private void Start()
    {
        this.gravityScaleBase = this.GetComponent<Rigidbody2D>().gravityScale;
    }
    private void FixedUpdate()
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

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position = new Vector3(transform.position.x + .1f, transform.position.y, transform.position.z);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position = new Vector3(transform.position.x - .1f, transform.position.y, transform.position.z);
            }


            if ((this.GetComponent<Transform>().position.y) < -50)
            {
                // Remove bloc
                // Retire one life
                // 
            }

            lastPositionY = (this.GetComponent<Transform>().position.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!grounded && (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Piece"))
        {
            grounded = true;
            GameObject.FindGameObjectWithTag("Spawn Box").GetComponent<SpawnBox>().SpawnNewBox();
        }
    }

}
