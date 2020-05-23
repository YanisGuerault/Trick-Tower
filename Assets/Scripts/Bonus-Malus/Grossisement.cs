using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grossisement : Malus
{
    public float scaleIncrease = 1;
    private Vector3 velocity = Vector3.zero;

    protected override void Activation(GameObject piece)
    {
        piece.transform.localScale = Vector3.SmoothDamp(piece.transform.localScale, new Vector3(scaleIncrease, scaleIncrease, scaleIncrease), ref velocity, 1f);
    }
}
