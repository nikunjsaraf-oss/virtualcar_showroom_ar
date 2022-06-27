using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Listens for touch events and performs an AR raycast from the screen touch point.
/// AR raycasts will only hit detected trackables like feature points and planes.
///
/// If a raycast hits a trackable, the <see cref="PlacedPrefab"/> is instantiated
/// and moved to the hit position.
/// </summary>
[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField] [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    private GameObject mPlacedPrefab;

    private UnityEvent _placementUpdate;

    [SerializeField] private GameObject visualObject;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject PlacedPrefab
    {
        get => mPlacedPrefab;
        set => mPlacedPrefab = value;
    }

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    private GameObject SpawnedObject { get; set; }

    private void Awake()
    {
        _mRaycastManager = GetComponent<ARRaycastManager>();

        _placementUpdate ??= new UnityEvent();

        _placementUpdate.AddListener(DisableVisual);
    }

    private static bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    private void Update()
    {
        if (!TryGetTouchPosition(out var touchPosition))
            return;

        if (!_mRaycastManager.Raycast(touchPosition, SHits, TrackableType.PlaneWithinPolygon)) return;
        // Raycast hits are sorted by distance, so the first one
        // will be the closest hit.
        var hitPose = SHits[0].pose;

        if (SpawnedObject == null)
        {
            SpawnedObject = Instantiate(mPlacedPrefab, hitPose.position, hitPose.rotation);
        }

        _placementUpdate.Invoke();
    }

    private void DisableVisual()
    {
        visualObject.SetActive(false);
    }

    private static readonly List<ARRaycastHit> SHits = new();

    private ARRaycastManager _mRaycastManager;
}