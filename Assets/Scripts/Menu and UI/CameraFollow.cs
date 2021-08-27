using UnityEngine;
using Photon.Pun;
using System.Collections;
public class CameraFollow : MonoBehaviour
{

    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    // void FixedUpdate()
    // {
    //     Vector3 desiredPosition = target.position + offset;
    //     Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    //     transform.position = smoothedPosition;

    //     transform.LookAt(target);
    // }

    void Start()
    {
        
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (PhotonView.Get(player).IsMine)
            {
                this.target = player.transform;
                break;
            }
        }
        
    }
    private void LateUpdate()
    {
        if(target != null) {
            transform.position = target.position + offset;
        }
        
    }

}