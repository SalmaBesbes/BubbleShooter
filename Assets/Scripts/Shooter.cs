using UnityEngine;

public class Shooter : MonoBehaviour
{
    SpriteRenderer Background;
    Bubble bubblePrefab;

    Bubble shooterBubble;
    Bubble NextBubble;

    void Start()
    {
        GameManager.Instance.RegisterForSingleOnEventOccured((this, "InitializeGame"), Init);
    }

    // Update is called once per frame
    void Init()
    {

        Background = GameManager.Instance.GetBackground();
        bubblePrefab = GameManager.Instance.GetBubblePrefab();


        CreateShooterBubble();
        CreateNextShooterBubble();
    }

    void CreateShooterBubble()
    {
        var bubbleSize = GameManager.Instance.GetBubbleSize();
        Bubble bubble = Instantiate(bubblePrefab);
        bubble.transform.position = new Vector3(Background.bounds.center.x, Background.bounds.min.y, 0);
        bubble.transform.localScale = new Vector3(bubbleSize, bubbleSize, bubbleSize);
        bubble.Collider.enabled = false;
        shooterBubble = bubble;
        GameManager.Instance.SetShooterBubble(bubble);

    }

    void CreateNextShooterBubble()
    {
        var bubbleSize = GameManager.Instance.GetBubbleSize();
        Bubble bubble = Instantiate(bubblePrefab);
        bubble.Collider.enabled = false;
        bubble.transform.position = new Vector3(Background.bounds.center.x - bubbleSize, Background.bounds.min.y, 0);
        bubble.transform.localScale = new Vector3(3*bubbleSize/4, 3 * bubbleSize / 4, 3 * bubbleSize / 4);

    }
}
