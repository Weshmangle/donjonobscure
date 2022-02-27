using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsGenerator : MonoBehaviour
{
    [SerializeField] int nbChestToSpawn, nbHoleToSpawn, nbEnemyToSpawn;

    Grid grid;
    
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
    
    [SerializeField] GridSeed [] levels;
    
    Quaternion rotationTop = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    Quaternion rotationRight = Quaternion.Euler(0.0f, 90.0f, 0.0f);
    Quaternion rotationBot = Quaternion.Euler(0.0f, 180.0f, 0.0f);
    Quaternion rotationLeft = Quaternion.Euler(0.0f, 270.0f, 0.0f);

    public void GenerateElement(Tile[,] tiles, Grid _grid)
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
                    ElementGrid wallExternElement = InstantiateElementGrid(prefabWall, tiles[index, width-1].Position, rotationBot);
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
        GenerateGate(tiles, true);
        GenerateGate(tiles, false);

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
            ElementGrid wallExternElement = InstantiateElementGrid(prefabWall, tiles[i, 0].Position,rotationTop);
            setAlphaWall(wallExternElement, 0);
            tiles[i,0].Content = wallExternElement;
            
        }
        for (int i = 0; i < width; i++)
        {
            ElementGrid wallExternElement = InstantiateElementGrid(prefabWall, tiles[i, width-1].Position, rotationBot);
            tiles[i, width-1].Content = wallExternElement;
            
        }
        for (int i = 0; i < height; i++)
        {
            ElementGrid wallExternElement = InstantiateElementGrid(prefabWall, tiles[0, i].Position, rotationRight);
            setAlphaWall(wallExternElement, 0);
            tiles[0, i].Content = wallExternElement;
            
        }
        for (int i = 0; i < height; i++)
        {
            ElementGrid wallExternElement = InstantiateElementGrid(prefabWall, tiles[height-1, i].Position, rotationLeft);
            tiles[height-1, i].Content = wallExternElement;
        }

    }

    public void GenerateGate(Tile[,] tiles, bool isExitGate)
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
                    rotation = rotationTop;
                    NextToGatePosition = new Vector2Int(currentTile.Position.x, currentTile.Position.y+1);
                    break;
                case 1:
                    currentTile = tiles[_myRandPosition, width-1]; 
                    rotation = rotationBot;
                    NextToGatePosition = new Vector2Int(currentTile.Position.x, currentTile.Position.y - 1);
                    break;
                case 2:
                    currentTile = tiles[0, _myRandPosition];
                    rotation = rotationRight;
                    NextToGatePosition = new Vector2Int(currentTile.Position.x + 1, currentTile.Position.y);
                    break;
                case 3:
                    currentTile = tiles[height-1, _myRandPosition];
                    rotation = rotationLeft;
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