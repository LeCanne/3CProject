using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DallesController : MonoBehaviour
{
    // Catégories pour assigner le type de dalle
    public enum CATEGORY
    {
        TRUE,
        FALSE, 
        FINALE,
        START
    }
    public CATEGORY categories;
    private MeshRenderer mainMesh;
    public Material matBlue, matRed, matGrey;

    public DallesController[] dallesArray;
    public int nbDalles;

    private bool haveWin, haveLose;

    public GameObject door;
    public float timerOpen;

    public AudioManager theAudio;

    private void Awake()
    {
        theAudio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        mainMesh = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (categories == CATEGORY.TRUE && !haveWin && !haveLose)
            {
                mainMesh.material = matBlue;
            }

            if (categories == CATEGORY.FALSE && !haveWin && !haveLose)
            {
                foreach (var dalle in dallesArray) 
                {
                    dalle.mainMesh.material = matRed;
                    dalle.haveLose = true;
                }
            }

            if (categories == CATEGORY.START && !haveWin)
            {
                foreach (var dalle in dallesArray)
                {
                    dalle.mainMesh.material = matGrey;
                    dalle.haveLose = false;
                }

                dallesArray[24].mainMesh.material = matBlue;
            }

            if (categories == CATEGORY.FINALE && !haveWin && !haveLose)
            {
                StartCoroutine(OpenDoor());

                foreach (var dalle in dallesArray)
                {
                    dalle.mainMesh.material = matBlue;
                    dalle.haveWin = true;
                }
            }
        }
    }

    IEnumerator OpenDoor()
    {
        theAudio.Door();

        Vector3 startPosition = new Vector3(door.transform.position.x, door.transform.position.y, door.transform.position.z);
        Vector3 endPosition = new Vector3(startPosition.x, door.transform.position.y + 10, startPosition.z);

        while (timerOpen < 3)
        {
            timerOpen += Time.deltaTime;
            door.transform.position = Vector3.Lerp(startPosition, endPosition, timerOpen / 3f);

            yield return null;
        }
    }
}
