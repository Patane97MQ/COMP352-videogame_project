using EditorUtilities;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    
    public SceneField firstLevel;

    public void PlayGame()
    {
        Utilities.LoadNextScene();
	}

    public void QuitGame()
    {
        Application.Quit();
    }
}
