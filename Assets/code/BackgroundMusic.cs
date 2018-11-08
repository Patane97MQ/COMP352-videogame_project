using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

    AudioSource source;
    public AudioClip music;

	void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
        source.PlayOneShot(music);
	}
}