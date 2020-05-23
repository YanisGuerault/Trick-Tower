using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool onPause = false;
    bool coroutineSave = false;
    List<Player> playerList = new List<Player>();

    private void Start()
    {
        foreach(GameObject p in GameObject.FindGameObjectsWithTag("Spawn Box"))
        {
            playerList.Add(p.GetComponent<Player>());
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

    IEnumerator waitAfterPause()
    {
        yield return new WaitForSeconds(1f);
        coroutineSave = false;
    }
}
