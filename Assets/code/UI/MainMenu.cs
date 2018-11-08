using EditorUtilities;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    
    public SceneField firstLevel;

    public void PlayGame()
    {
        Restart.GoToLevel(firstLevel);
	}

    public void QuitGame()
    {
        Application.Quit();
    }
}
