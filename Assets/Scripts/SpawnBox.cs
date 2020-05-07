using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnBox : MonoBehaviour
{

    public GameObject[] boxList;

    private GameObject lastPiece;

    void Start()
    {
        SpawnNewBox();
    }

    public void SpawnNewBox()
    {
        int i = Random.Range(0, boxList.Length);
        lastPiece = Instantiate(boxList[i], transform.position, Quaternion.identity);
    }

    public GameObject getLastPiece()
    {
        return lastPiece;
    }
}
