using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBlock : Malus
{
    protected override void Activation(GameObject piece)
    {
        piece.GetComponent<Piece>().allowRotation = false;
        piece.AddComponent<RotationBlock>();
    }
}
