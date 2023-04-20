using System.Linq;
using TMPro;
using UnityEngine;

public class Bubble : MonoBehaviour
{

    float value;
    [SerializeField] private TextMeshPro valueText;

     private Collider2D _collider;

    public Collider2D Collider { get { return _collider == null ? GetComponent<Collider2D>(): _collider; } }

    private SpriteRenderer _spriteRenderer;

    public SpriteRenderer SpriteRenderer { get { return _spriteRenderer == null ? GetComponent<SpriteRenderer>() : _spriteRenderer; } }


    private Rigidbody2D _rigidbody;

    public Rigidbody2D Rigidbody { get { return _rigidbody == null ? GetComponent<Rigidbody2D>() : _rigidbody; } }

    public void SetNumber(float val)
    {
        value = val;
        valueText.text = value < 1024 ? value.ToString() : (value / 1024f).ToString() + "K";

    }

    public float GetNumber()
    {
        return value;
    }


    public void SetColor(Color color)
    {
        SpriteRenderer.color = color;
    }

    public Color GetColor()
    {
        return SpriteRenderer.color;
    }

    public CustomEvent<Bubble> onReadyToCheckForMerge = new CustomEvent<Bubble>("readyToCheckForMerge");

    
}
