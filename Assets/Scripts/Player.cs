using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Controls { ZQSD, Arrow };
    public int nbLivesStart;
    public Controls controls;
    public SpawnBox spawner;
    public bool commandsEnable = true;
    bool invicibility;
    bool stopPiece = false;
    Bonus bonus = null;
    Malus malus = null;
    GameManager gameManager;


    [SerializeField] private int nbLives;
    // Start is called before the first frame update
    void Start()
    {
        nbLives = nbLivesStart;
        invicibility = false;
        /*bonus = new Liane();
        malus = new Grossisement();*/
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Player "+ controls +" life :" + nbLives);
        keyControl();
        //spawner.changeSimulate(gameManager.getOnPause());
        Debug.Log("Player : " + controls + " | Bonus : " + bonus + " | Malus : " + malus);
    }

    public void removeLife()
    {
        if (!invicibility)
        {
            nbLives -= 1;
            invicibility = true;
            StartCoroutine("Invicibility");
        }
        checkLife();
    }

    private void checkLife()
    {
        if(nbLives <= 0)
        {
            gameManager.endGame();
        }
    }
    IEnumerator Invicibility()
    {
        yield return new WaitForSeconds(3f);
        invicibility = false;
    }

    private void keyControl()
    {
        if (commandsEnable)
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
                    if (Input.GetKeyDown(KeyCode.O))
                    {
                        ActivateBonus();
                    }
                    if (Input.GetKeyDown(KeyCode.L))
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

        if(Input.GetKeyDown(KeyCode.P))
        {
            gameManager.changePauseState();
        }

    }

    public Bonus getBonus()
    {
        return bonus;
    }

    public void setBonus(Bonus bonus)
    {
        this.bonus = bonus;
    }

    public void removeBonus()
    {
        bonus = null;
    }

    public Malus getMalus()
    {
        return malus;
    }

    public void setMalus(Malus malus)
    {
        this.malus = malus;
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
