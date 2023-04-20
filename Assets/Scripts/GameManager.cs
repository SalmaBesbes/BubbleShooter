using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : EventManager
{

    public static GameManager Instance { get; private set; }

    public Config config;

    SceneContext scene;

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;   
        }
    }

    private void Start()
    {
        scene = FindObjectOfType<SceneContext>();
        if (scene == null)
        {
            Debug.LogError("Scene Context is Missing ! ");
        }
        else
        {
            if (config)
            {
                config.initData();
                CalculateBubbleSize();

            }
            TriggerEvent("InitializeGame");
        }

        
    }

    void CalculateBubbleSize()
    {

        var bubbleSize = (scene.Background.bounds.size.x + (scene.Background.bounds.size.x / config.bubblePerRow) / 2) / config.bubblePerRow;
        SetBubbleSize(bubbleSize);
    }

    public SpriteRenderer GetBackground()
    {
        return scene.Background;
    }

    public int GetBubblePerRow()
    {
        return config.bubblePerRow;
    }

    public int GetInitialRowNumber()
    {
        return config.initialRowsNumber;
    }

    public Bubble GetBubblePrefab()
    {
        return config.bubblePrefab;
    }

    public void SetBubbleSize(float size)
    {
        scene.bubbleSize = size;
    }

    public float GetBubbleSize()
    {
        return scene.bubbleSize;
    }

    public void SetShooterBubble(Bubble b)
    {
        scene.mainBubble = b;
        TriggerEvent("ShooterBubbleReady");
    }

    public Bubble GetShooterBubble()
    {
        return scene.mainBubble; 
    }

    public void SetShootDirection(Vector3 direction)
    {
        scene.shootDirection = direction;
    }
    public Vector3 GetShootDirection()
    {
        return scene.shootDirection;
    }

    public void SetShootPath(Vector3[] path)
    {
        scene.shootPath = path;
    }

    public Vector3[] GetShootPath()
    {
        return scene.shootPath;
    }



    public void SetCurrentBubbles(List<Bubble> bubbles)
    {
        scene.currentBubbles = bubbles;
    }


    public List<Bubble> GetCurrentBubbles()
    {
        return  scene.currentBubbles;
    }

}
