using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFollower : MonoBehaviour
{
    public Camera CameraToFollow;
    public float DistanceFromCamera;

    void Update()
    {
        transform.position = CameraToFollow.transform.position + CameraToFollow.transform.forward * DistanceFromCamera;
    }
}
