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
    GameObject HorizontalCellWall;
    [SerializeField]
    GameObject VerticalCellWall;
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
    private float playerStartX;
    private float playerStartY;
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
        InstantiateSectionWalls();
        
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
        MovePlayerToStartPosition();
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

    private void MovePlayerToStartPosition()
    {
        var player = FindObjectOfType<CharacterController>();
        var currentPos = player.transform.position;
        player.transform.position = new Vector3(playerStartX, playerStartY, currentPos.z);
        
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

    private int vWallHeight = 8;
    private int hWallWidth = 8;
    private float wallThickness = 0.5f;
    private void InstantiateSectionWalls()
    {
        for (int y = 0; y <= SectionGridHeight; y++)
        {
            for (int x = 0; x <= SectionGridWidth; x++)
            {
                if (y < SectionGridHeight)
                {
                    if (sectionWallsVertical[x][y])
                    {
                        Instantiate(VerticalCellWall, new Vector3( (x-0.5f) * hWallWidth, (y+0.5f) * vWallHeight - wallThickness, 0), Quaternion.identity);
                    }

                }
                if (x < SectionGridWidth)
                {
                    if (sectionWallsHorizontal[x][y])
                    {
                        Instantiate(HorizontalCellWall, new Vector3((x) * hWallWidth + wallThickness / 2.0f, y * vWallHeight - wallThickness/2.0f, 0), Quaternion.identity);
                    }
                }
            }
        }
    }

    private void GenerateCells(System.Random rand)
    {
        List<Direction> pathTaken = new List<Direction>();
        int xPos = rand.Next(SectionGridWidth / 4);
        int yPos = rand.Next(SectionGridHeight);
        playerStartX = xPos*hWallWidth;
        playerStartY = yPos*vWallHeight;
        string path = $"{xPos},{yPos}\n";
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
                endLevel = true;
            }
            int newX = xPos;
            int newY = yPos;
            switch (direction)
            {
                case Direction.Up:
                    newY += 1;
                    break;
                case Direction.Down:
                    newY -= 1;
                    break;
                case Direction.Right:
                    newX += 1;
                    break;
                case Direction.Left:
                    newX -= 1;
                    break;
            }
            if (endLevel)
            {
                AddSectionWalls(xPos, yPos, direction, previousDirection);
                pathTaken.Add(direction);
                previousDirection = direction;
            }
            else if (!sectionGrid[newX][newY]) //only progress if we're not going back to a previous cell
            {
                AddSectionWalls(xPos, yPos, direction, previousDirection);
                xPos = newX;
                yPos = newY;
                sectionGrid[xPos][yPos] = true;
                pathTaken.Add(direction);
                
                previousDirection = direction;
            }

        }
        LogGrid<bool>(sectionGrid);
        LogSectionWalls();
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
    private void LogSectionWalls()
    {
        string wallsString = "";
        for (int y = SectionGridHeight; y >= 0; y--)
        {
            for (int x = 0; x <= SectionGridWidth; x++)
            {
                if (y < SectionGridHeight && sectionWallsVertical[x][y])
                {
                    wallsString += "|";
                }
                else
                {
                    wallsString += " ";
                }


                if (x < SectionGridWidth)
                {
                    if (sectionWallsHorizontal[x][y])
                    {
                        wallsString += "_ ";
                    }
                    else
                    {
                        wallsString += "  ";
                    }
                }

            }
            wallsString += "\n";
        }
        Debug.Log(wallsString);
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
