using _3CFeel.Controller;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public PlayerController thePlayer;

    public static bool gamIsPaused = false;

    public GameObject pauseMenuUI;

    public DepthOfField blur;
    public Volume volume;
    public ClampedFloatParameter cfp;

    public float timerBlur;

    private void Awake()
    {
        thePlayer = GameObject.FindAnyObjectByType<PlayerController>();

        DepthOfField dof;
        if (volume.profile.TryGet<DepthOfField>(out dof))
        {
            blur = dof;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //blur.focalLength = new ClampedFloatParameter(1, 0, 300);

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
        timerBlur = 0f;
        gamIsPaused = true;
        EventSystem.current.SetSelectedGameObject(pauseMenuUI.transform.GetChild(1).gameObject);
        //thePlayer.isDead = true;

        StartCoroutine(Blur());
    }

    public IEnumerator Blur() 
    {
        DepthOfField startBlur = blur;
        startBlur.focalLength.value = 1;
        DepthOfField endBlur = blur;
        endBlur.focalLength.value = 100;

        while (timerBlur < 3)
        {
            timerBlur += Time.deltaTime;
            blur.focalLength.value = Mathf.Lerp(startBlur.focalLength.value, endBlur.focalLength.value, timerBlur/ 3f);

            yield return null;
        }
        Time.timeScale = 0f;
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
}
