using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Controls { ZQSD, Arrow };
    public int nbLivesStart;
    public Controls controls;
    public SpawnBox spawner;
    bool invicibility;
    Bonus bonus;
    Malus malus;


    private int nbLives;
    // Start is called before the first frame update
    void Start()
    {
        nbLives = nbLivesStart;
        invicibility = false;
        bonus = new Liane();
        malus = new Grossisement();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Player "+ controls +" life :" + nbLives);
        keyControl();
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

    private void keyControl()
    {
        switch (controls)
        {
            case Player.Controls.Arrow:
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    spawner.getLastPieceComponent().rotation();
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    spawner.getLastPieceComponent().fastDrop();
                }
                else
                {
                    spawner.getLastPieceComponent().resetDrop();
                }

                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    spawner.getLastPieceComponent().movePiece(1);
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    spawner.getLastPieceComponent().movePiece(-1);
                }
                if (Input.GetKeyDown(KeyCode.P))
                {
                    ActivateBonus();
                }
                if(Input.GetKeyDown(KeyCode.M))
                {
                    ActivateMalus();
                }
                break;
            default:
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    spawner.getLastPieceComponent().rotation();
                }

                if (Input.GetKey(KeyCode.S))
                {
                    spawner.getLastPieceComponent().fastDrop();
                }
                else
                {
                    spawner.getLastPieceComponent().resetDrop();
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    spawner.getLastPieceComponent().movePiece(1);
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    spawner.getLastPieceComponent().movePiece(-1);
                }
                break;
        }

    }

    public Bonus getBonus()
    {
        return bonus;
    }

    public void removeBonus()
    {
        bonus = null;
    }

    public Malus getMalus()
    {
        return malus;
    }

    public void removeMalus()
    {
        malus = null;
    }

    public void ActivateBonus()
    {
        bonus.Activate(spawner.getLastPiece());
        removeBonus();
        removeMalus();
    }

    public void ActivateMalus()
    {
        malus.Activate(spawner.getLastPiece());
        removeBonus();
        removeMalus();
    }

}
