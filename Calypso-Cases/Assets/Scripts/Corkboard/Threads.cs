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

            if (Physics.Raycast(ray, out hit)) {

                Vector3 hitPoint = hit.point;

                CorkboardEvidence reference = hit.transform.gameObject.GetComponent<CorkboardEvidence>();
                threadPoints.Add(reference.getName()); 

                if (currentThread.Count == 0)
                {
                    currentLineRenderer = Instantiate(linePrefab).GetComponent<LineRenderer>();
                    currentThread.Add(hit.collider.bounds.center);
                    StartConnecting();
                    UpdateLineRenderer();
                }
                else
                {
                    if (Vector3.Distance(currentThread[currentThread.Count - 1], hitPoint) > 0.1f)
                    {
                        currentThread.Add(hit.collider.bounds.center);
                        UpdateLineRenderer();
                        FinishCurrentThread();
                    }
                }
            }
        }    
        if(currentThread.Count > 0 && isConnecting)
        {
            ConnectToMouse();
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
