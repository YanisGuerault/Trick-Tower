using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liane : Bonus
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Piece") && collision.rigidbody != null)
        {
            FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
            joint.connectedBody = collision.rigidbody;
            joint.enableCollision = true;
        }
    }

    public override void Activate(GameObject piece)
    {
        piece.AddComponent<Liane>();
    }
}
