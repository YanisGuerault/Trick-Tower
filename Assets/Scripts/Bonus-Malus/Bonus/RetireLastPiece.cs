﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetireLastPiece : Bonus
{
    public override void Activate(GameObject piece)
    {
        piece.GetComponent<Piece>().removeLastPieceGrounded();
    }
}
