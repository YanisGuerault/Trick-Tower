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
    List<Bonus> bonus = new List<Bonus>();


    private int nbLives;
    // Start is called before the first frame update
    void Start()
    {
        nbLives = nbLivesStart;
        invicibility = false;
        bonus.Add(new Ice());
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
                    Debug.Log("Activate bonus");
                    foreach (Bonus b in bonus)
                    {
                        b.Activate(spawner.getLastPiece());
                    }
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

    public List<Bonus> getBonusList()
    {
        return bonus;
    }

    public void removeBonus()
    {
        bonus = new List<Bonus>();
    }

}
