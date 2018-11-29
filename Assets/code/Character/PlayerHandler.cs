using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Animator))]
public class PlayerHandler : MonoBehaviour {

    Animator animator;
    PlayerMovement pMovement;
    public GameObject restarter;
    public bool restarterEnabled = true;
    private AudioSource source;
    public bool required;
    private bool dead;
    // Use this for initialization
    void Start()
    {
        Restart restart = FindObjectOfType<Restart>();
        if (restart)
            restart.keyActive = restarterEnabled;
        else
        {
            GameObject r = Instantiate(restarter);
            r.GetComponent<Restart>().keyActive = restarterEnabled;
        }
        animator = GetComponent<Animator>();
        pMovement = GetComponent<PlayerMovement>();
    }
    private void Awake()
    {
        source = gameObject.GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update ()
    {
        if (!dead)
        {
            animator.SetBool("Walk", pMovement.moving);

            animator.SetBool("Crouch", pMovement.crouching);

            animator.SetBool("Jump", pMovement.jumping);

            animator.SetBool("Airborne", pMovement.airborne);

            animator.SetBool("Push", pMovement.pushing || pMovement.pulling ? true : false);
        }
        
        
    }
    public void ResetAnim()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Crouch", false);
        animator.SetBool("Jump", false);
        animator.SetBool("Airborne", false);
        animator.SetBool("Push", false);
    }
    public void Death(DeathType type)
    {
        dead = true;
        switch (type)
        {
            case DeathType.Acid:
                animator.SetTrigger("Acid Death");
                break;
            default:
                break;
        }
    }
    public void AfterDeath()
    {
        if(required)
            Restart.Go();
    }
    public void PlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
    public enum DeathType
    {
        Acid, Laser
    }
    [System.Serializable]
    public class GlobalAnimations
    {
        Animation restart;
    }
}
