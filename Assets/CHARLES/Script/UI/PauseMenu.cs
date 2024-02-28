using _3CFeel.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public PlayerController thePlayer;

    public static bool gamIsPaused = false;

    public GameObject pauseMenuUI;

    private void Awake()
    {
        thePlayer = GameObject.FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !InventoryController.noUseInventory)
        {

            if (gamIsPaused)
            {
                CameraController.noUseCamera = false;
                Resume();
            }
            else
            {
                CameraController.noUseCamera = true;
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamIsPaused = false;

        //thePlayer.isDead = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamIsPaused = true;
        EventSystem.current.SetSelectedGameObject(pauseMenuUI.transform.GetChild(1).gameObject);
        //thePlayer.isDead = true;

    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        gamIsPaused = false;

        /*SceneManager.LoadScene("menueStart")*/
        ;
    }
}
