using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLife : Bonus
{
    public override void Activate(GameObject piece)
    {
        piece.GetComponent<Piece>().player.addLife();
    }
}
