using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mouseCursor : MonoBehaviour
{   
    public static mouseCursor instance;

    public Texture2D normalCursor, crosshair;
    
    private void Awake() {
        instance = this;
    }

    void Start() {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateNormalCursor() {
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
    }

    public void ActivateCrosshair() {
        Vector2 cursorHotspot = new Vector2 (crosshair.width / 2, crosshair.height / 2);
        Cursor.SetCursor(crosshair, cursorHotspot, CursorMode.Auto);
    }
}
