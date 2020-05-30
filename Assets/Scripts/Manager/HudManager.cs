using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    #region Parameters
    public bool pauseActive = false;
    public Sprite coeurVie;
    public Sprite coeurMort;
    public Dictionary<int, GameObject> getRootHUD = new Dictionary<int, GameObject>();
    public Canvas root;
    public int nbLives;

    #endregion

    #region Background
    public Sprite[] bgList;
    public SpriteRenderer bg;
    #endregion

    #region Panels
    public GameObject pausePanel;
    public GameObject winLoose;
    #endregion

    #region Managers
    GameManager gameManager;
    #endregion

    #region Start function

    public void HUDStart()
    {
        gameManager = FindObjectOfType<GameManager>();

        foreach(Player p in gameManager.getPlayerList())
        {
            getRootHUD.Add(p.identifiant, root.gameObject.transform.Find("Joueurs").Find("Joueur" + p.identifiant).gameObject);
            eraseBonus(p);
            eraseMalus(p);
        }

        if(pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
        }

        int i = Random.Range(0, bgList.Length);
        bg.sprite = bgList[i];
        winLoose.SetActive(false);
    }

    #endregion

    #region Life functions

    public void retireALive(Player p, int nbLivesRestant)
    {
        GameObject parentRoot = null;
        getRootHUD.TryGetValue(p.identifiant, out parentRoot);
        Transform coeurRoot = parentRoot.transform.Find("Coeur");
        for(int i = 0; i < coeurRoot.childCount; i++)
        {
            if(coeurRoot.GetChild(i).name.Split(' ')[1] == (nbLivesRestant+1).ToString())
            {
                coeurRoot.GetChild(i).gameObject.GetComponent<Image>().sprite = coeurMort;
            }
        }
    }

    public void addALive(Player p, int nbLivesRestant)
    {
        GameObject parentRoot = null;
        getRootHUD.TryGetValue(p.identifiant, out parentRoot);
        Transform coeurRoot = parentRoot.transform.Find("Coeur");
        for (int i = 0; i < coeurRoot.childCount; i++)
        {
            if (coeurRoot.GetChild(i).name.Split(' ')[1] == (nbLivesRestant).ToString())
            {
                coeurRoot.GetChild(i).gameObject.GetComponent<Image>().sprite = coeurVie;
            }
        }
    }

    #endregion

    #region Compteur functions

    public void changePieceCompteur(Player p, int nbPiecesRestant)
    {
        GameObject parentRoot = null;
        getRootHUD.TryGetValue(p.identifiant, out parentRoot);
        Transform compteur = parentRoot.transform.Find("Compteur");
        compteur.gameObject.GetComponent<Text>().text = nbPiecesRestant.ToString(); 
    }

    #endregion

    #region Bonus functions

    public void changeBonus(Player p, Bonus b)
    {
        GameObject parentRoot = null;
        getRootHUD.TryGetValue(p.identifiant, out parentRoot);
        Transform bonus = parentRoot.transform.Find("Bonus");
        bonus.gameObject.GetComponent<Image>().sprite = b.icone;
        bonus.gameObject.GetComponent<Image>().enabled = true;
    }

    public void changeMalus(Player p, Malus m)
    {
        GameObject parentRoot = null;
        getRootHUD.TryGetValue(p.identifiant, out parentRoot);
        Transform malus = parentRoot.transform.Find("Malus");
        malus.gameObject.GetComponent<Image>().sprite = m.icone;
        malus.gameObject.GetComponent<Image>().enabled = true;
    }

    public void eraseBonus(Player p)
    {
        GameObject parentRoot = null;
        getRootHUD.TryGetValue(p.identifiant, out parentRoot);
        Transform bonus = parentRoot.transform.Find("Bonus");
        bonus.gameObject.GetComponent<Image>().enabled = false;
        bonus.gameObject.GetComponent<Image>().sprite = null;
        
    }

    public void eraseMalus(Player p)
    {
        GameObject parentRoot = null;
        getRootHUD.TryGetValue(p.identifiant, out parentRoot);
        Transform bonus = parentRoot.transform.Find("Malus");
        bonus.gameObject.GetComponent<Image>().enabled = false;
        bonus.gameObject.GetComponent<Image>().sprite = null;
    }

    #endregion

    #region OnClick Functions
    public void Pause()
    {
        pauseActive = !pauseActive;
        pausePanel.SetActive(pauseActive);
    }

    public void Retour()
    {
        gameManager.changePauseState();
    }

    public void Menu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void WinPane(Player p)
    {
        winLoose.SetActive(true);
        winLoose.transform.Find("Name").GetComponent<Text>().text = "Player " + p.identifiant + " win";
    }

    public void WinPane()
    {
        winLoose.SetActive(true);
        winLoose.transform.Find("Name").GetComponent<Text>().text = "You win";
    }

    public void LoosePane()
    {
        winLoose.SetActive(true);
        winLoose.transform.Find("Name").GetComponent<Text>().text = "You Loose";
    }
    #endregion

    #region Getter/Setter

    public Canvas GetCanvas()
    {
        return root;
    }

    #endregion
}
