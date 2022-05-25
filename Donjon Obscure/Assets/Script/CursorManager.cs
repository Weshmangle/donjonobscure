using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;

    public Texture2D cursorDefault;
    public Texture2D cursorAttak; 
    public Texture2D cursorMove; 
    public Texture2D cursorInterraction; 
    void Start()
    {
        Instance = this;
        SetDefaultCursor();
    }

    public void ChangeCursor(Texture2D texture)
    {
        Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
    }

    public void SetDefaultCursor()
    {
        ChangeCursor(cursorDefault);
    }

    public void SetAttaktCursor()
    {
        ChangeCursor(cursorAttak);
    }

    public void SetMovementCursor()
    {
        ChangeCursor(cursorMove);
    }

    public void SetInterractionCursor()
    {
        ChangeCursor(cursorInterraction);
    }
}
