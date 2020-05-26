using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Canvas canvas;
    public GameObject CreditPanel;
    public GameObject MenuPanel;
    public GameObject activePanel;

    private void Start()
    {
        activePanel = MenuPanel;
        CreditPanel.SetActive(false);
    }
    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Quit()
    {
        Application.Quit(0);
    }

    public void ChangePanel()
    {
        activePanel.SetActive(false);
        if(activePanel == CreditPanel)
        {
            activePanel = MenuPanel;
        }
        else
        {
            activePanel = CreditPanel;
        }
        activePanel.SetActive(true);
        
    }
}
