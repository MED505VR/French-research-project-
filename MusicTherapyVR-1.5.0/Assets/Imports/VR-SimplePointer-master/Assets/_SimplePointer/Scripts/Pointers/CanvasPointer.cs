using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasPointer : MonoBehaviour
{
    public float defeaultlength = 3.0f;

    public EventSystem eventSystem = null;
    public StandaloneInputModule inputModule = null;

    private LineRenderer lineRenderer = null;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        UpdateLength();
    }

    private void UpdateLength()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, GetEnd());
    }

    private Vector3 GetEnd()
    {
        var distance = GetCanvasDistance();
        var endPosition = CalculateEnd(defeaultlength);

        if (distance != 0.0f)
            endPosition = CalculateEnd(distance);

        return endPosition;
    }

    private float GetCanvasDistance()
    {
        // Get Data
        var eventData = new PointerEventData(eventSystem);
        eventData.position = inputModule.inputOverride.mousePosition;

        // Raycast using data
        var results = new List<RaycastResult>();
        eventSystem.RaycastAll(eventData, results);

        // Get closest
        var closestResult = FindFirstRaycast(results);
        var distance = closestResult.distance;

        // Clamp
        distance = Mathf.Clamp(distance, 0.0f, defeaultlength);
        return distance;
    }

    private RaycastResult FindFirstRaycast(List<RaycastResult> results)
    {
        foreach (var result in results)
        {
            if (!result.gameObject)
                continue;

            return result;
        }

        return new RaycastResult();
    }

    private Vector3 CalculateEnd(float length)
    {
        return transform.position + transform.forward * length;
    }
}