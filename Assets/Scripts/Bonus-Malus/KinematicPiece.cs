using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicPiece : Bonus
{
    private bool activation = false;
    private Rigidbody2D piece = null;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Piece") && collision.rigidbody != null && activation)
        {
            piece.simulated = false;
        }
        activation = false;
    }

    public override void Activate(GameObject piece)
    {
        activation = true;
        this.piece = piece.GetComponent<Rigidbody2D>();
    }
}
