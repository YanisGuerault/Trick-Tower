using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State { Play, Pause, End};
    bool onPause = false;
    bool coroutineSave = false;
    bool attributeBonusMalus = false;
    [SerializeField] List<Player> playerList = new List<Player>();
    [SerializeField] State actualState = State.Play;
    public int nbPlayers = 2;
    public Bonus[] listOfBonus;
    public Malus[] listOfMalus;
    public float traySize = 10;
    public int nbPiecesBase = 64;
    public int[] nbPiecesAvailable;
    public SpawnBox[] spawnBox;
    public int nbLives;

    public static bool isReady = false;

    private void Start()
    {
        foreach(GameObject p in GameObject.FindGameObjectsWithTag("Spawn Box"))
        {
            playerList.Add(p.GetComponent<Player>());
            bonusAndMalusAttribution(p.GetComponent<Player>());
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

        FindObjectOfType<HudManager>().HUDStart();

        GameManager.isReady = true;
    }

    private void FixedUpdate()
    {
        
    }

    private void bonusAndMalusAttribution(Player player)
    {
        if(!attributeBonusMalus)
        {
            StartCoroutine(reloadBonus(player));
        } else {
            int bonusRandom = UnityEngine.Random.Range(0, listOfBonus.Length);
            int malusRandom = UnityEngine.Random.Range(0, listOfMalus.Length);
            player.setBonus(listOfBonus[bonusRandom]);
            player.setMalus(listOfMalus[malusRandom]);
        }
    }

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
            StartCoroutine(waitAfterPause());
        }
    }

    public void endGame()
    {
        actualState = State.End;
        startStopAllPhysics(false);
    }

    IEnumerator waitAfterPause()
    {
        yield return new WaitForSeconds(1f);
        coroutineSave = false;
    }

    IEnumerator reloadBonus(Player player)
    {
        yield return new WaitForSeconds(15f);
        attributeBonusMalus = true;
        bonusAndMalusAttribution(player);
    }

    public bool retireAPiece(Player p)
    {
        int playerIdx = getPlayerList().IndexOf(p);
        if(nbPiecesAvailable[playerIdx] <= 0 )
        {
            return false;
        }
        nbPiecesAvailable[playerIdx] -= 1;
        return true;
    }

    public List<Player> getPlayerList()
    {
        return playerList;
    }

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
}
