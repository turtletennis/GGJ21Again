using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum Direction
{
    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3
}
public class ProceduralLevelGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject Block;
    [SerializeField]
    GameObject WinZone;
    public float BlockWidth;
    public float BlockHeight;
    [SerializeField]
    int GridWidth = 64;
    [SerializeField]
    int GridHeight = 32;
    [SerializeField]
    int SectionGridWidth = 4;
    [SerializeField]
    int SectionGridHeight = 4;
    [SerializeField]
    int MaxJumpHeight = 2;
    [SerializeField]
    float JumpPower = 25;
    [SerializeField]
    float GravityPower = 1;
    [SerializeField]
    float JumpXVelocity = 10;
    [SerializeField]
    int MaxFallDepth = 5;
    int JumpDistance = 4;
    private List<List<int>> grid;
    private List<List<bool>> sectionGrid;
    private List<List<bool>> sectionWallsHorizontal;
    private List<List<bool>> sectionWallsVertical;
    double jumpChance = 0.93;
    float timeToJumpApex;
    Quaternion[] possibleRotations = {
        Quaternion.identity,Quaternion.Euler(new Vector3(90,0,0)),
        Quaternion.Euler(0,90,0), Quaternion.Euler(0,0,90)
    };

    private float GetJumpHeightAtTime(float jumpTime)
    {
        return JumpPower * jumpTime - GravityPower / 2.0f * Mathf.Pow(jumpTime, 2);
    }
    // Start is called before the first frame update
    void Start()
    {
        //MaxJumpHeight = JumpPower;
        var timeToJumpApex = JumpPower / GravityPower;
        MaxJumpHeight = Mathf.FloorToInt(GetJumpHeightAtTime(timeToJumpApex));
        MaxJumpHeight--; //make it easier

        var rand = new System.Random();
        InitialiseGrids();
        GenerateCells(rand);
        int xPos = 0;
        int yPos = GridHeight / 2;
        for (int i = 0; i < GridWidth; i++)
        {
            grid[xPos][yPos] = rand.Next(possibleRotations.Length);
            int dX = 0;

            dX = rand.Next(JumpDistance - 1) + 1;
            if (xPos + dX < GridWidth)
            {
                xPos += dX;
            }
            else
            {
                break;
            }

            float jumpTime = dX / JumpXVelocity;
            float maxJumpHeight = jumpTime < timeToJumpApex ? MaxJumpHeight : GetJumpHeightAtTime(jumpTime);

            int dY = 0;
            if (rand.NextDouble() < jumpChance)
            {
                //jump
                dY = rand.Next(Mathf.CeilToInt(maxJumpHeight));
                if (yPos + dY < GridHeight)
                {
                    yPos += dY;
                }
            }
            else
            {
                //fall
                dY = rand.Next(MaxFallDepth);
                if (yPos - dY > 0)
                {
                    yPos -= dY;
                }

            }

        }
        float winyPosition = (yPos - GridHeight / 2 - 0.5f) * BlockHeight / 2;
        Instantiate(WinZone, new Vector3((xPos - 1) * BlockWidth, winyPosition, 0), Quaternion.identity);

        for (int x = 1; x < GridWidth; x++)
        {
            for (int y = 0; y < GridHeight; y++)
            {
                if (grid[x][y] >= 0)
                {
                    float yPosition = (y - GridHeight / 2 - 0.5f) * BlockHeight / 2;
                    Instantiate(Block, new Vector3(x * BlockWidth, yPosition, 0), possibleRotations[grid[x][y]]);
                }
            }
        }
    }

    private Direction? Opposite(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Direction.Down;
            case Direction.Right:
                return Direction.Left;
            case Direction.Down:
                return Direction.Up;
            case Direction.Left:
                return Direction.Right;
            default:
                return null;
        }
    }

    private void GenerateCells(System.Random rand)
    {
        List<Direction> pathTaken = new List<Direction>();
        int xPos = rand.Next(SectionGridWidth / 4);
        int yPos = rand.Next(SectionGridHeight);
        string path = $"{xPos},{yPos}";
        sectionGrid[xPos][yPos] = true;
        bool endLevel = false;
        Direction? previousDirection = null;
        while (!endLevel)
        {
            Direction direction = (Direction)rand.Next(4);

            //don't go the way we just came from
            while (previousDirection == Opposite(direction))
            {
                direction = (Direction)rand.Next(4);
            }

            if (direction == Direction.Up && yPos >= SectionGridHeight - 1)
            {   //reached top of world, go right instead of up unless we just came from there
                if (previousDirection != Direction.Left)
                {
                    direction = Direction.Right;
                }
                else
                {
                    direction = Direction.Down;
                }
            }

            if (direction == Direction.Left && xPos == 0)
            {   //reached left of world, go up unless we just came from there
                if (yPos < SectionGridHeight - 1 && previousDirection != Direction.Down)
                {
                    direction = Direction.Up;
                }
                else
                {
                    direction = Direction.Right;
                }
            }

            if (direction == Direction.Down && yPos == 0)
            {
                direction = Direction.Right;
            }

            if (direction == Direction.Right && xPos >= SectionGridWidth - 1)
            {
                xPos--;//to avoid overflow, because it's going to be incremented
                endLevel = true;
            }

            switch (direction)
            {
                case Direction.Up:
                    yPos += 1;
                    break;
                case Direction.Down:
                    yPos -= 1;
                    break;
                case Direction.Right:
                    xPos += 1;
                    break;
                case Direction.Left:
                    xPos -= 1;
                    break;
            }
            sectionGrid[xPos][yPos] = true;
            pathTaken.Add(direction);
            AddSectionWalls(xPos, yPos, direction, previousDirection);
            previousDirection = direction;

        }
        LogGrid<bool>(sectionGrid);
        path += string.Join(",", pathTaken);
        Debug.Log(path);

    }

    private void AddSectionWalls(int x, int y, Direction currentDirection, Direction? previousDirection)
    {
        if (y + 1 < SectionGridHeight)
        {
            //if we're not going up and we didn't come from above, so put a wall there, otherwise remove it
            sectionWallsHorizontal[x][y + 1] = (currentDirection != Direction.Up && previousDirection != Direction.Down);
        }
        //if we're not going down and we didn't come from below, so put a wall there, otherwise remove it
        sectionWallsHorizontal[x][y] = (currentDirection != Direction.Down && previousDirection != Direction.Up);
        if (x + 1 < SectionGridWidth)
        {
            //if we're not going right and we didn't come from the right, so put a wall there, otherwise remove it
            sectionWallsVertical[x + 1][y] = (currentDirection != Direction.Right && previousDirection != Direction.Left);
        }
        //if we're not going left and we didn't come from the left, so put a wall there, otherwise remove it
        sectionWallsVertical[x][y] = (currentDirection != Direction.Left && previousDirection != Direction.Right);
    }

    private void LogGrid<T>(List<List<T>> grid)
    {
        string gridString = "";
        for (int y = grid[0].Count - 1; y >= 0; y--)
        {
            for (int x = 0; x < grid.Count; x++)
            {
                gridString = gridString + grid[x][y].ToString() + "\t";
            }
            gridString += "\n";
        }
        Debug.Log(gridString);
    }

    private void InitialiseGrids()
    {
        grid = new List<List<int>>();
        for (int x = 0; x < GridWidth; x++)
        {
            grid.Add(new List<int>());
            for (int y = 0; y < GridHeight; y++)
            {
                grid[x].Add(-1);
            }
        }

        sectionGrid = new List<List<bool>>();
        sectionWallsHorizontal = new List<List<bool>>();
        sectionWallsVertical = new List<List<bool>>();
        for (int x = 0; x <= SectionGridWidth; x++)
        {
            if (x < SectionGridWidth)
            {
                sectionGrid.Add(new List<bool>());
                sectionWallsHorizontal.Add(new List<bool>());
            }
            sectionWallsVertical.Add(new List<bool>());

            for (int y = 0; y <= SectionGridHeight; y++)
            {
                if (y < SectionGridHeight)
                {
                    if (x < SectionGridWidth)
                    {
                        sectionGrid[x].Add(false);
                    }
                    if (y < SectionGridHeight)
                        sectionWallsVertical[x].Add(x == 0 || x == SectionGridWidth); //left and right edges of world
                }
                if (x < SectionGridWidth)
                {
                    sectionWallsHorizontal[x].Add(y == 0 || y == SectionGridHeight); //top and bottom edges of world
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
