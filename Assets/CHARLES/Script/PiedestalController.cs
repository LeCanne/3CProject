using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PiedestalController : MonoBehaviour
{
    public enum CATEGORY
    {
        PIEDESTAL1,
        PIEDESTAL2
    }

    public Transform trPlacement;
    public GameObject prefabItem;

    public GameObject putObject;
    public Image imgPutObject;
    public TextMeshProUGUI txtPutObject;
    public static bool canPut, havePut;

    public void PutObject()
    {
        putObject.SetActive(false);
        canPut = false;
        havePut = true;
    }

    public IEnumerator PutobjectOn()
    {
        imgPutObject.color = new Color(0, 0.8f, 1, 0.2f);
        txtPutObject.color = new Color(0, 0, 0, 0.2f);
        yield return new WaitForSeconds(0.05f);
        imgPutObject.color = new Color(0, 0.8f, 1, 0.4f);
        txtPutObject.color = new Color(0, 0, 0, 0.4f);
        yield return new WaitForSeconds(0.05f);
        imgPutObject.color = new Color(0, 0.8f, 1, 0.6f);
        txtPutObject.color = new Color(0, 0, 0, 0.6f);
        yield return new WaitForSeconds(0.05f);
        imgPutObject.color = new Color(0, 0.8f, 1, 0.8f);
        txtPutObject.color = new Color(0, 0, 0, 0.8f);
        yield return new WaitForSeconds(0.05f);
        imgPutObject.color = new Color(0, 0.8f, 1, 1f);
        txtPutObject.color = new Color(0, 0, 0, 1f);
    }

    public IEnumerator PutobjectOf()
    {
        imgPutObject.color = new Color(0, 0.8f, 1, 1f);
        txtPutObject.color = new Color(0, 0, 0, 1f);
        yield return new WaitForSeconds(0.05f);
        imgPutObject.color = new Color(0, 0.8f, 1, 0.8f);
        txtPutObject.color = new Color(0, 0, 0, 0.8f);
        yield return new WaitForSeconds(0.05f);
        imgPutObject.color = new Color(0, 0.8f, 1, 0.6f);
        txtPutObject.color = new Color(0, 0, 0, 0.6f);
        yield return new WaitForSeconds(0.05f);
        imgPutObject.color = new Color(0, 0.8f, 1, 0.4f);
        txtPutObject.color = new Color(0, 0, 0, 0.4f);
        yield return new WaitForSeconds(0.05f);
        imgPutObject.color = new Color(0, 0.8f, 1, 0.2f);
        txtPutObject.color = new Color(0, 0, 0, 0.2f);
        yield return new WaitForSeconds(0.05f);
        imgPutObject.color = new Color(0, 0.8f, 1, 0);
        txtPutObject.color = new Color(0, 0, 0, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !havePut)
        {
            putObject.SetActive(true);
            canPut = true;
            StartCoroutine(PutobjectOn());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !havePut)
        {
            canPut = false;
            StartCoroutine(PutobjectOf());
        }
    }
}
