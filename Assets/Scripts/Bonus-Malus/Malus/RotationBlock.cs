using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBlock : Malus
{
    public Material material = null;
    protected override void Activation(GameObject piece)
    {
        piece.GetComponent<Piece>().allowRotation = false;
        piece.AddComponent<RotationBlock>();
        changeTexture(piece);
    }

    public void changeTexture(GameObject piece)
    {
        piece.transform.Find("default").GetComponent<MeshRenderer>().material = material;
    }
}
