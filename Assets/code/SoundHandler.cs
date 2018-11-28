using UnityEngine;

public class SoundHandler : MonoBehaviour {
    private AudioSource source;

    public void Awake()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
