using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] sfxBootStepList;
    public AudioSource sfxPickUp;
    public AudioSource sfxDoor, sfxScrapping;


    public int nbBootStep;

    // Proc�dures activ�es par des scripts
    public void BootStep()
    {
        nbBootStep = Random.Range(0, 4);
        sfxBootStepList[nbBootStep].Play();
    }

    // Proc�dures activ�es sur des animations
    public void PickUp()
    {
        sfxPickUp.Play();
    }

    public void Door()
    {
        sfxDoor.Play();
    }

    public void Scrapping()
    {
        sfxScrapping.Play();
    }
}
