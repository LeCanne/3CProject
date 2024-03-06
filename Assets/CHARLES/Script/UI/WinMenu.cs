using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    public GameObject winMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            winMenu.SetActive(true);
            CameraController.noUseCamera = true;
            Time.timeScale = 0;
        }
    }
}
