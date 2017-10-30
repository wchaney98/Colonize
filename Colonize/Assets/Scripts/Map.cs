using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Point
{
    public int x;
    public int y;
}

public class Map
{
    public int CellSize { get; private set; }

    private int[,] map;
    private List<Point> resourceSpots;

    public Map(int mapSize, int cellSize)
    {
        map = new int[mapSize, mapSize];
        CellSize = cellSize;
        resourceSpots = new List<Point>();
        PopulateMap(mapSize);
    }

    private void PopulateMap(int size)
    {

        int centerCell = size / 2;
        Point startingSpot;
        startingSpot.x = centerCell + Random.Range(-size / 3, size / 3);
        startingSpot.y = centerCell + Random.Range(-size / 3, size / 3);

        resourceSpots.Add(startingSpot);

        // TODO: Add corruption
    }
}
