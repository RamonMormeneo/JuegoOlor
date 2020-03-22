using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyPath : MonoBehaviour
{
    public GameObject myPath;
    private float currentTime=0.0f;
    private float step = 0.1f;
    public void showPath()
    {
        myPath.SetActive(true);
       StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
        myPath.SetActive(false);
    }
}

