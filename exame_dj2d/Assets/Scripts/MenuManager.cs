using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject currentActivePanel;

    private void Awake()
    {
        GameObject[] menuPanels = GameObject.FindGameObjectsWithTag("Menu");
        foreach (GameObject panel in menuPanels)
        {
            panel.SetActive(false);
        }
        currentActivePanel.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SetActivePanel(GameObject panel)
    {
        currentActivePanel.SetActive(false);
        currentActivePanel = panel;
        currentActivePanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
