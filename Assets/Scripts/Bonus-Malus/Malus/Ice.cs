using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : Malus
{
    public PhysicsMaterial2D iceMaterial;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Activation(GameObject piece)
    {
        piece.GetComponent<BoxCollider2D>().sharedMaterial = iceMaterial;
        piece.GetComponent<Rigidbody2D>().sharedMaterial = iceMaterial;
    }
}
