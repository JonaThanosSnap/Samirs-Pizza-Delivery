using UnityEngine;
using Unity.Netcode;

public class CameraFollow : MonoBehaviour
{
    Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {   
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 8, 12));

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // TODO Look at mouse
        //Vector3 mousePos = new Vector3();
        //mousePos.x = Input.
        transform.LookAt(target);
    }
}