using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Malus : MonoBehaviour
{
    public Sprite icone;
    public void Activate(GameObject piece)
    {
        foreach(GameObject spawn in GameObject.FindGameObjectsWithTag("Spawn Box"))
        {
            SpawnBox box = spawn.GetComponent<SpawnBox>();

            if (box.getLastPieceComponent().player != piece.GetComponent<Piece>().player)
            {
                Activation(box.getLastPiece());
            }
        }
    }

    protected abstract void Activation(GameObject piece);
}
