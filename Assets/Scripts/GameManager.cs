using UnityEngine;

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
            TriggerEvent("InitializeGame");
        }
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
    }

    public Bubble GetShooterBubble()
    {
        return scene.mainBubble; 
    }




}
