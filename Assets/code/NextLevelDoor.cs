﻿using UnityEngine;

public class NextLevelDoor : Activating {

    protected override void Activate()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    protected override void DeActivate()
    { }
}