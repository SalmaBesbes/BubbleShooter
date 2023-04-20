
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class HexGrid : MonoBehaviour
{
    
    Vector2 startPos;
    float bubbleSize;

    SpriteRenderer Background;
    int bubblePerRow;
    int initialRowsNumber;
    Bubble bubblePrefab;

    void Awake()
    {
        GameManager.Instance.RegisterForSingleOnEventOccured((this, "InitializeGame"), Init);
    }

    // Update is called once per frame
    void Init()
    {
        Background = GameManager.Instance.GetBackground();
        bubblePerRow = GameManager.Instance.GetBubblePerRow();
        initialRowsNumber = GameManager.Instance.GetInitialRowNumber();
        bubblePrefab = GameManager.Instance.GetBubblePrefab();
        bubbleSize = GameManager.Instance.GetBubbleSize();

        CalculateStartingPosition();
        CreateGrid();

        CreateGridEdges();



    }

    void CalculateStartingPosition()
    {

        startPos = new Vector2(Background.bounds.min.x + bubbleSize / 2, Background.bounds.max.y);

    }

    Vector3 CalculateBubblePosition(Vector2 gridPos)
    {
        float offset = 0;
        if (gridPos.y % 2 != 0)
            offset = bubbleSize / 2;

        float x = startPos.x + gridPos.x * bubbleSize+offset;
        float y = startPos.y - gridPos.y * bubbleSize;



        return new Vector3(x, y, 0);
    }
     List<Bubble> bubbles = new List<Bubble>();
    
    void CreateGrid()
    {
        for (int y = 0; y < initialRowsNumber; y++)
        {
            for (int x = 0; x < bubblePerRow; x++)
            {
                Bubble bubble = Instantiate(bubblePrefab);
                Vector2 gridPos = new Vector2(x, y);
                bubble.transform.position = CalculateBubblePosition(gridPos);
                bubble.transform.localScale = new Vector3(bubbleSize, bubbleSize, bubbleSize);
                bubble.transform.parent = this.transform;


                var data = GameManager.Instance.config.SpawnableBubbleData.Next();

                bubble.SetNumber(data.Number);
                bubble.SetColor(data.Color);

                bubble.name = "Bubble " + y + "|" + x; 
                bubbles.Add(bubble);

            }
        }
        GameManager.Instance.SetCurrentBubbles(bubbles);
    }

    void CreateGridEdges()
    {
        GameObject Edges = new GameObject("Edges");
        GameObject Left = new GameObject("Left");
        GameObject Right = new GameObject("Right");
        Left.transform.parent = Edges.transform;
        Right.transform.parent = Edges.transform;

        EdgeCollider2D leftEdge = Left.AddComponent<EdgeCollider2D>();
        EdgeCollider2D rightEdge = Right.AddComponent<EdgeCollider2D>();

        leftEdge.points = new Vector2[] { new Vector2(Background.bounds.min.x, Background.bounds.min.y), new Vector2(Background.bounds.min.x, Background.bounds.max.y) };
        rightEdge.points = new Vector2[] { new Vector2(Background.bounds.max.x, Background.bounds.min.y), new Vector2(Background.bounds.max.x, Background.bounds.max.y) };

    }


    
}