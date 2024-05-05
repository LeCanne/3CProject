using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSpawn : MonoBehaviour
{
    public GameObject Door;
    private AudioManager theAudio;

    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        theAudio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !isActive)
        {
            isActive = true;
            Door.SetActive(true);
            theAudio.sfxDoor.Play();
        }
    }
}
