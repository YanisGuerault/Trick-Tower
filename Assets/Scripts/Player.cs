using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int identifiant;

    #region Enum
    public enum Controls { ZQSD, Arrow };
    #endregion

    #region Scene object
    public SpawnBox spawner;
    #endregion

    #region Controls
    public Controls controls;
    public bool commandsEnable = true;
    #endregion

    #region Lives
    bool invicibility;
    [SerializeField] private int nbLives;
    #endregion

    #region Bonus/Malus
    Bonus bonus = null;
    Malus malus = null;
    #endregion

    #region Manager
    GameManager gameManager;
    HudManager hudManager;
    #endregion

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        hudManager = FindObjectOfType<HudManager>();
        nbLives = gameManager.nbLives;
        invicibility = false;
    }


    #region Lives functions
    public void removeLife()
    {
        if (!invicibility)
        {
            nbLives -= 1;
            invicibility = true;
            StartCoroutine("Invicibility");
            hudManager.retireALive(this, nbLives);
        }
        checkLife();
    }

    public void addLife()
    {
        if (nbLives < gameManager.nbLives)
        {
            nbLives += 1;
            hudManager.addALive(this, nbLives);
        }
    }

    public int getLives()
    {
        return nbLives;
    }

    private void checkLife()
    {
        if(nbLives <= 0)
        {
            gameManager.aPlayerDie(this);
        }
    }
    IEnumerator Invicibility()
    {
        yield return new WaitForSeconds(3f);
        invicibility = false;
    }

    #endregion

    #region Controls functions
    void Update()
    {
        //Debug.Log("Player "+ controls +" life :" + nbLives);
        keyControl();
        //spawner.changeSimulate(gameManager.getOnPause());
        Debug.Log("Player : " + controls + " | Bonus : " + bonus + " | Malus : " + malus);
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
                    if (Input.GetKeyDown(KeyCode.T))
                    {
                        ActivateBonus();
                    }
                    if (Input.GetKeyDown(KeyCode.G))
                    {
                        ActivateMalus();
                    }
                    break;
            }
        }

    }

    #endregion

    #region Bonus functions

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
        if (bonus != null)
        {
            bonus.Activate(spawner.getLastPiece());
            hudManager.eraseBonus(this);
            hudManager.eraseMalus(this);
            removeBonus();
            removeMalus();
        }
    }

    public void ActivateMalus()
    {
        if (malus != null)
        {
            malus.Activate(spawner.getLastPiece());
            hudManager.eraseBonus(this);
            hudManager.eraseMalus(this);
            removeBonus();
            removeMalus();
        }
    }
    #endregion
}
