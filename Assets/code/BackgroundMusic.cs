using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

    AudioSource source;
    public AudioClip music;

	void Start()
    {
        if (FindObjectsOfType<BackgroundMusic>().Length > 1)
            return;
        source = gameObject.GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
        source.PlayOneShot(music);
	}
}