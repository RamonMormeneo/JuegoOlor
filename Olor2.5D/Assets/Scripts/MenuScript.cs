using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject howToPlay;

    public void exitGame()
    {
        Application.Quit();
    }


    public void play()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
