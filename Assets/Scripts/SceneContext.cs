using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// scene context is a class that holds all the data existing in the scene which will be accessible via gameManager
/// </summary>
public class SceneContext : MonoBehaviour
{

    public float bubbleSize = 1f;
    public Bubble mainBubble;

    public List<Bubble> currentBubbles;

    public SpriteRenderer Background;

    [HideInInspector]
    public Vector3 shootDirection;

    [HideInInspector]
    public Vector3[] shootPath;

}
