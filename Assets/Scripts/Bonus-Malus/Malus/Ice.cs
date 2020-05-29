using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : Malus
{
    public PhysicsMaterial2D iceMaterial;
    public Material material = null;
    // Start is called before the first frame update
    protected override void Activation(GameObject piece)
    {
        foreach(BoxCollider2D collid in piece.GetComponents<BoxCollider2D>())
        {
            collid.sharedMaterial = iceMaterial;
        }
        piece.GetComponent<Rigidbody2D>().sharedMaterial = iceMaterial;
        piece.AddComponent<Ice>();
        changeTexture(piece);
    }

    public void changeTexture(GameObject piece)
    {
        piece.transform.Find("default").GetComponent<MeshRenderer>().material = material;
    }
}
