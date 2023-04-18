
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public Transform hexPrefab;

    public SpriteRenderer Background;

    public int bubblePerRow = 6;
    public int initialRow = 3;


    Vector2 startPos;
    float bubbleSize;


    void Start()
    {


        bubbleSize =( Background.bounds.size.x + (Background.bounds.size.x / bubblePerRow) / 2)/bubblePerRow;

        startPos = new Vector2(Background.bounds.min.x + bubbleSize / 2, Background.bounds.max.y );


        CreateGrid();

    }

    Vector3 CalcWorldPos(Vector2 gridPos)
    {
        float offset = 0;
        if (gridPos.y % 2 != 0)
            offset = bubbleSize / 2;

        float x = startPos.x + gridPos.x * bubbleSize+offset;
        float y = startPos.y - gridPos.y * bubbleSize;



        return new Vector3(x, y, 0);
    }

    void CreateGrid()
    {
        for (int y = 0; y < initialRow; y++)
        {
            for (int x = 0; x < bubblePerRow; x++)
            {
                Transform hex = Instantiate(hexPrefab) as Transform;
                Vector2 gridPos = new Vector2(x, y);
                hex.position = CalcWorldPos(gridPos);
                hex.parent = this.transform;
            }
        }
    }
}