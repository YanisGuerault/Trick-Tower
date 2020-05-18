using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : Bonus
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

    public override void Activate(GameObject piece)
    {
        piece.GetComponent<BoxCollider2D>().sharedMaterial = iceMaterial;
        piece.GetComponent<Rigidbody2D>().sharedMaterial = iceMaterial;
    }
}
