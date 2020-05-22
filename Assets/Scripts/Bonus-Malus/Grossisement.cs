using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grossisement : Malus
{
    public float scaleIncrease = 1;

    protected override void Activation(GameObject piece)
    {
         piece.transform.localScale += new Vector3(scaleIncrease, scaleIncrease, scaleIncrease);
    }
}
