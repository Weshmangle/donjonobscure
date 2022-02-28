using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

public class GridSeedCreator : EditorWindow
{
    static GridSeedCreator window;
    int Row = 10;
    int Column = 10;
    int SeedTileSize = 64;
    
    Vector2 offset;
    Vector2 drag; 
    Vector2 nodePosition;
    List<List<SeedNode>> nodes;
    GUIStyle empty;
    GUIStyle currentStyle;
    GUIStyle brushToggleStyle;
    GUIStyle brushToolbarStyle;
    GUIStyle brushToolbarVerticalStyle;
    bool iSErasing;
    string gridSeedName = "New Untilted Seed";
    string SeedDescription = "Description of the Seed";
    ElementData[] elementsData;
    ElementData emptyElementData;
    ElementData currentElementData;

    Rect Centered()
    {
        int width = Row * SeedTileSize;
        int height = Column * SeedTileSize;
        return new Rect((position.width - width)/2, (position.height - height)/2, position.width, position.height);
        // return new Rect(0, 0, position.width, position.height);
    }

    [MenuItem("Donjon Obscure/GridSeed Creator")]
    static void ShowWindow()
    {
        window = GetWindow<GridSeedCreator>();
        
        window.titleContent = new GUIContent("Grid Seed Creator");
        window.Show();
        

    }
    void OnEnable()
    {
        SetupGUIStyle();
        SetupSeedBrushes();
        SetupNodes(); 
        SetPositionToCenter();
    }

    private void SetupGUIStyle()
    {
        brushToggleStyle = new GUIStyle(EditorStyles.toolbarButton);
        brushToggleStyle.fixedHeight = SeedTileSize;
        brushToggleStyle.imagePosition = ImagePosition.ImageAbove;
        brushToolbarStyle = new GUIStyle(EditorStyles.toolbar);
        brushToolbarStyle.fixedHeight = 0;
        brushToolbarVerticalStyle = new GUIStyle(brushToolbarStyle);
        
        brushToolbarStyle.fixedHeight = 0;
        
    }

    private void SetupSeedBrushes()
    {
        try {
            elementsData = Resources.Load<ElementDataContainer>("Elements/ElementDataInGame").Elements;
            for (int i = 0; i < elementsData.Length; i++)
            {
                elementsData[i].Style = new GUIStyle();
                elementsData[i].Style.normal.background = elementsData[i].Icon;
            }
        }
        catch (Exception exception){ Debug.LogError(exception);}
        empty =  elementsData[0].Style;
        currentStyle =  elementsData[1].Style;
        emptyElementData = elementsData[0];
        currentElementData = elementsData[1];
        
    }

    void SetupNodes()
    {
        nodes = new List<List<SeedNode>>();
        for (int i = 0; i < Row; i++)
        {
            nodes.Add(new List<SeedNode>());
            for (int j = 0; j < Column; j++)
            {
                nodePosition.Set(i*SeedTileSize, j*SeedTileSize);
                nodes[i].Add(new SeedNode(nodePosition, SeedTileSize, SeedTileSize, empty, elementsData[0]));
            }
        }
    }
    void OnGUI()
    {
        DrawMenuBar();
        DrawPaintingBar();
        GUILayout.BeginArea(new Rect(0, 72, position.width - 81, position.height));
        DrawGrid();
        DrawNodes();
        ProcessNodes(Event.current);
        ProcessGrid(Event.current);
        GUILayout.EndArea();
        // DrawCursorCoordinates();
        if(GUI.changed)
        {
            Repaint();
        }
        
    }
    private void DrawCursorCoordinates()
    {
        string mousePosition = Event.current.mousePosition.ToString();
        Rect gridPosition = new Rect(64, 128, position.width, 64);
        GUILayout.BeginArea(gridPosition, EditorStyles.toolbar);
        GUILayout.TextArea(mousePosition);
        GUILayout.EndArea();
    }
    void DrawGrid()
    {
        int widthDivider = Mathf.CeilToInt(position.width / SeedTileSize);
        int heightDivider = Mathf.CeilToInt(position.height / SeedTileSize);
        Rect gridPosition = new Rect(0, 0, position.width, position.height);
        GUILayout.BeginArea(gridPosition);
        Handles.color = new Color(0.5f, 0.5f, 0.5f, 0.2f);
        
        offset += drag;
        Vector3 newOffset = new Vector3(offset.x%SeedTileSize, offset.y%SeedTileSize, 0);
        for (int i = 0; i < widthDivider; i++)
        {
            Handles.DrawLine(new Vector3(SeedTileSize*i, -SeedTileSize, 0)+newOffset, new Vector3(SeedTileSize*i, position.height, 0)+newOffset);
        }
        for (int i = 0; i < heightDivider; i++)
        {
            Handles.DrawLine(new Vector3(-SeedTileSize, SeedTileSize*i, 0)+newOffset, new Vector3(position.width, SeedTileSize*i, 0)+newOffset);
        }
        Handles.color = Color.white;
        GUILayout.EndArea();
    }
    void DrawNodes()
    {
        for (int i = 0; i < Row; i++)
        {
            for (int j = 0; j < Column; j++)
            {
                nodes[i][j].Draw();
            }
        }
    }

