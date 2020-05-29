﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liane : Bonus
{
    List<GameObject> objectWithJoin = new List<GameObject>();
    bool activate = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Piece") && collision.rigidbody != null && !(objectWithJoin.Contains(collision.gameObject)) && activate)
        {
            FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
            joint.connectedBody = collision.rigidbody;
            joint.enableCollision = true;
            objectWithJoin.Add(collision.gameObject);
            StartCoroutine(endEffect());
        }
    }

    public override void Activate(GameObject piece)
    {
        piece.AddComponent<Liane>();
    }

    IEnumerator endEffect()
    {
        yield return new WaitForSeconds(0.5f);
        activate = false;
    }
}
