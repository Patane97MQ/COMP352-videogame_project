using UnityEngine;
using EditorUtilities;
using System.Collections;

public class ChangeLevelDoor : Activating {

    public SceneField scene;
    bool open = false;

    public ChangeLevelDoorSounds sounds = new ChangeLevelDoorSounds();

    private Animator animator;
    private AudioSource source;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }
    public void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    public override void Activate()
    {
        animator.SetTrigger("Open");
    }

    public void SoundOpen()
    {
        source.PlayOneShot(sounds.open);
    }

    public void SoundSuccess()
    {
        source.PlayOneShot(sounds.success);
    }

    public void VisualOpen()
    {
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
    [System.Serializable]
    public class ChangeLevelDoorSounds
    {
        public AudioClip open;
        public AudioClip success;
    }
}
