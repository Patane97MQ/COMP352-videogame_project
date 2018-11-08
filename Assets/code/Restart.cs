using EditorUtilities;
using UnityEngine;

public class Restart : MonoBehaviour {
    
    public static Restart r;
    private static SceneField nextScene;
    private static bool loading;

    Animator animator;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        r = this;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Go();
    }

    public void ResetScene()
    {
        Utilities.ReloadScene();
    }
    public void LoadNextScene()
    {
        loading = false;
        if(nextScene == null)
        {
            Debug.Log("Failed to load scene as 'nextScene' field is missing");
            Utilities.ReloadScene();
        }
        Utilities.LoadScene(nextScene);
        r.animator.SetTrigger("ResetAnim");
    }

    public static void Go()
    {
        r.animator.SetTrigger("Restart");
    }

    public static void GoToLevel(SceneField scene)
    {
        nextScene = scene;
        if (!loading)
        {
            loading = true;
            r.animator.SetTrigger("Change Level");
        }

    }


}
