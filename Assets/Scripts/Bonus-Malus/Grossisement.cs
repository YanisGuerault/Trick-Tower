using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grossisement : Bonus
{
    public float scaleIncrease = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Activate()
    {
        Debug.Log("It's work");
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Piece"))
        {
            g.transform.localScale += new Vector3(scaleIncrease, scaleIncrease, scaleIncrease);
        }
    }
}
