using UnityEngine;

public class Restart : MonoBehaviour {
    
    public static Restart r;
    Animator animator;
    // Update is called once per frame

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

    public static void Go()
    {
        r.animator.SetTrigger("Restart");
    }

}
