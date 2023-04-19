using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "Game/Config")]
public sealed class Config : ScriptableObject
{
    [SerializeField] public Bubble bubblePrefab;
    [SerializeField] private BubbleData[] bubbleData;
    [SerializeField] public int initialRowsNumber ;
    [SerializeField] public int bubblePerRow;
    
    public IReadOnlyList<BubbleData> BubbleData => bubbleData;

}

[Serializable]
public struct BubbleData
{
    public int Number;
    public Color32 Color;
}