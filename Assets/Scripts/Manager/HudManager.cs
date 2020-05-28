using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{

    //Dictionnaire de dictionnaire, chemin : Dictionnaire all -> Dictionnaire par player -> Tableau d'images bonus ou lives
    // Pour all, clé = int (identifiant du player), pour player, clé = int (0 = lives et 1 = bonus)
    //public Dictionary<int,Dictionary<int, Image[]>> all = new Dictionary<int, Dictionary<int, Image[]>>();
    public Sprite coeurVie;
    public Sprite coeurMort;
    public Dictionary<int, GameObject> getRootHUD = new Dictionary<int, GameObject>();
    public Canvas root;
    public int nbLives;

    GameManager gameManager;

    public void HUDStart()
    {
        gameManager = FindObjectOfType<GameManager>();

        foreach(Player p in gameManager.getPlayerList())
        {
            getRootHUD.Add(p.identifiant, root.gameObject.transform.Find("Joueur" + p.identifiant).gameObject);
            eraseBonus(p);
            eraseMalus(p);
        }
    }

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

    public void changePieceCompteur(Player p, int nbPiecesRestant)
    {
        GameObject parentRoot = null;
        getRootHUD.TryGetValue(p.identifiant, out parentRoot);
        Transform compteur = parentRoot.transform.Find("Compteur");
        compteur.gameObject.GetComponent<Text>().text = nbPiecesRestant.ToString(); 
    }

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

    public Canvas GetCanvas()
    {
        return root;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
