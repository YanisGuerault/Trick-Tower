using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Enum
    public enum State { Play, Pause, End};
    #endregion

    #region Game State Controls
    bool onPause = false;
    bool coroutineSave = false;
    [SerializeField] State actualState = State.Play;
    #endregion

    #region Pieces controls
    public int nbPiecesBase = 64;
    public int[] nbPiecesAvailable;
    #endregion

    #region Lifes controls
    public int nbLives;
    int nbPlayersAlives;
    #endregion

    #region Bonus/Malus
    public Bonus[] listOfBonus;
    public Malus[] listOfMalus;
    #endregion

    #region Players controls
    [SerializeField] List<Player> playerList = new List<Player>();
    public int nbPlayers = 2;
    public SpawnBox[] spawnBox;
    #endregion

    #region Managers
    private HudManager hudManager;
    #endregion


    #region Start function
    public static bool isReady = false;


    private void Start()
    {
        hudManager = FindObjectOfType<HudManager>();

        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Spawn Box"))
        {
            playerList.Add(p.GetComponent<Player>());
            //bonusAndMalusAttribution(p.GetComponent<Player>());
            StartCoroutine(reloadBonus(p.GetComponent<Player>()));
        }

        nbPiecesAvailable = new int[nbPlayers];
        for(int i = 0; i < nbPlayers; i++)
        {
            nbPiecesAvailable[i] = nbPiecesBase;
        }

        spawnBox = new SpawnBox[nbPlayers];
        int y = 0;
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Spawn Box"))
        {
            spawnBox[y] = obj.GetComponent<SpawnBox>();
            y++;
        }

        hudManager.HUDStart();
        nbPlayersAlives = playerList.Count;

        GameManager.isReady = true;

    }
    #endregion

    #region Controls functions
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            changePauseState();
        }

        /*if(hudManager.pauseActive && Input.GetKeyDown(KeyCode.Escape))
        {
            changePauseState();
        }*/
    }
    #endregion

    #region Bonus/Malus Functions

    private void bonusAndMalusAttribution(Player player)
    {
        if (actualState == State.Play && player.getBonus() == null && player.getMalus() == null)
        {
            if(nbPlayers > 1)
            {
                int malusRandom = UnityEngine.Random.Range(0, listOfMalus.Length);
                player.setMalus(listOfMalus[malusRandom]);
                hudManager.changeMalus(player, listOfMalus[malusRandom]);
            }

            int bonusRandom = UnityEngine.Random.Range(0, listOfBonus.Length);
            player.setBonus(listOfBonus[bonusRandom]);
            hudManager.changeBonus(player, listOfBonus[bonusRandom]);
            
        }

        StartCoroutine(reloadBonus(player));
    }

    IEnumerator reloadBonus(Player player)
    {
        yield return new WaitForSeconds(15f);
        bonusAndMalusAttribution(player);
    }

    #endregion

    #region Pause functions
    public bool getOnPause()
    {
        return onPause;
    }

    public void changePauseState()
    {
        if (!coroutineSave)
        {
            actualState = State.Pause;
            onPause = !onPause;

            startStopAllPhysics(!onPause);

            if(onPause)
            {
                actualState = State.Pause;
            }
            else
            {
                actualState = State.Play;
            }

            coroutineSave = true;
            hudManager.Pause();
            StartCoroutine(waitAfterPause());
        }
    }

    IEnumerator waitAfterPause()
    {
        yield return new WaitForSeconds(0.5f);
        coroutineSave = false;
    }

    #endregion

    #region Ending Game Functions

    public void endGame()
    {
        if(nbPlayers == 1)
        {
            if (nbPiecesAvailable[0] <= 0)
            {
                hudManager.WinPane();
                actualState = State.End;
                startStopAllPhysics(false);
            }
            else
            {
                hudManager.LoosePane();
                actualState = State.End;
                startStopAllPhysics(false);
            }
        }
        else
        {
            List<Player> playersAlive = new List<Player>();
            for(int i = 0; i < nbPlayers; i++)
            {
                if(playerList[i].getLives() > 0)
                {
                    playersAlive.Add(playerList[i]);
                }
            }
            if(playersAlive.Count == 1)
            {
                hudManager.WinPane(playersAlive[0]);
                actualState = State.End;
                startStopAllPhysics(false);
            }
            else
            {
                for (int i = 0; i < nbPlayers; i++)
                {
                    if (nbPiecesAvailable[playerList[i].identifiant] <= 0)
                    {
                        hudManager.WinPane(playerList[i]);
                        actualState = State.End;
                        startStopAllPhysics(false);
                        break;
                    }
                }
            }
            //Si le code arrive ici c'est qu'il y a un soucis
        }
    }

    public void endGame(Player p)
    {
        hudManager.WinPane(p);
        actualState = State.End;
        startStopAllPhysics(false);
    }

    public void aPlayerDie(Player p)
    {
        nbPlayersAlives -= 1;
        endGame();
    }

    #endregion

    #region Physcis function
    private void startStopAllPhysics(bool condition)
    {
        foreach (SpawnBox sb in spawnBox)
        {
            sb.changeSimulate(condition);
        }
        foreach (Player p in playerList)
        {
            p.commandsEnable = condition;
        }
    }

    #endregion

    #region Piece function

    public bool retireAPiece(Player p)
    {
        int playerIdx = getPlayerList().IndexOf(p);
        if(nbPiecesAvailable[playerIdx] <= 0 )
        {
            endGame(p);
            return false;
        }
        nbPiecesAvailable[playerIdx] -= 1;
        hudManager.changePieceCompteur(p, nbPiecesAvailable[playerIdx]);
        return true;
    }

    #endregion

    #region Getter/Setter function
    public List<Player> getPlayerList()
    {
        return playerList;
    }
    #endregion
}
