using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    // the Grid itself
    public static int w = 10;
    public static int h = 20;
    public static Transform[,] grid = new Transform[w, h];

    // round vector coordinates function
    public static Vector2 roundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    // checks if given position is inside game border
    public static bool insideBorder(Vector2 pos)
    {
        return ((int) pos.x >= 0 && (int) pos.x < w && (int) pos.y >= 0);
    }

    // deletes a row of blocks from the screen
    public static void deleteRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    // makes a row of blocks go one line down
    public static void decreaseRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] != null)
            {
                // updates the grid values
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                // updates block position
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    // makes all rows above row y - 1 go one line down
    public static void decreaseRowsAbove(int y)
    {
        for (int i = y; i < h; ++i)
        {
            decreaseRow(i);
        }
    }

    // checks if a row is full
    public static bool isRowFull(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }

        return true;
    }

    // checks for full rows, deletes them and decreases rows above them
    public static void deleteFullRows()
    {
        for (int y = 0; y < h; ++y)
        {
            if (isRowFull(y))
            {
                deleteRow(y);
                decreaseRowsAbove(y + 1);
                --y;
            }
        }
    }

}
