using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GroundPositioningHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabToSpawn; // Prefab to place on the ground.

    private ARTrackedImageManager trackedImageManager;
    private ARPlaneManager planeManager;
    private Camera arCamera;

    // public reference to the TextMeshPro component
    public TextMeshPro textMeshPro;

    private GameObject spawnedObject;
    public string[] ImageDescribtion;
    public Vector3[] offsetCharacterImage;

 
    void Awake()
    {
        // Get references to required components
        trackedImageManager = GetComponent<ARTrackedImageManager>();
        planeManager = GetComponent<ARPlaneManager>();
        arCamera = Camera.main; // AR Camera

    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {

        foreach (var trackedImage in eventArgs.added)
        {
            SpawnObjectOnGround(trackedImage);
        }
        foreach (var trackedImage in eventArgs.updated)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
                UpdateObjectPosition(trackedImage);
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            if (spawnedObject != null)
                Destroy(spawnedObject);
        }
    }

    private void SpawnObjectOnGround(ARTrackedImage trackedImage)
    {
        if (spawnedObject == null)
        {
            spawnedObject = Instantiate(prefabToSpawn);
            spawnedObject.SetActive(true);
            GameObject TextObject = spawnedObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
            textMeshPro = TextObject.GetComponent<TextMeshPro>();
        }

        UpdateObjectPosition(trackedImage);
    }

    private void UpdateObjectPosition(ARTrackedImage trackedImage)
    {
        if (spawnedObject != null)
        {
            char lastChar = trackedImage.referenceImage.name[trackedImage.referenceImage.name.Length - 1];
            int IndexText = int.Parse(lastChar.ToString());
            textMeshPro.text = ImageDescribtion[IndexText-1];

            // Get the position from the AR camera, projected to the ground
            Vector3 cameraPosition = arCamera.transform.position;

            // Find the closest detected plane
            ARPlane closestPlane = FindClosestPlane(cameraPosition);

            if (closestPlane != null)
            {
                Vector3 groundPosition = new Vector3(trackedImage.transform.position.x, closestPlane.transform.position.y, trackedImage.transform.position.z);
                spawnedObject.transform.position = groundPosition + offsetCharacterImage[IndexText - 1];
                spawnedObject.transform.rotation = Quaternion.identity; // Optional: Reset rotation to match ground plane.
            }

        }
    }

    private ARPlane FindClosestPlane(Vector3 position)
    {
        ARPlane closestPlane = null;
        float closestDistance = float.MaxValue;

        foreach (var plane in planeManager.trackables)
        {
            float distance = Vector3.Distance(position, plane.transform.position);
            if (distance < closestDistance && plane.gameObject.activeSelf)
            {
                closestDistance = distance;
                closestPlane = plane;
            }
        }

        return closestPlane;
    }
}
