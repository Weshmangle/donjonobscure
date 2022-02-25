using UnityEngine;
public class SeedNode : MonoBehaviour
{
    Rect rect;
    public GUIStyle style;
    public ElementData elementType;
    public SeedNode(Vector2 position, float width, float height, GUIStyle defaultStyle, ElementData elementType)
    {
        rect = new Rect(position.x, position.y, width, height);
        style = defaultStyle;
        this.elementType = elementType;
    }
    public void Drag(Vector2 delta)
    {
        rect.position += delta;
    }
    public void Draw()
    {
        GUI.Box(rect, "", style);
    }
    public void SetStyleAndElementType(GUIStyle nodeStyle, ElementData elementType)
    {
        style = nodeStyle;
        this.elementType = elementType;
    }
}