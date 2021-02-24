using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralLevelGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject Block;
    [SerializeField]
    GameObject WinZone;
    public float BlockWidth;
    public float BlockHeight;
    [SerializeField]
    int GridWidth = 50;
    [SerializeField]
    int GridHeight = 20;
    [SerializeField]
    int MaxJumpHeight = 2;
    [SerializeField]
    float JumpPower=25;
    [SerializeField]
    float GravityPower = 1;
    [SerializeField]
    float JumpXVelocity = 10;
    [SerializeField]
    int MaxFallDepth = 5;
    int JumpDistance = 4;
    private List<List<int>> grid;
    double jumpChance = 0.93;
    float timeToJumpApex;
    Quaternion[] possibleRotations= { 
        Quaternion.identity,Quaternion.Euler(new Vector3(90,0,0)),
        Quaternion.Euler(0,90,0), Quaternion.Euler(0,0,90)
    };

    private float GetJumpHeightAtTime(float jumpTime)
    {
        return JumpPower*jumpTime - GravityPower / 2.0f * Mathf.Pow(jumpTime, 2);
    }
    // Start is called before the first frame update
    void Start()
    {
        //MaxJumpHeight = JumpPower;
        var timeToJumpApex = JumpPower / GravityPower;
        MaxJumpHeight = Mathf.FloorToInt(GetJumpHeightAtTime(timeToJumpApex));
        MaxJumpHeight--; //make it easier
        var rand = new System.Random();
        grid = new List<List<int>>();
        for(int x=0; x<GridWidth; x++)
        {
            grid.Add( new List<int>());
            for(int y=0; y<GridHeight; y++)
            {
                grid[x].Add(-1);
            }
        }

        int xPos = 0;
        int yPos = GridHeight/2;
        for(int i=0; i<GridWidth; i++)
        {
            grid[xPos][yPos] = rand.Next(possibleRotations.Length);
            int dX = 0;

            dX = rand.Next(JumpDistance-1) + 1;
            if (xPos + dX < GridWidth)
            {
                xPos += dX;
            }
            else
            {
                break;
            }

            float jumpTime = dX / JumpXVelocity;
            float maxJumpHeight = jumpTime< timeToJumpApex ? MaxJumpHeight :  GetJumpHeightAtTime(jumpTime);

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
        Instantiate(WinZone, new Vector3((xPos-1) * BlockWidth, winyPosition, 0),Quaternion.identity);

        for (int x = 1; x < GridWidth; x++)
        {
            for (int y = 0; y < GridHeight; y++)
            {
                if (grid[x][y] >= 0)
                {
                    float yPosition = (y -GridHeight/2 - 0.5f) * BlockHeight / 2;
                    Instantiate(Block, new Vector3(x * BlockWidth, yPosition, 0), possibleRotations[grid[x][y]]);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
