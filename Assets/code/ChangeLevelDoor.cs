using UnityEngine;
using EditorUtilities;
using System.Collections;

public class ChangeLevelDoor : Activating {

    public SceneField scene;
    bool open = false;

    Animator animator;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }
    public override void Activate()
    {
        StartCoroutine(RunOpen());
    }
    private IEnumerator RunOpen()
    {
        animator.SetTrigger("Open");
        yield return new WaitForSeconds(1.3f);
        open = true;
    }

    public override void DeActivate()
    { }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (open && collider.gameObject.tag.Equals("Player"))
            Utilities.LoadScene(scene);

    }
    void OnTriggerStay2D(Collider2D collider)
    {
        if (open && collider.gameObject.tag.Equals("Player"))
            Utilities.LoadScene(scene);

    }
}
