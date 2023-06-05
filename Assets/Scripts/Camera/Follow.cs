using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject player;
    Vector3 offset = new Vector3(0, 0, -10);
    float smoothSpeed = 0.125f;
    float minX = -11.42f;
    float maxX = 8.18f;
    float minY = -3.21f;
    float maxY = 7.59f; // Límites del mapa
    

    void LateUpdate()
    {
        Vector3 desiredPosition = player.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        smoothedPosition = new Vector3(
            Mathf.Clamp(smoothedPosition.x, minX, maxX),
            Mathf.Clamp(smoothedPosition.y, minY, maxY),
            smoothedPosition.z
        );
        transform.position = smoothedPosition;
    }
}
