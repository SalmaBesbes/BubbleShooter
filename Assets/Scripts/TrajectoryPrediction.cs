using System.Collections.Generic;
using UnityEngine;

public class TrajectoryPrediction : MonoBehaviour
{


    LineRenderer _line;

    LineRenderer Line { get { if (_line == null ) return GetComponent<LineRenderer>(); return _line; } }

    public int reflectionCount = 2;

    public SpriteRenderer PredictionBubble;


    private void Start()
    {
        Line.enabled = false;
    }
    public void RotateLine() {

        Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void EnablePrediction()
    {
        Line.enabled = true;
        Line.positionCount = 0;

        PredictionBubble.enabled = true;

    }

    public void DisablePrediction()
    {
        Line.enabled = false;
        PredictionBubble.enabled = false;

    }


    public void SetPredictionTrajectory(Vector3[] points)
    {
        Line.positionCount = points.Length;
        Line.SetPositions(points);
    }
    //SUPER MESSY WAY, BUT I HAD NO CHOICE, DEADLINE IS GETTING CLOSER
    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            EnablePrediction();
            RotateLine();

            Vector2 direction = Line.transform.right;
            Vector2 origin = (Vector2)Line.transform.position;
            var hit = Physics2D.Raycast(origin, direction);
            if (hit.collider!= null)
            {

            var bubble = hit.collider.GetComponent<Bubble>();
            List<Vector3> pos = new()
            {
                Line.transform.position,
                hit.point
            };

                if (bubble == null)
                {
                    direction = Vector2.Reflect(direction.normalized, hit.normal);
                    origin = hit.point *direction;

                    hit = Physics2D.Raycast(origin, direction);
                    pos.Add(hit.point);

                    bubble = hit.collider.GetComponent<Bubble>();
                    if (bubble == null)
                    {
                        DisablePrediction();

                    }

                }

                if (PredictionBubble.enabled)
                {
                    //Things got a bit messy here 
                    var bg = GameManager.Instance.GetBackground();


                    var targetPos = new Vector3(hit.point.x , hit.collider.transform.position.y - GameManager.Instance.GetBubbleSize());

                    PredictionBubble.transform.position = new Vector3(
                        Mathf.Clamp(RoundToNearestGrid(targetPos.x, GameManager.Instance.GetBubbleSize()), bg.bounds.min.x+ GameManager.Instance.GetBubbleSize()/2, bg.bounds.max.x- GameManager.Instance.GetBubbleSize() / 2),
                        Mathf.Clamp(targetPos.y, bg.bounds.min.y+ GameManager.Instance.GetBubbleSize() / 2, bg.bounds.max.y- GameManager.Instance.GetBubbleSize() / 2),
                        targetPos.z);
                    
                    
                    PredictionBubble.transform.localScale = new Vector3(GameManager.Instance.GetBubbleSize(), GameManager.Instance.GetBubbleSize(), GameManager.Instance.GetBubbleSize());

                }

                List<Vector3> shootPath = new(pos);
                shootPath[shootPath.Count - 1] = PredictionBubble.transform.position;

                SetPredictionTrajectory(pos.ToArray());
                GameManager.Instance.SetShootPath(shootPath.ToArray());
            }
        }
        else
        {
            if (Line.enabled)
            {
                GameManager.Instance.SetShootDirection(Line.transform.right);
                GameManager.Instance.TriggerEvent("Shoot");
            }
            DisablePrediction();
        }
    }

    float RoundToNearestGrid(float pos,float unitSize)
    {
        float xDiff = pos % unitSize;
        pos -= xDiff;
        if (xDiff > (unitSize / 2))
        {
            pos += unitSize;
        }
        return pos;
    }


}
