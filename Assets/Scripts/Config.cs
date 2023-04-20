using System;
using System.Collections.Generic;
using UnityEngine;
using KaimiraGames;


[CreateAssetMenu(fileName = "Config", menuName = "Game/Config")]
public sealed class Config : ScriptableObject
{
    [SerializeField] public Bubble bubblePrefab;
    [SerializeField] private List<BubbleData> bubblesPresets;
    [SerializeField] public int initialRowsNumber ;
    [SerializeField] public int bubblePerRow;

    private WeightedList<BubbleData> weightedBubbleData = new();

    public void initData()
    {
        foreach (var item in bubblesPresets)
        {
            if (item.weight==1) return;
            weightedBubbleData.Add(item, item.weight);
        }
    }

    public WeightedList<BubbleData> SpawnableBubbleData => weightedBubbleData;

    [HideInInspector]
    public List<BubbleData> BubbblePresets => bubblesPresets;


}

[Serializable]
public struct BubbleData
{
    public int Number;
    [Range(1,10)]
    public int weight;
    public Color32 Color;
}

public struct MergeBubble
{
    
    public Bubble mainBubble;
        public List<Bubble> Bubbles;

    public MergeBubble(Bubble mainBubble, List<Bubble> bubbles)
    {
        this.mainBubble = mainBubble;
        this.Bubbles = bubbles;
    }
}