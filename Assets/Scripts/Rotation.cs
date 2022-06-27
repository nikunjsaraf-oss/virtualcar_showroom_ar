using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField, Range(1, 100)] private float speed = 10f;

    private Transform _objectTransform;


    private void Start()
    {
        _objectTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (Input.touchCount != 3) return;
        var screenTouch = Input.GetTouch(0);

        if (screenTouch.phase != TouchPhase.Moved) return;
        _objectTransform.Rotate(0, -screenTouch.deltaPosition.x * Time.deltaTime * speed, 0);
    }
}