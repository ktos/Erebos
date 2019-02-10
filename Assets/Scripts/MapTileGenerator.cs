using System;
using System.Collections.Generic;
using UnityEngine;

public class MapTileGenerator : MonoBehaviour
{
    [Header("Tiles")]
    public Transform floor;

    public Transform ceiling;
    public Transform wall;
    public Transform door;
    public Transform upstairs;
    public Transform downstairs;
    public Transform corridor;

    [Header("Items")]
    public Transform barrel;

    [Header("Dungeon Settings")]
    public int width;

    public int height;
    public int things;
    public int seed = -1;

    [Header("Miscellaneous")]
    public GameObject player;

    public TextAsset level;
    public GameObject sceneSwitcher;

    private void Start()
    {
        var r = new BasicRandom(seed);

        Dungeon dungeon;
        if (level == null)
        {
            DungeonGenerator d = new DungeonGenerator(r, s => { Debug.Log("Dungeon: " + s); });
            d.CreateDungeon(width, height, things);

            dungeon = new Dungeon();
            dungeon.Tiles = d.GetDungeonAs2D();
            dungeon.Start = new Tuple<int, int> { Item1 = -1, Item2 = -1 };
        }
        else
        {
            dungeon = DungeonGenerator.FromFile(level.text);
        }

        CreateLevel(dungeon.Tiles);

        CreateObjects(dungeon.Doodles);

        if (dungeon.Start.Item1 == -1)
            MovePlayerToRandomTile(r, dungeon.Tiles);
        else
            MovePlayerToTile(dungeon.Start.Item1, dungeon.Start.Item2);
    }

    private void CreateObjects(IEnumerable<Item> doodles)
    {
        foreach (var item in doodles)
        {
            switch (item.Type)
            {
                default:
                    Instantiate(barrel, new Vector3(item.X, item.Z, item.Y), Quaternion.Euler(-item.RotationX, -item.RotationY, -item.RotationZ));
                    break;
            }
        }
    }

    private void MovePlayerToRandomTile(BasicRandom r, TileType[,] dungeon)
    {
        var randomTile = dungeon[0, 0];
        int x = 0, y = 0;
        while (randomTile != TileType.Floor)
        {
            x = r.Next(0, dungeon.GetLength(0));
            y = r.Next(0, dungeon.GetLength(1));
            randomTile = dungeon[x, y];
        }

        MovePlayerToTile(x, y);
    }

    private void MovePlayerToTile(int x, int y)
    {
        player.transform.position = new Vector3(x, 1, y);
    }

    private void CreateLevel(TileType[,] tiles)
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                switch (tiles[i, j])
                {
                    case TileType.Floor:
                        Instantiate(floor, new Vector3(i, 0, j), Quaternion.identity);
                        Instantiate(ceiling, new Vector3(i, 4, j), Quaternion.Euler(-180f, 0, 0));
                        break;

                    case TileType.Wall:
                        var orientation = GetTileOrientation(tiles, i, j);
                        if (orientation == Direction.Unknown) orientation = Direction.North;

                        int rotate = 0;
                        switch (orientation)
                        {
                            case Direction.North:
                                rotate = 180;
                                break;

                            case Direction.East:
                                rotate = 270;
                                break;

                            case Direction.South:
                                rotate = 0;
                                break;

                            case Direction.West:
                                rotate = 90;
                                break;

                            case Direction.Unknown:
                                break;
                        }

                        Instantiate(wall, new Vector3(i, 2, j), Quaternion.Euler(0, rotate, 0));
                        break;

                    case TileType.Door:
                        orientation = GetTileOrientation(tiles, i, j);

                        Debug.Log(orientation);

                        int rotate2 = 0;
                        if (orientation == Direction.South)
                        {
                            tiles[i + 1, j] = TileType.Unused;
                        }
                        else if (orientation == Direction.East)
                        {
                            tiles[i, j + 1] = TileType.Unused;
                            rotate2 = 90;
                        }

                        float fixX = orientation == Direction.South ? 0.5f : 0;
                        float fixZ = orientation == Direction.East ? 0.5f : 0;

                        var createdDoor = Instantiate(door, new Vector3(i + fixX, 0, j + fixZ), Quaternion.Euler(0, rotate2, 0));

                        //Instantiate(ceiling, new Vector3(i, 4, j), Quaternion.Euler(-180f, 0, 0));
                        break;

                    default:
                        break;
                }
            }
        }
    }

    private Direction GetTileOrientation(TileType[,] tiles, int x, int y)
    {
        int maxx = tiles.GetLength(0);
        int maxy = tiles.GetLength(1);

        if (x > maxx || x < 0 || y > maxy || y < 0)
            throw new ArgumentOutOfRangeException();

        if (tiles[x, y] == TileType.Floor || tiles[x, y] == TileType.Unused)
            throw new ArgumentException("Floor or empty tiles are not oriented.");

        if (tiles[x, y] == TileType.Wall)
        {
            if (x + 1 <= maxx - 1 && tiles[x + 1, y] == TileType.Floor) return Direction.South;
            if (x - 1 >= 0 && tiles[x - 1, y] == TileType.Floor) return Direction.North;
            if (y + 1 <= maxy - 1 && tiles[x, y + 1] == TileType.Floor) return Direction.East;
            if (y - 1 >= 0 && tiles[x, y - 1] == TileType.Floor) return Direction.West;

            return Direction.Unknown;
        }

        if (tiles[x, y] == TileType.Door)
        {
            if (x + 1 <= maxx - 1 && tiles[x + 1, y] == TileType.Door) return Direction.South;
            if (y + 1 <= maxy - 1 && tiles[x, y + 1] == TileType.Door) return Direction.East;

            return Direction.Unknown;
        }

        throw new Exception("Unknown orientation: " + x + "," + y);
    }
}