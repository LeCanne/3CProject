using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ShowObject : MonoBehaviour
{
    public DepthOfField blur;
    public Volume volume;
    public ClampedFloatParameter cfp;

    public float timerBlur;

    public TextMeshProUGUI txtLabel;
    public Image imgObject;
    private bool canClose;

    private void Awake()
    {
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
            gameObject.SetActive(false);
        }
    }

    public IEnumerator Blur()
    {
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
