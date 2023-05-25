using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChangeToNormal : MonoBehaviour
{
    public void Start() {
        mouseCursor.instance.ActivateNormalCursor();
    }

}
