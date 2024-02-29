using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject panelMainMenu;
    public GameObject panelPause;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(panelMainMenu.transform.GetChild(0).gameObject);
    }

    public void MyLoadScene(int idScene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(idScene);
        Time.timeScale = 1.0f;

        PiedestalController.clear1 = false;
        PiedestalController.clear2 = false;
        PiedestalController.canPut1 = false;
        PiedestalController.canPut1 = false;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
