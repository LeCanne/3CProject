using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DallesController : MonoBehaviour
{
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

    private bool haveWin, haveLose;

    public GameObject barrière;

    private void Awake()
    {
        mainMesh = GetComponent<MeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        
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
            }

            if (categories == CATEGORY.FINALE && !haveWin && !haveLose)
            {
                //barrière.SetActive(false);
                foreach (var dalle in dallesArray)
                {
                    dalle.mainMesh.material = matBlue;
                    dalle.haveWin = true;
                }
            }
        }
    }
}
