using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Threads : MonoBehaviour
{
    public GameObject linePrefab;
    private GameObject levelManager;
    private SceneChange sceneChange;
    private List<LineRenderer> lineRenderers = new List<LineRenderer>();
    private List<Vector3> currentThread = new List<Vector3>();
    private List<string> threadPoints = new List<string>();
    private LineRenderer currentLineRenderer;
    private bool isConnecting = false;
    private int currentScene;

    private void Start()
    {
        levelManager = GameObject.Find("LevelManager");
        sceneChange = levelManager.gameObject.GetComponent<SceneChange>();
        currentScene = sceneChange.GetCurrentScene();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                LayerMask pinLayerMask = LayerMask.GetMask("PinLayer");

                // Check if the hit object is a child of a pin or the pin itself
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, pinLayerMask))
                {
                    Transform hitTransform = hit.transform;

                    if (hitTransform.CompareTag("Pin"))
                    {
                        CorkboardEvidence reference = hit.transform.gameObject.GetComponent<CorkboardEvidence>();
                        threadPoints.Add(reference.getName());

                        // Get the pin's position
                        Vector3 pinPosition = hitTransform.position;

                        if (!isConnecting)
                        {
                            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
                            currentLineRenderer = Instantiate(linePrefab).GetComponent<LineRenderer>();
                            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(currentScene));
                            lineRenderers.Add(currentLineRenderer);
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

    public List<string> getThreads()
    {
        return threadPoints;
    }

    public void clearThreads()
    {
        threadPoints = new List<string>();
        isConnecting = false;
        foreach(LineRenderer renderer in lineRenderers) {
            Destroy(renderer);
        }
    }
}
