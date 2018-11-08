using UnityEngine;

public class MainMenu : MonoBehaviour {

    public void PlayGame()
    {
        Utilities.LoadNextScene();

	}

    public void QuitGame()
    {
        Application.Quit();
    }
}
