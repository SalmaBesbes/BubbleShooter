using System.IO;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    SpriteRenderer Background;
    Bubble bubblePrefab;

    Bubble shooterBubble;
    Bubble NextBubble;

    float bubbleSize;

    bool shoot=false;

    void Awake()
    {

        GameManager.Instance.RegisterForSingleOnEventOccured((this, "InitializeGame"), Init);
        GameManager.Instance.RegisterForOnEventOccured((this, "Shoot"), () =>
        {
            shoot = true;

        });

    }

    // Update is called once per frame
    void Init()
    {
        Background = GameManager.Instance.GetBackground();
        bubblePrefab = GameManager.Instance.GetBubblePrefab();
        bubbleSize = GameManager.Instance.GetBubbleSize();


        CreateShooterBubble();
        CreateNextShooterBubble();
    }

    void CreateShooterBubble()
    {

        Bubble bubble = Instantiate(bubblePrefab);
        bubble.transform.position = new Vector3(Background.bounds.center.x, Background.bounds.min.y, 0);
        bubble.transform.localScale = new Vector3(bubbleSize, bubbleSize, bubbleSize);
        bubble.Collider.enabled = false;
        shooterBubble = bubble;
        GameManager.Instance.SetShooterBubble(bubble);

        var data = GameManager.Instance.config.SpawnableBubbleData.Next();

        bubble.SetNumber(data.Number);
        bubble.SetColor(data.Color);

    }

    void CreateNextShooterBubble()
    {
        var bubbleSize = GameManager.Instance.GetBubbleSize();
        Bubble bubble = Instantiate(bubblePrefab);
        bubble.Collider.enabled = false;
        bubble.transform.position = new Vector3(Background.bounds.center.x - bubbleSize, Background.bounds.min.y, 0);
        bubble.transform.localScale = new Vector3(3*bubbleSize/4, 3 * bubbleSize / 4, 3 * bubbleSize / 4);
        var data = GameManager.Instance.config.SpawnableBubbleData.Next();
        NextBubble = bubble;

        bubble.SetNumber(data.Number);
        bubble.SetColor(data.Color);
    }

    int i = 0;

    private void FixedUpdate()
    {
        
        if (shoot)
        {
            Vector3[] path = GameManager.Instance.GetShootPath();
            if (shooterBubble.transform.position != path[i])
            {
                Vector2 newPosition = Vector2.MoveTowards(shooterBubble.transform.position, path[i], Time.fixedDeltaTime * 25f);

                shooterBubble.Rigidbody.MovePosition(newPosition);
            }
            else if (i < path.Length-1)
            {
                i++;
            }else
            {
                shoot = false;
                i = 0;
                shooterBubble.onReadyToCheckForMerge.Call(shooterBubble);
                PrepNextBubble();
            }
        }
        
        
    }
    public void PrepNextBubble()
    {
        NextBubble.transform.position = Vector3.Lerp(NextBubble.transform.position, new Vector3(Background.bounds.center.x, Background.bounds.min.y, 0), 2f);
        NextBubble.transform.localScale = Vector3.Lerp(NextBubble.transform.localScale, new Vector3(bubbleSize, bubbleSize, bubbleSize), 2f);
        shooterBubble = NextBubble;
        GameManager.Instance.SetShooterBubble(shooterBubble);


        CreateNextShooterBubble();
    }
}
