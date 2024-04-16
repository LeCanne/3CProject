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
    public DepthOfField blur;
    public Volume volume;
    public ClampedFloatParameter cfp;

    public float timerBlur;

    private void Awake()
    {
        DepthOfField dof;
        if (volume.profile.TryGet<DepthOfField>(out dof))
        {
            blur = dof;
        }
    }

    public IEnumerator Blur() 
    {
        timerBlur = 0;
        DepthOfField startBlur = blur;
        startBlur.focalLength.value = 1;
        DepthOfField endBlur = blur;
        endBlur.focalLength.value = 20;

        while (timerBlur < 0.5f)
        {
            timerBlur += Time.deltaTime;
            blur.focalLength.value = Mathf.Lerp(1, 40, timerBlur/ 0.5f);

            yield return null;
        }
    }

    public void StopTime()
    {
        Time.timeScale = 0f;
    }
}
