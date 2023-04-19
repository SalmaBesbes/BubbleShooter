using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// scene context is a class that holds all the data existing in the scene which will be accessible via gameManager
/// </summary>
public class SceneContext : MonoBehaviour
{

    public float bubbleSize;
    public Bubble mainBubble;

    public List<Bubble> currentBubbles;

    public SpriteRenderer Background; 

}
