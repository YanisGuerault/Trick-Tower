using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grossisement : Malus
{
    private Vector3 velocity = Vector3.zero;

    protected override void Activation(GameObject piece)
    {
        Vector3 newSize = new Vector3(piece.transform.localScale.x * 2, piece.transform.localScale.y * 2, piece.transform.localScale.z * 2);
        //piece.transform.localScale = Vector3.SmoothDamp(piece.transform.localScale, newSize, ref velocity, 1f);
        piece.transform.localScale = newSize;
        piece.AddComponent<Grossisement>();
    }
}
