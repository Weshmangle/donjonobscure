using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElementsGenerator : MonoBehaviour
{
    [SerializeField] int nbChestToSpawn, nbHoleToSpawn, nbEnemyToSpawn;

    Grid grid;
    Tile[,] tiles;

    Wall prefabWall;
    Hole prefabHole;
    Chest prefabChest;
    Obstacle prefabObstacle;
    Gate prefabGate;
    Enemy prefabEnemy;
    int width = 10;
    int height = 10;

    Vector2Int NextToExitGate;
    Vector2Int NextToEntryGate;

    [SerializeField] Material wallTransparentMat;

    [SerializeField] GridSeed[] levels;


    
    Quaternion rotationLeft = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    Quaternion rotationTop = Quaternion.Euler(0.0f, 90.0f, 0.0f);
    Quaternion rotationRight = Quaternion.Euler(0.0f, 180.0f, 0.0f);
    Quaternion rotationBot = Quaternion.Euler(0.0f, 270.0f, 0.0f);

    #region GenerateElementFromSeed

    public void GenerateElementFromSeed(Tile[,] tiles, GridSeed seed, Grid grid)
    {
        this.grid = grid;
        this.tiles = tiles;
        ClearContentTile();
        ElementData[,] elementDataGrid = SeedElementDataArrayToElementDataGrid(seed);
        PopulateTile(tiles, elementDataGrid);
        
        
    }

    private void ClearContentTile()
    {
        foreach(Tile tile in tiles)
        {
            if(tile.Content != null && !(tile.Content is Character) )
            {
                Destroy(tile.Content);
            }
        }
    }

    ElementData[,] SeedElementDataArrayToElementDataGrid(GridSeed seed)
    {
        ElementData[] elementDatas = (Resources.Load("Seeds/" + seed.Name) as GridSeed).ElementGrid;
        ElementData[,] elementDataGrid = new ElementData[seed.Row, seed.Column];
        int row = 0;
        int column = 0;
        for (int i = 0; i < elementDatas.Length; i++)
        {
            if(row < seed.Row)
            {
                elementDataGrid[row, column] = elementDatas[i];
            }
            
            if (column == seed.Column - 1)
                {
                    row++;
                    column = 0;
                }
            else column++;
        }

        return elementDataGrid;
    }
    private void PopulateTile(Tile[,] tiles, ElementData[,] elementDataGrid)
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                Tile currentTile = tiles[i,j];
                ElementData elementData = elementDataGrid[i,j];
                Vector2Int elementPosition = new Vector2Int(i, j);
                switch(elementData.Name)
                {
                    case "Floor":
                        currentTile.Content = elementData.Type;
                        break;
                    case "Wall":
                    {
                        GenerateWall(elementData, elementPosition);
                        break;
                    }
                    case "Entry Gate":
                    case "Exit Gate":
                    {
                        GenerateGate(elementData, elementPosition, currentTile);
                        break;
                    }
                    case "Chest":
                    {
                        GenerateElement(elementData, elementPosition, rotationRight);
                        break;
                    }
                    case "Hole":
                    {
                        GenerateElement(elementData, elementPosition, rotationTop);
                        break;
                    }
                    case "Goul":
                    {
                        GenerateEnemy(elementData, elementPosition, Quaternion.identity);
                        break;
                    }

                    case "Player":
                    {
                        break;
                    }

                }
            }
        }
    }

    private void GenerateElement(ElementData elementData, Vector2Int elementPosition, Quaternion orientation)
    {
        ElementGrid element = InstantiateElementGrid(elementData.Type, tiles[elementPosition.x, elementPosition.y].Position, orientation);
        tiles[elementPosition.x, elementPosition.y].Content = element;
    }
    private void GenerateEnemy(ElementData elementData, Vector2Int elementPosition, Quaternion orientation)
    {
        Enemy enemy = InstantiateElementGrid(elementData.Type, tiles[elementPosition.x, elementPosition.y].Position, orientation) as Enemy;
        tiles[elementPosition.x, elementPosition.y].Content = enemy;

        Game.Instance.Enemies.Add(enemy);
        enemy.TeleportTo(elementPosition, elementPosition);
    }

    private void GenerateWall(ElementData elementData, Vector2Int elementPosition)
    {

        ElementGrid wallExternElement = InstantiateElementGrid(elementData.Type, tiles[elementPosition.x, elementPosition.y].Position, Orientation(ref elementPosition));
        if (Orientation(ref elementPosition) == rotationBot || Orientation(ref elementPosition) == rotationLeft)
        {
            setAlphaWall(wallExternElement, 0);
        }
        tiles[elementPosition.x, elementPosition.y].Content = wallExternElement;
    }
    void GenerateGate(ElementData elementData, Vector2Int elementPosition, Tile currentTile)
    {
        Gate gate = InstantiateElementGrid(elementData.Type, tiles[elementPosition.x, elementPosition.y].Position, Orientation(ref elementPosition)) as Gate;
        tiles[elementPosition.x, elementPosition.y].Content = gate;
        if(elementData.Name == "Entry Gate")
        {
            NextToEntryGate = NextToGate(elementPosition);
            Game.Instance.grid.Entry = currentTile;
            Game.CharacterSpawnPosition = NextToEntryGate;
        }
        else if(elementData.Name == "Exit Gate")
        {
            NextToExitGate = NextToGate(elementPosition);

            gate.IsExitGate = true;
            Game.Instance.grid.Exit = currentTile;
        }
        else
        {
            Debug.LogError("ElementData is not a Gate" + elementData.Name);
        }
    }
    Vector2Int NextToGate(Vector2Int gatePosition)
    {
        Vector2Int nextToGate = gatePosition;

        if(gatePosition.x == 0)
        {
            nextToGate = new Vector2Int(gatePosition.x + 1, gatePosition.y);
        }
        else if (gatePosition.y == 9)
        {
            nextToGate = new Vector2Int(gatePosition.x, gatePosition.y - 1);
        }
        else if (gatePosition.y == 0)
        {
            nextToGate = new Vector2Int(gatePosition.x, gatePosition.y + 1);
        }
        else if (gatePosition.x == 9)
        {
            nextToGate = new Vector2Int(gatePosition.x - 1, gatePosition.y);
        }
        return nextToGate;
    }
    Quaternion Orientation(ref Vector2Int position)
    {
        Quaternion orientation = Quaternion.identity;


        if (position.x == 0)
        {
            orientation = rotationTop;
        }
        else if (position.y == 9)
        {
            orientation = rotationRight;
        }
        else if (position.x == 9)
        {
            orientation = rotationBot;
        }
        else if (position.y == 0)
        {
            orientation = rotationLeft;

        }

        return orientation;
    }

    #endregion
    public void GenerateElements(Tile[,] tiles, Grid _grid)
    {
        grid = _grid;
        prefabWall = (Resources.Load("Prefabs/Wall") as GameObject).GetComponent<Wall>();
        prefabHole = (Resources.Load("Prefabs/Hole") as GameObject).GetComponent<Hole>();
        prefabChest = (Resources.Load("Prefabs/Chest") as GameObject).GetComponent<Chest>();
        prefabObstacle = (Resources.Load("Prefabs/Obstacle") as GameObject).GetComponent<Obstacle>();
        prefabGate = (Resources.Load("Prefabs/Gate") as GameObject).GetComponent<Gate>();
        prefabEnemy = (Resources.Load("Prefabs/Enemy") as GameObject).GetComponent<Enemy>();

        var indexLevel = Random.Range(0, levels.Length);
        var level = levels[indexLevel];
        var index = 0;
        foreach (var elementData in level.ElementGrid)
        {
            switch (elementData.name)
            {
                case "Floor":
                    break;
                case "Wall":
                    ElementGrid wallExternElement = InstantiateElementGrid(prefabWall, tiles[index, width-1].Position, rotationRight);
                    tiles[index, width-1].Content = wallExternElement;
                    break;
                case "Entry":
                    break;
                case "Goul":
                    break;
                case "Hole":
                    break;
                case "Exit":
                    break;
                case "Chest":
                    break;
            }
            index = (index + 1) % width;
        }
    }
    
    #region OldGenerateElement
    public void OldGenerateElement(Tile[,] tiles, Grid _grid)
    {
        grid = _grid;
        prefabWall = (Resources.Load("Prefabs/Wall") as GameObject).GetComponent<Wall>();
        prefabHole = (Resources.Load("Prefabs/Hole") as GameObject).GetComponent<Hole>();
        prefabChest = (Resources.Load("Prefabs/Chest") as GameObject).GetComponent<Chest>();
        prefabObstacle = (Resources.Load("Prefabs/Obstacle") as GameObject).GetComponent<Obstacle>();
        prefabGate = (Resources.Load("Prefabs/Gate") as GameObject).GetComponent<Gate>();
        prefabEnemy = (Resources.Load("Prefabs/Enemy") as GameObject).GetComponent<Enemy>();

        //Tile chestTile = tiles[0,0];
        //GenerateChestList(tiles);
        GenerateExternWall(tiles);
        OldGenerateGate(tiles, true);
        OldGenerateGate(tiles, false);

        GenerateElementsGrid(tiles, nbChestToSpawn, prefabHole);
        GenerateElementsGrid(tiles, nbChestToSpawn, prefabChest);

        GenerateEnemy(tiles);
    }

    protected void setAlphaWall(ElementGrid wall, float alpha)
    {
        Renderer renderer = wall.GetComponentInChildren<Renderer>();
        renderer.material = wallTransparentMat;
    }

    public void GenerateExternWall(Tile[,] tiles)
    {
        for (int i = 0; i < width; i++)
        {
            ElementGrid wallExternElement = InstantiateElementGrid(prefabWall, tiles[i, 0].Position,rotationLeft);
            setAlphaWall(wallExternElement, 0);
            tiles[i,0].Content = wallExternElement;
            
        }
        for (int i = 0; i < width; i++)
        {
            ElementGrid wallExternElement = InstantiateElementGrid(prefabWall, tiles[i, width-1].Position, rotationRight);
            tiles[i, width-1].Content = wallExternElement;
            
        }
        for (int i = 0; i < height; i++)
        {
            ElementGrid wallExternElement = InstantiateElementGrid(prefabWall, tiles[0, i].Position, rotationTop);
            setAlphaWall(wallExternElement, 0);
            tiles[0, i].Content = wallExternElement;
            
        }
        for (int i = 0; i < height; i++)
        {
            ElementGrid wallExternElement = InstantiateElementGrid(prefabWall, tiles[height-1, i].Position, rotationBot);
            tiles[height-1, i].Content = wallExternElement;
        }

    }

    public void OldGenerateGate(Tile[,] tiles, bool isExitGate)
    {
        int _sideRandPosition = Random.Range(0, 4);
        Tile currentTile = null;
        Vector2Int NextToGatePosition = Vector2Int.zero;
        Quaternion rotation = Quaternion.identity;
        do
        {
            int _myRandPosition = Random.Range(1, width-1);
            
            switch (_sideRandPosition)
            {
                case 0:
                    currentTile = tiles[_myRandPosition, 0];
                    rotation = rotationLeft;
                    NextToGatePosition = new Vector2Int(currentTile.Position.x, currentTile.Position.y+1);
                    break;
                case 1:
                    currentTile = tiles[_myRandPosition, width-1]; 
                    rotation = rotationRight;
                    NextToGatePosition = new Vector2Int(currentTile.Position.x, currentTile.Position.y - 1);
                    break;
                case 2:
                    currentTile = tiles[0, _myRandPosition];
                    rotation = rotationTop;
                    NextToGatePosition = new Vector2Int(currentTile.Position.x + 1, currentTile.Position.y);
                    break;
                case 3:
                    currentTile = tiles[height-1, _myRandPosition];
                    rotation = rotationBot;
                    NextToGatePosition = new Vector2Int(currentTile.Position.x - 1, currentTile.Position.y);
                    break;
                default:
                        Debug.Log("Provided stat does not exist.");
                    break;
            }
        }
        while (typeof(Gate).IsInstanceOfType(currentTile.Content));
        
        Gate doorElement = InstantiateElementGrid(prefabGate, currentTile.Position, rotation) as Gate;

        if (isExitGate)
        {
            doorElement.IsExitGate = true;
            Game.Instance.grid.Exit = currentTile;
            NextToExitGate = NextToGatePosition;

        }
        else
        {
            Game.Instance.grid.Entry = currentTile;
            NextToEntryGate = NextToGatePosition;
            Game.CharacterSpawnPosition = NextToGatePosition;
        }

        currentTile.Content = doorElement;

    }

    public void GenerateElementsGrid(Tile[,] tiles, int quantity, ElementGrid prefab)
    {
        for (int i = 0; i < quantity; i++)
        {
            ElementGrid chestElement;

            int _myRandPositionX;
            int _myRandPositionZ;
            
            Tile tile;
            do
            {
                _myRandPositionX = Random.Range(1, width-1);
                _myRandPositionZ = Random.Range(1, height-1);
                tile = tiles[_myRandPositionX, _myRandPositionZ];
            }
            while(tile.Content != null ||
                tile.Position == NextToEntryGate ||
                tile.Position == NextToExitGate ||
                !Pathfinding.FindPathFromTile(grid.Entry, grid.Exit, grid)
            );
            
            chestElement = InstantiateElementGrid(prefab, tiles[_myRandPositionX, _myRandPositionZ].Position, Quaternion.Euler(0, 180, 0));
            tiles[_myRandPositionX, _myRandPositionZ].Content = chestElement;
        }
    }
    
    public void GenerateEnemy(Tile[,] tiles)
    {
        for (int i = 0; i < nbEnemyToSpawn; i++)
        {
            Tile tile;
            do
            {
                int _myRandPositionX = Random.Range(1, width);
                int _myRandPositionZ = Random.Range(1, height);
                tile = tiles[_myRandPositionX, _myRandPositionZ];
            }
            while(!(tile.Content == null && tile.Position != NextToEntryGate));

            ElementGrid enemyElement = InstantiateElementGrid(prefabEnemy, tile.Position);
            tile.Content = enemyElement;
            Game.Instance.Enemies.Add(enemyElement as Enemy);
            (enemyElement as Enemy).TeleportTo(tile.Position, tile.Position);
        }
    }

    public ElementGrid InstantiateElementGrid(ElementGrid element, Vector2Int position)
    {
        return Instantiate(element, new Vector3(position.x, 0, position.y), Quaternion.identity, this.transform);
    }

    public ElementGrid InstantiateElementGrid(ElementGrid element, Vector2Int position, Quaternion rotation)
    {
        return Instantiate(element, new Vector3(position.x, 0, position.y), rotation, this.transform);
    }

    //public ElementGrid InstantiateElementGridFromElementData(ElementData elementData, Vector2Int position)
    //{
    //    return Instantiate(element, new Vector3(position.x, 0, position.y), rotation, this.transform);
    //}

    /*
    public void GenerateChestList(List<Tile> tiles)
    {
        for (int i = 0; i < nbChestToSpawn; i++)
        {
            int indexElement = Random.Range(0, tiles.Count);
            tiles[indexElement].setContent(chest);
            tiles.RemoveAt(indexElement);
        }
    }
    */
}
#endregion