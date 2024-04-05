using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class MultiImageTracked : MonoBehaviour
{
    private ARTrackedImageManager ARTrackedImageManager;

    private Dictionary<string, GameObject> instanciatePrefab;

    [SerializeField]
    private Text prefabsCountText;

    private void Awake()
    {
        ARTrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        ARTrackedImageManager.trackedImagesChanged += OnTrackedImage;
    }

    private void OnDisable()
    {
        ARTrackedImageManager.trackedImagesChanged -= OnTrackedImage;
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
        GameObject prefab = Instantiate(Resources.Load<GameObject>(addedImage.referenceImage.name));
        prefab.transform.position = addedImage.transform.position;
        prefab.transform.rotation = addedImage.transform.rotation;
        prefab.transform.parent = addedImage.transform;

        instanciatePrefab.Add(addedImage.referenceImage.name, prefab);
        prefabsCountText.text = instanciatePrefab.Count.ToString();
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
            prefabsCountText.text = instanciatePrefab.Count.ToString();
        }
    }
}
