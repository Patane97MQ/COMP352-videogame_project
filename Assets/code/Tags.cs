using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tags : MonoBehaviour {

    public List<string> activateTags;

    public bool CheckTag(string tag)
    {
        if (activateTags.Count == 0 || activateTags.Contains(tag))
            return true;
        return false;
    }

}
