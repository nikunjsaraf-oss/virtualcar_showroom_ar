using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARController : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;

    private ARRaycastManager _arRaycastManager;
    private GameObject _objectInstance;

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        _objectInstance = null;
    }

    private void Update()
    {
        if (Input.touchCount <= 0 || Input.GetTouch(0).phase != TouchPhase.Began || _objectInstance != null) return;
        var touches = new List<ARRaycastHit>();
        _arRaycastManager.Raycast(Input.GetTouch(0).position, touches,
            TrackableType.Planes);
        if (touches.Count > 0)
        {
            _objectInstance = Instantiate(objectPrefab, touches[0].pose.position, touches[0].pose.rotation);
        }
    }
}