using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] sfxBootStepList;
    public AudioSource sfxFalling;
    public AudioSource sfxPickUp, sfxOpenBag;
    public AudioSource sfxDoor, sfxScrapping, sfxCloseDoor;

    public int nbBootStep;

    // Procédure activée par des animations
    public void BootStep()
    {
        nbBootStep = Random.Range(0, 4);
        sfxBootStepList[nbBootStep].Play();
    }

    // Procédures activées par des scripts
    public void Falling()
    {
        sfxFalling.Play();
    }

    public void PickUp()
    {
        sfxPickUp.Play();
    }

    public void OpenBag()
    {
        sfxOpenBag.Play();
    }

    public void Door()
    {
        sfxDoor.Play();
    }

    public void CloseDoor()
    {
        sfxCloseDoor.Play();
    }

    public void Scrapping()
    {
        sfxScrapping.Play();
    }
}
