using _3CFeel.Controller;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ShowObject : MonoBehaviour
{
    public DoorController theDoor;
    private ItemController theItem;
    private PlayerController thePlayer;

    public DepthOfField blur;
    public Volume volume;
    public ClampedFloatParameter cfp;

    public float timerBlur;

    public TextMeshProUGUI txtLabel;
    public Image imgObject;
    private bool canClose;
    public bool isRightEye;

    private void Awake()
    {
        thePlayer = GameObject.FindAnyObjectByType<PlayerController>();

        if (isRightEye) 
        { 
            theDoor =  GameObject.Find("porte_enigme_LowPoly_Exit").GetComponent<DoorController>();
        }

        DepthOfField dof;
        if (volume.profile.TryGet<DepthOfField>(out dof))
        {
            blur = dof;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire3") && canClose)
        {
            canClose = false;
            Time.timeScale = 1;
            CameraController.noUseCamera = false;
            thePlayer.iconeInventaire.SetActive(true);

            if (isRightEye)
            {
                isRightEye = false;
                theDoor.StartCoroutine(theDoor.OpenDoor());
            }

            gameObject.SetActive(false);
        }
    }

    public IEnumerator Blur()
    {
        thePlayer.iconeInventaire.SetActive(false);
        Time.timeScale = 0.0f;
        CameraController.noUseCamera = true;
        timerBlur = 0;
        DepthOfField startBlur = blur;
        startBlur.focalLength.value = 1;
        DepthOfField endBlur = blur;
        endBlur.focalLength.value = 20;

        while (timerBlur < 0.5f)
        {
            timerBlur += Time.deltaTime;
            blur.focalLength.value = Mathf.Lerp(1, 40, timerBlur / 1f);

            yield return null;
        }
    }

    public void AddObject(Sprite icone, string label)
    {
        imgObject.sprite = icone;
        txtLabel.text = label.ToString();
    }

    public void CanClose()
    {
        canClose = true;
    }
}
