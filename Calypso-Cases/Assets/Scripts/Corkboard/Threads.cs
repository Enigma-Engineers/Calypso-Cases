using System.Collections.Generic;
using UnityEngine;

public class Threads : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private List<Vector3> points = new List<Vector3>();
    private bool isDrawing = false;  // Tracks if we’re currently drawing a line

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Transform hitTransform = hit.transform;

                // Check if the hit object is a child of a pin or the pin itself
                if (hitTransform.CompareTag("Pin") || (hitTransform.parent != null && hitTransform.parent.CompareTag("Pin")))
                {
                    // Get the pin's position (parent's position if child is clicked)
                    Vector3 pinPosition = hitTransform.CompareTag("Pin") ? hitTransform.position : hitTransform.parent.position;

                    if (!isDrawing)
                    {
                        // Start a new line with the first pin
                        points.Clear();
                        points.Add(pinPosition);  // Add first pin's position
                        points.Add(pinPosition);  // Temporary second point to follow mouse
                        isDrawing = true;
                    }
                    else
                    {
                        // Finish the line at the second pin
                        points[1] = pinPosition;
                        isDrawing = false;
                    }

                    UpdateLineRenderer();
                }
            }
        }

        // If we're drawing, update the second point to follow the mouse position
        if (isDrawing)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            mousePosition.z = points[0].z;  // Keep the line on the same plane as the pins
            points[1] = mousePosition;
            UpdateLineRenderer();
        }
    }

    private void UpdateLineRenderer()
    {
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
}
