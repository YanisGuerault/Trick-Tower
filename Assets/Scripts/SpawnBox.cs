using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnBox : MonoBehaviour
{

    public GameObject[] boxList;

    void Start()
    {
        SpawnNewBox();
    }

    public void SpawnNewBox()
    {
        int i = Random.Range(0, boxList.Length);
        Instantiate(boxList[i], transform.position, Quaternion.identity);
    }
}
