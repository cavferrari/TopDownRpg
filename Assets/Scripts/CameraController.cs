using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float smoothing;
    public Vector2 minBoundary;
    public Vector2 maxBoundary;

    private Transform target;
    private Vector3 newPosition;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        newPosition = this.transform.position;
    }


    void LateUpdate()
    {
        newPosition.Set(target.position.x, target.position.y, this.transform.position.z);
        if (this.transform.position != newPosition)
        {
            newPosition.x = Mathf.Clamp(newPosition.x, minBoundary.x, maxBoundary.x);
            newPosition.y = Mathf.Clamp(newPosition.y, minBoundary.y, maxBoundary.y);
            this.transform.position = Vector3.Lerp(this.transform.position, newPosition, smoothing);
        }
    }
}
