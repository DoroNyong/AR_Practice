using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MultiImageTracked : MonoBehaviour
{
    private ARTrackedImageManager arTrackedImageManager;

    private Dictionary<string, GameObject> instanciatePrefab;

    [SerializeField]
    private GameObject Cube;
    [SerializeField]
    private Color colorE0000;
    [SerializeField]
    private Color colorE0001;

    private void Awake()
    {
        arTrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        arTrackedImageManager.trackedImagesChanged += OnTrackedImage;
    }

    private void OnDisable()
    {
        arTrackedImageManager.trackedImagesChanged -= OnTrackedImage;
    }

    void OnTrackedImage(ARTrackedImagesChangedEventArgs args)
    {
        foreach (ARTrackedImage trackedImage in args.added)
        {
            InstantiateGameObject(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in args.updated)
        {
            UpdateTrackingGameObject(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in args.removed)
        {
            DestroyGameObject(trackedImage);
        }
    }

    private void InstantiateGameObject(ARTrackedImage addedImage)
    {
        GameObject prefab = Instantiate(Cube);
        SkinnedMeshRenderer prefabCubeSkinnedMeshRenderer = prefab.transform.GetComponent<SkinnedMeshRenderer>();
        prefabCubeSkinnedMeshRenderer.material = Instantiate(prefabCubeSkinnedMeshRenderer.material);
        switch (addedImage.referenceImage.name)
        {
            case "E0000":
                prefabCubeSkinnedMeshRenderer.material.color = colorE0000;
                break;
            case "E0001":
                prefabCubeSkinnedMeshRenderer.material.color = colorE0001;
                break;
            default:
                throw new Exception("대체 뭘 식별한거죠?");
        }
        prefab.transform.position = addedImage.transform.position;
        prefab.transform.rotation = addedImage.transform.rotation;
        prefab.transform.parent = addedImage.transform;

        instanciatePrefab.Add(addedImage.referenceImage.name, prefab);
    }

    private void UpdateTrackingGameObject(ARTrackedImage updatedImage)
    {
        if (instanciatePrefab.TryGetValue(updatedImage.referenceImage.name, out GameObject prefab))
        {
            prefab.transform.position = updatedImage.transform.position;
            prefab.transform.rotation = updatedImage.transform.rotation;
            prefab.SetActive(true);
        }
    }

    private void DestroyGameObject(ARTrackedImage removedImage)
    {
        if (instanciatePrefab.TryGetValue(removedImage.referenceImage.name, out GameObject prefab))
        {
            instanciatePrefab.Remove(removedImage.referenceImage.name);
            Destroy(prefab);
        }
    }
}
