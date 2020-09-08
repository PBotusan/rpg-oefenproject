using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlls : MonoBehaviour
{
    /// <summary>
    /// Texture used for mousecursor
    /// </summary>
    [SerializeField] Texture2D cursorTexture;


    [SerializeField] GameObject mousePoint;

    /// <summary>
    /// mode van cursor soort
    /// </summary>
    private CursorMode mode = CursorMode.ForceSoftware;

    /// <summary>
    /// plek waar je op drukt ingame.
    /// </summary>
    private Vector2 hotSpot = Vector2.zero;

    /// <summary>
    /// instantiate mouse position
    /// </summary>
    private GameObject instantiateMouse;

    /// <summary>
    /// when player clicks left mouse button
    /// </summary>
    private int mouseButtonDown = 1;

    /// <summary>
    /// when player releases mouse click.
    /// </summary>
    private int mouseButtonUp = 0;

    /// <summary>
    /// the height of the terrain in y axis .
    /// </summary>
    private float TerrainHeight = 15.4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, mode);


        if (Input.GetMouseButtonUp(mouseButtonUp))
        {
            PlaceMouseCursor();
        }
    }

    private void PlaceMouseCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider is TerrainCollider)
            {
                Vector3 temp = hit.point;
                temp.y = TerrainHeight;

                Instantiate(mousePoint, temp, Quaternion.identity);
            }
        }
    }
}
