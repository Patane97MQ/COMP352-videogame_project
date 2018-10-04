using UnityEngine;
using UnityEngine.SceneManagement;
using EditorUtilities;

public class ChangeLevelDoor : Activating {

    public SceneField scene;
    bool open = false;

    public override void Activate()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("objects/exit_door_open", typeof(Sprite)) as Sprite;
        open = true;
    }

    public override void DeActivate()
    { }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (open && collider.gameObject.tag.Equals("Player"))
            Utilities.LoadScene(scene);
    }
}
