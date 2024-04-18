using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] sfxBootStepList;
    public AudioSource sfxPickUp;
    public AudioSource sfxTorche;
    public AudioSource sfxDoor;

    public int nbBootStep;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BootStep()
    {
        nbBootStep = Random.Range(0, 5);
        sfxBootStepList[nbBootStep].Play();
    }

    public void PickUp()
    {
        sfxPickUp.Play();
    }

    public void Door()
    {
        sfxDoor.Play();
    }
}
