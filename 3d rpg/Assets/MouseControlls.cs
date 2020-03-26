using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlls : MonoBehaviour
{
    /// <summary>
    /// Texture used for mousecursor
    /// </summary>
    public Texture2D cursorTexture;

    /// <summary>
    /// 
    /// </summary>
    private CursorMode mode = CursorMode.ForceSoftware;

    /// <summary>
    /// 
    /// </summary>
    private Vector2 hotSpot = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, mode);
        
    }
}
