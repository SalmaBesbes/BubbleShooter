using TMPro;
using UnityEngine;

public class Bubble : MonoBehaviour
{

    float value;
    [SerializeField] private TextMeshPro valueText;

    public void SetNumber(float val)
    {
        value = val;
        valueText.text = value < 1024 ? value.ToString() : (value / 1024f).ToString() + "K";

    }

    public float GetNumber()
    {
        return value;
    }

    
}
