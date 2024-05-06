using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public void MyLoadScene(int idScene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(idScene);
        Time.timeScale = 1.0f;

        PiedestalController.clear1 = false;
        PiedestalController.clear2 = false;
        PiedestalController.canPut1 = false;
        PiedestalController.canPut1 = false;
    }
}
