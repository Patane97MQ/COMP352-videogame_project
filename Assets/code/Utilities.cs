using UnityEngine.SceneManagement;

public class Utilities {
    
    public static void LoadScene(int index)
    {
        ColourHandler.Reset();
        SceneManager.LoadScene(index);
    }
    public static void LoadScene(string name)
    {
        ColourHandler.Reset();
        SceneManager.LoadScene(name);
    }
}
