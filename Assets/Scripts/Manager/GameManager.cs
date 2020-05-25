using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool onPause = false;
    bool coroutineSave = false;
    bool attributeBonusMalus = false;
    List<Player> playerList = new List<Player>();
    public Bonus[] listOfBonus;
    public Malus[] listOfMalus;
    public float taillePlateau = 10;

    private void Start()
    {
        foreach(GameObject p in GameObject.FindGameObjectsWithTag("Spawn Box"))
        {
            playerList.Add(p.GetComponent<Player>());
            bonusAndMalusAttribution(p.GetComponent<Player>());
        }
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
            onPause = !onPause;
            foreach (GameObject piece in GameObject.FindGameObjectsWithTag("Piece"))
            {
                piece.GetComponent<Rigidbody2D>().simulated = !onPause;
            }
            foreach(Player p in playerList)
            {
                p.commandsEnable = !onPause;
            }
            coroutineSave = true;
            StartCoroutine(waitAfterPause());
        }
    }

    public void endGame()
    {

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
}
