using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

public class GridSeedCreator : EditorWindow
{
    static GridSeedCreator window;
    int Row = 10;
    int Column = 10;
    int maxGridSize = 300;
    int SeedTileSize = 64;
    Vector2 offset;
    Vector2 drag;
    Vector2 nodePosition;
    List<List<SeedNode>> nodes;
    GUIStyle empty;
    GUIStyle currentStyle;
    SeedTileManager seedTileManager;
    bool iSErasing;
    string gridSeedName = "New Untilted Grid Seed";
    Rect Centered()
    {
        int width = Row * SeedTileSize;
        int height = Column * SeedTileSize;
        // return new Rect((position.width - width)/2, (position.height - height)/2, position.width, position.height);
        return new Rect(0, 0, position.width, position.height);
    }

    [MenuItem("Donjon Obscure/GridSeed Creator")]
    static void ShowWindow()
    {
        window = GetWindow<GridSeedCreator>();
        
        window.titleContent = new GUIContent("Grid Seed Creator");

    }
    void OnEnable()
    {
        SetupSeedTileManager();
        SetupNodes();
    }

    private void SetupSeedTileManager()
    {
        try {
            seedTileManager = GameObject.FindGameObjectWithTag("SeedTileManager").GetComponent<SeedTileManager>();
            for (int i = 0; i < seedTileManager.seedTiles.Length; i++)
            {
                seedTileManager.seedTiles[i].NodeStyle = new GUIStyle();
                seedTileManager.seedTiles[i].NodeStyle.normal.background = seedTileManager.seedTiles[i].Icon;
            }
        }
        catch (Exception exception){}
        empty =  seedTileManager.seedTiles[0].NodeStyle;
        currentStyle =  seedTileManager.seedTiles[1].NodeStyle;
        
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
                nodes[i].Add(new SeedNode(nodePosition, SeedTileSize, SeedTileSize, empty));
            }
        }
    }
    void OnGUI()
    {
        DrawCursorCoordinates();
        DrawGrid();
        DrawNodes();
        DrawMenuBar();
        DrawPaintingBar();
        ProcessNodes(Event.current);
        ProcessGrid(Event.current);
        if(GUI.changed)
        {
            Repaint();
        }
        
    }

    private void DrawCursorCoordinates()
    {
        Rect gridPosition = new Rect(0, 0, position.width, position.height);
        GUILayout.BeginArea(gridPosition);
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
        Rect menuBar = new Rect(0, 0, position.width, 32);
        GUILayout.BeginArea(menuBar, EditorStyles.toolbar);
        GUILayout.BeginHorizontal();
            gridSeedName = GUILayout.TextField(gridSeedName);
            if(GUILayout.Button("Save Grid Seed"))
            {
                Debug.Log(gridSeedName);
            }
            
        
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    private void DrawPaintingBar()
    {
        Rect menuBar = new Rect(0, 21, position.width, 64);
        GUILayout.BeginArea(menuBar, EditorStyles.toolbar);
        GUILayout.BeginHorizontal();

        for (int i = 0; i < seedTileManager.seedTiles.Length; i++)
        {
            if(GUILayout.Toggle(
                currentStyle == seedTileManager.seedTiles[i].NodeStyle,
                new GUIContent(
                    seedTileManager.seedTiles[i].Icon,
                    seedTileManager.seedTiles[i].ButtonText),
                EditorStyles.toolbarButton,
                GUILayout.Width(80),
                GUILayout.Height(64)))
            {
                currentStyle = seedTileManager.seedTiles[i].NodeStyle;
            }
        }
        
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
        return !((_event.mousePosition.x - offset.x) < 0 ||
            (_event.mousePosition.x - offset.x) > maxGridSize ||
            (_event.mousePosition.y - offset.y) < 0 ||
            (_event.mousePosition.y - offset.y) > maxGridSize);
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
            nodes[Row][Column].SetStyle(empty);
            GUI.changed = true;
        }
        else
        {
            nodes[Row][Column].SetStyle(currentStyle);
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
    
    
}