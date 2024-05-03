using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;
using _3CFeel.Controller;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public GameObject panelMainMenu;
    public GameObject pauseMenuUI;

    public PlayerController thePlayer;

    public static bool gamIsPaused = false;

    private void Start()
    {
        if(panelMainMenu != null)
        EventSystem.current.SetSelectedGameObject(panelMainMenu.transform.GetChild(0).gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //blur.focalLength = new ClampedFloatParameter(1, 0, 300);

        if (Input.GetButtonDown("Cancel") && !InventoryController.noUseInventory)
        {

            if (gamIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        CameraController.noUseCamera = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamIsPaused = false;

        //thePlayer.isDead = false;
    }

    public void Pause()
    {
        CameraController.noUseCamera = true;
        pauseMenuUI.SetActive(true);
        gamIsPaused = true;
        EventSystem.current.SetSelectedGameObject(pauseMenuUI.transform.GetChild(1).gameObject);
        //thePlayer.isDead = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        gamIsPaused = false;
        pauseMenuUI.SetActive(false);
        CameraController.noUseCamera = false;
        /*SceneManager.LoadScene("menueStart")*/
        ;
    }

    public void MyLoadScene(int idScene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(idScene);
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