    void DrawMenuBar()
    {
        Rect menuBar = new Rect(0, 0, position.width, 72);
        GUILayout.BeginArea(menuBar, brushToolbarStyle);
        GUILayout.BeginHorizontal();
        SeedDescription = GUILayout.TextField(SeedDescription, GUILayout.MinWidth(600), GUILayout.Height(64));
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical();
        gridSeedName = GUILayout.TextField(gridSeedName, GUILayout.Width(144), GUILayout.Height(20));
        if(GUILayout.Button("Save Grid Seed", GUILayout.Width(144), GUILayout.Height(42)))
        {
            string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/Seeds/" + gridSeedName + ".asset");
            GridSeed seed = GridSeed.CreateInstance<GridSeed>();
            SetupSeedData(seed);
            AssetDatabase.CreateAsset(seed, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = seed;
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    private void SetupSeedData(GridSeed seed)
    {
        seed.Name = gridSeedName;
        seed.Description = SeedDescription;
        seed.ElementGrid = new ElementData[Row*Column];
        for (int i = 0; i < nodes.Count; i++)
        {
            for (int j = 0; j < nodes[i].Count; j++)
            {
                seed.ElementGrid[j*nodes[i].Count+i] = nodes[i][j].elementType;
            }
        }
        // foreach(List<SeedNode> nodeList in nodes)
        // {
        //     foreach(SeedNode node in nodeList)
        //     {
        //         seed.ElementGrid[seed.ElementGrid.Length-i] = node.elementType;
        //         i++;
        //     }
            
        // }
    }

    private void DrawPaintingBar()
    {
        
        Rect menuBar = new Rect(0, 72, position.width, position.height);
        GUILayout.BeginArea(menuBar);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical(brushToolbarVerticalStyle, GUILayout.ExpandHeight(true));

        for (int i = 0; i < elementsData.Length; i++)
        {
            if(GUILayout.Toggle(
                currentStyle == elementsData[i].Style,
                new GUIContent(
                    elementsData[i].Name,
                    elementsData[i].Icon), brushToggleStyle,
                GUILayout.Width(80),
                GUILayout.Height(64)))
            {
                currentStyle = elementsData[i].Style;
                currentElementData = elementsData[i];
            }
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    private void ProcessNodes(Event _event)
    {
        int Row = (int)((_event.mousePosition.x - offset.x) / SeedTileSize);
        int Column = (int)((_event.mousePosition.y - offset.y) / SeedTileSize);
        if (CursorInGrid(_event))
        {
            if(_event.type == EventType.MouseDown)
            {
                if(nodes[Row][Column].style.normal.background.name == "Floor")
                {
                    iSErasing = false;
                    
                }
                else
                {
                    iSErasing = true;
                }
                PaintNodes(Row, Column);
            }
            if(_event.type == EventType.MouseDrag)
            {
                PaintNodes(Row, Column);
                _event.Use();
            }
        }
            
    }

    bool CursorInGrid(Event _event)
    {
        
        int maxGridSizeWidth = Row * SeedTileSize;
        int maxGridSizeHeight = Column * SeedTileSize;
        return !((_event.mousePosition.x - offset.x) < 0 ||
            (_event.mousePosition.x - offset.x) > maxGridSizeWidth ||
            (_event.mousePosition.y - offset.y) < 0 ||
            (_event.mousePosition.y - offset.y) > maxGridSizeHeight);
    }

    void ProcessGrid(Event _event)
    {
        drag = Vector2.zero;
        switch (_event.type)
        {
            case EventType.MouseDrag:
                if(_event.button == 0)
                {
                    OnMouseDrag(_event.delta);
                }
                break;
        }
    }
    void PaintNodes(int Row, int Column)
    {
        if(iSErasing)
        {
            nodes[Row][Column].SetStyleAndElementType(empty, emptyElementData);
            GUI.changed = true;
        }
        else
        {
            nodes[Row][Column].SetStyleAndElementType(currentStyle, currentElementData);
            GUI.changed = true;
        }
    }
    void OnMouseDrag(Vector2 delta) 
    {
        drag = delta;
        for (int i = 0; i < Row; i++)
        {
            for (int j = 0; j < Column; j++)
            {
                nodes[i][j].Drag(delta);
            }
        }
        GUI.changed = true;
    }
    void SetPositionToCenter()
    {
        int maxGridSizeWidth = Row * SeedTileSize;
        int maxGridSizeHeight = Column * SeedTileSize;
        drag = new Vector2((window.position.width - maxGridSizeWidth - 72) / 2, ((window.position.height- maxGridSizeHeight  - 72) / 2));
        for (int i = 0; i < Row; i++)
        {
            for (int j = 0; j < Column; j++)
            {
                nodes[i][j].Drag(drag);
            }
        }
        GUI.changed = true;
    }
    
    
}