using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicPiece : Bonus
{
    public Material material = null;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Piece") && collision.rigidbody != null)
        {
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public override void Activate(GameObject piece)
    {
        //this.piece = piece.GetComponent<Rigidbody2D>();
        piece.AddComponent<KinematicPiece>();
        changeTexture(piece);
    }

    public void changeTexture(GameObject piece)
    {
        piece.transform.Find("default").GetComponent<MeshRenderer>().material = material;
    }
}
