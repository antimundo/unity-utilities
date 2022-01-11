using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterCameraBetweenPoints : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform[] points;
    [SerializeField] LineRenderer[] lines;
    [SerializeField] float cameraSpeed = 3;

    void Update()
    {
        setPosition();
        setSize();
    }

    private void setSize()
    {
        Vector2 longestVector = new Vector2();

        for(int i = 0; i < points.Length; i++)
        {
            if (lines.Length > 0)
            {
                lines[i].SetPosition(0, transform.position);
                lines[i].SetPosition(1, points[i].position);
            }

            Vector2 thisVector = transform.position - points[i].position;
            if (thisVector.magnitude > longestVector.magnitude)
            {
                longestVector = thisVector;
            }
        }

        cam.orthographicSize = Mathf.Lerp(
                cam.orthographicSize,
                longestVector.magnitude,
                Time.deltaTime * cameraSpeed
            );
    }

    private void setPosition()
    {
        Vector3 newPosition = new Vector3(0, 0, -10);

        foreach (Transform point in points)
        {
            newPosition.x += point.position.x;
            newPosition.y += point.position.y;
        }
        newPosition.x /= points.Length;
        newPosition.x = Mathf.Lerp(transform.position.x, newPosition.x, Time.deltaTime * cameraSpeed);

        newPosition.y /= points.Length;
        newPosition.y = Mathf.Lerp(transform.position.y, newPosition.y, Time.deltaTime * cameraSpeed);

        cam.transform.position = newPosition;
    }

}
