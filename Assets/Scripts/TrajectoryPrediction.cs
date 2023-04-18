using System.Collections.Generic;
using UnityEngine;

public class TrajectoryPrediction : MonoBehaviour
{


    LineRenderer _line;

    LineRenderer Line { get { if (_line == null ) return GetComponent<LineRenderer>(); return _line; } }

    public int reflectionCount = 2;

    public void RotateLine() {

        Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void EnablePrediction()
    {
        Line.enabled = true;
    }

    public void DisablePrediction()
    {
        Line.enabled = false;
        Line.positionCount = 0;
    }


    public void SetPredictionTrajectory(Vector3[] points)
    {
        Line.positionCount = points.Length;
        Line.SetPositions(points);
    }

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
                    Debug.DrawLine(origin, hit.point, Color.red);

                    hit = Physics2D.Raycast(origin, direction);
                    pos.Add(hit.point);

                }
                SetPredictionTrajectory(pos.ToArray());
            }
        }
        else
        {
            DisablePrediction();
        }
    }




}
