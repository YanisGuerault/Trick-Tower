using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Controls { ZQSD, Arrow };
    public int nbLivesStart;
    public Controls controls;
    bool invicibility;

    private int nbLives;
    // Start is called before the first frame update
    void Start()
    {
        nbLives = nbLivesStart;
        invicibility = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Player "+ controls +" life :" + nbLives);
    }

    public void removeLife()
    {
        if (!invicibility)
        {
            nbLives -= 1;
            checkLife();
            invicibility = true;
            StartCoroutine("Invicibility");
        }
    }

    private void checkLife()
    {
        //TODO
    }
    IEnumerator Invicibility()
    {
        yield return new WaitForSeconds(3f);
        invicibility = false;
    }

}
