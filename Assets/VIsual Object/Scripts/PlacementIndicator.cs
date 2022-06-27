using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using static UnityEngine.Screen;

namespace Visual_Object.Scripts
{
    public class PlacementIndicator : MonoBehaviour
    {

        private ARRaycastManager _rayManager;
        [SerializeField]
        private GameObject visual;

        private void Start()
        {
            // get the components
            _rayManager = FindObjectOfType<ARRaycastManager>();
            visual = transform.GetChild(0).gameObject;

            // hide the placement indicator visual
            visual.SetActive(false);
        }

        private void Update()
        {
            // shoot a raycast from the center of the screen
            var hits = new List<ARRaycastHit>();
            _rayManager.Raycast(new Vector2(width / 2, height / 2), hits, TrackableType.Planes);

            // if we hit an AR plane surface, update the position and rotation
            if (hits.Count <= 0) return;
            var transform1 = transform;
            transform1.position = hits[0].pose.position;
            transform1.rotation = hits[0].pose.rotation;

            // enable the visual if it's disabled
            if (!visual.activeInHierarchy)
                visual.SetActive(true);
        }
    }
}