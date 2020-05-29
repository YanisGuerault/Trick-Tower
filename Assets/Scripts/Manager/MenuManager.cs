using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Canvas canvas;
    public GameObject CreditPanel;
    public GameObject MenuPanel;
    public GameObject SelectionPanel;
    public GameObject activePanel;

    private void Start()
    {
        activePanel = MenuPanel;
        CreditPanel.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Menu();
        }
    }
    public void Play1J()
    {
        SceneManager.LoadScene("GameScene1J");
    }

    public void Play2J()
    {
        SceneManager.LoadScene("GameScene2J");
    }

    public void Quit()
    {
        Application.Quit(0);
    }

    public void Menu()
    {
        if(activePanel != MenuPanel)
        {
            activePanel.SetActive(false);
            MenuPanel.SetActive(true);
            activePanel = MenuPanel;
        }
    }

    public void Credits()
    {
        if (activePanel != CreditPanel)
        {
            activePanel.SetActive(false);
            CreditPanel.SetActive(true);
            activePanel = CreditPanel;
        }
    }

    public void Play()
    {
        if (activePanel != SelectionPanel)
        {
            activePanel.SetActive(false);
            SelectionPanel.SetActive(true);
            activePanel = SelectionPanel;
        }
    }
}
