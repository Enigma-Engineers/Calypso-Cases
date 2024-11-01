using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class Threads : MonoBehaviour
{
    public GameObject linePrefab;
    private List<LineRenderer> lineRenderers = new List<LineRenderer>();
    private List<Vector3> currentThread = new List<Vector3>();
    private List<string> threadPoints = new List<string>();
    private LineRenderer currentLineRenderer;
    private bool isConnecting = false;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Transform hitTransform = hit.transform;

                CorkboardEvidence reference = hit.transform.gameObject.GetComponent<CorkboardEvidence>();
                threadPoints.Add(reference.getName());

                // Check if the hit object is a child of a pin or the pin itself
                if (hitTransform.CompareTag("Evidence") || (hitTransform.parent != null && hitTransform.parent.CompareTag("Evidence")))
                {
                    // Get the pin's position (parent's position if child is clicked)
                    Vector3 pinPosition = hitTransform.CompareTag("Evidence") ? hitTransform.position : hitTransform.parent.position;

                    if (!isConnecting)
                    {
                        currentLineRenderer = Instantiate(linePrefab).GetComponent<LineRenderer>();
                        // Start a new line with the first pin
                        currentThread.Clear();
                        currentThread.Add(pinPosition);  // Add first pin's position
                        currentThread.Add(pinPosition);  // Temporary second point to follow mouse
                        isConnecting = true;
                    }
                    else
                    {
                        // Finish the line at the second pin
                        currentThread[1] = pinPosition;
                        isConnecting = false;
                    }

                    UpdateLineRenderer();
                }
            }
        }

        // If we're drawing, update the second point to follow the mouse position
        if (isConnecting)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            mousePosition.z = currentThread[0].z;  // Keep the line on the same plane as the pins
            currentThread[1] = mousePosition;
            UpdateLineRenderer();
        }
    }

    private void UpdateLineRenderer()
    {
        currentLineRenderer.positionCount = currentThread.Count + (isConnecting ? 1 : 0);

        currentLineRenderer.SetPositions(currentThread.ToArray());

        if(isConnecting)
        {
            UpdateMousePosition();
        }
    }

    private void UpdateMousePosition()
    {
        if(currentThread.Count > 0)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            currentLineRenderer.SetPosition(currentThread.Count, mousePosition);
        }
    }

    private void ConnectToMouse()
    {
        UpdateMousePosition();
    }

    private void StartConnecting()
    {
        isConnecting=true;
    }

    private void FinishCurrentThread()
    {
        if(currentThread.Count > 0)
        {
            currentThread.Clear();
            isConnecting = false;
        }
    }

    public List<string> getThreads()
    {
        return threadPoints;
    }
}
