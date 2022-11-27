using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float horizontalOffset = 3f;
    public float verticalOffset = 3f;
    private void LateUpdate()
    {
        float followPosx = target.position.x + horizontalOffset;
        float followPosy = target.position.y + verticalOffset;
        transform.position = new Vector3(followPosx, followPosy, transform.position.z);
    }
}
