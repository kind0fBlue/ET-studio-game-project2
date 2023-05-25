using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChangeToCrosshair : MonoBehaviour
{
    public void Start() {
        mouseCursor.instance.ActivateCrosshair();
    }

}
