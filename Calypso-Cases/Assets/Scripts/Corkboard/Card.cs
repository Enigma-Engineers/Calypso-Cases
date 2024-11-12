using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private bool isGrabbing = false;
    private Vector3 offset;
    private GameObject card;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryGrabObject();
        }

        if(isGrabbing)
        {
            MoveObject();
        }

        if (Input.GetMouseButtonUp(0))
        {
            ReleaseObject();
        }
    }

    void TryGrabObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // First, check if the ray hits the pin (by specifying the pin layer in LayerMask)
        LayerMask pinLayerMask = LayerMask.GetMask("PinLayer");
        LayerMask cardLayerMask = LayerMask.GetMask("CardLayer");

        // Check raycast for pin collider only
        if (!Physics.Raycast(ray, out hit, Mathf.Infinity, pinLayerMask) && Physics.Raycast(ray, out hit, Mathf.Infinity, cardLayerMask))
        {
            card = hit.transform.parent.gameObject;  // Assuming card itself is what you want to move
            isGrabbing = true;
            offset = card.transform.position - hit.point;
        }
    }

    void MoveObject()
    {
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast (ray, out hit))
        {
            Vector3 targetPosition = hit.point + offset;
            card.transform.position = targetPosition;
        }

        GameObject pin;
        pin = card.transform.GetChild(0).gameObject;
        pin.SetActive(false);

        GetComponent<Threads>().clearThreads();

    }

    void ReleaseObject()
    {
        GameObject pin;
        if(card != null)
        {
            pin = card.transform.GetChild(0).gameObject;
            pin.SetActive(true);
        }
        card = null;
        isGrabbing = false;
        
    }
}
