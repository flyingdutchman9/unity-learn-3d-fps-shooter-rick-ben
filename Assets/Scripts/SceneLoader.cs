using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class SceneLoader : MonoBehaviour
{
    int timeScale = 1;

    private void Update()
    {
        SwapTimeScaleOnEscape();
    }

    private void SwapTimeScaleOnEscape()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (timeScale == 1)
        //    {
        //        timeScale = 0;
        //    }
        //    else
        //    {
        //        timeScale = 1;
        //    }

        //    Time.timeScale = timeScale;
        //}
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
