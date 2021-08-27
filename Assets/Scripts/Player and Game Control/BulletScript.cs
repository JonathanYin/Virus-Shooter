using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public float maxLifetime = 10.0f;
    public float speed = 500.0f;
    PhotonView view;
    public PlayerScript player;

    private void Start() {
         view = GetComponent<PhotonView>();
    }

    public void addPlayer(PlayerScript p){
        player = p;
    }

    private void Awake() {
       
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction) {
        _rigidbody.AddForce(direction * this.speed);
        
        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(this.gameObject);
    }
}
