using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneClassifier : MonoBehaviour
{
    public ARPlaneManager arPlaneManager;

    private void OnEnable()
    {
        if (arPlaneManager == null)
        {
            Debug.LogError("ARPlaneManager not assigned!");
            return;
        }

        // Subscribe to the planesChanged event
        arPlaneManager.planesChanged += OnPlanesChanged;
    }

    private void OnDisable()
    {
        // Unsubscribe from the planesChanged event
        if (arPlaneManager != null)
        {
            arPlaneManager.planesChanged -= OnPlanesChanged;
        }
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        // Process newly added planes
        foreach (var plane in args.added)
        {
            ClassifyPlane(plane);
        }
    }

    private void ClassifyPlane(ARPlane plane)
    {
        if (plane == null)
            return;

        // Check the plane's alignment and take action
        switch (plane.alignment)
        {
            case PlaneAlignment.HorizontalUp:
                if (plane.center.y > 1.2f)
                    plane.gameObject.SetActive(false);
                break;

            case PlaneAlignment.HorizontalDown:
                plane.gameObject.SetActive(false);
                break;

            case PlaneAlignment.Vertical:
                plane.gameObject.SetActive(false);
                break;

            case PlaneAlignment.None:
            default:
                break;
        }
    }
}
