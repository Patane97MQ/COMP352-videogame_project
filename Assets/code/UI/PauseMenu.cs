using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool GamePaused = false;

    public GameObject pauseMenuUI;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                Resume();

            }

            else
            {

                Pause();
            }

         
        }
	}

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        foreach (PlayerHandler pHandler in GameObject.FindObjectsOfType<PlayerHandler>())
        {
            pHandler.gameObject.GetComponent<PlayerMovement>().enabled = true;
            pHandler.enabled = true;
        }

        GamePaused = false;
    }

    void Pause()
    {

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        foreach (PlayerHandler pHandler in GameObject.FindObjectsOfType<PlayerHandler>())
        {
            pHandler.gameObject.GetComponent<PlayerMovement>().enabled = false;
            pHandler.enabled = false;
        }
        GamePaused = true;
    }

    public void LoadMenu()
    {
        foreach (PlayerHandler pHandler in GameObject.FindObjectsOfType<PlayerHandler>())
        {
            pHandler.gameObject.GetComponent<PlayerMovement>().enabled = true;
            pHandler.enabled = true;
        }
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        Utilities.ReloadScene();
        GamePaused = false;
    }
}
