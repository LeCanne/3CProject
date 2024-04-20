using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool canOpen = true;
    public Transform tr;

    public float timerOpen;

    public AudioManager theAudio;

    private void Awake()
    {
        theAudio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PiedestalController.clear1 && PiedestalController.clear2 && canOpen)
        {
            canOpen = false;
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor() 
    {
        theAudio.Door();
        Vector3 startPosition = tr.position;
        Vector3 endPosition = new Vector3(startPosition.x , tr.position.y + 10, startPosition.z);

        while (timerOpen < 3)
        {
            timerOpen += Time.deltaTime;
            tr.position = Vector3.Lerp(startPosition, endPosition, timerOpen / 3f);

            yield return null;
        }
    }
}
