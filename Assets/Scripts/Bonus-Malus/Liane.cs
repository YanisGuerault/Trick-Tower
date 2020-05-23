using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liane : Bonus
{
    List<GameObject> objectWithJoin = new List<GameObject>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Piece") && collision.rigidbody != null && !(objectWithJoin.Contains(collision.gameObject)))
        {
            FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
            joint.connectedBody = collision.rigidbody;
            joint.enableCollision = true;
            objectWithJoin.Add(collision.gameObject);
        }
    }

    public override void Activate(GameObject piece)
    {
        piece.AddComponent<Liane>();
    }
}
