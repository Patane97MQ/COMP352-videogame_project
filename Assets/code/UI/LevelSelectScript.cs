using UnityEngine;

public class LevelSelectScript : MonoBehaviour {

    public int index;

    public void SelectLevel(int x)
    {
        Utilities.LoadScene(x);
    }
}
