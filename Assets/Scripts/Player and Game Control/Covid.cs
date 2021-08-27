using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class Covid : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer _spriteRenderer;
    public float speed = 50.0f;
    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxLifetime = 30.0f;
    public float maxSize = 1.5f;
    private Rigidbody2D _rigidbody;

    PhotonView view;

    public TextMeshProUGUI scoreText;
    public int scoreValue;
    public AudioSource splat;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        view = GetComponent<PhotonView>();

        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size;

        _rigidbody.mass = this.size;
    }

    public void setTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        splat.Play();
        if (collision.gameObject.tag == "Vax") //the bullet tag is Vax

        {
            if (this.size * 0.5f >= this.minSize)
            {
                CreateSplit();
                CreateSplit();
            } 
            BulletScript bullet = collision.gameObject.GetComponent<BulletScript>();
            FindObjectOfType<GameManager>().CovidDestroyed(this, bullet.player);
            Destroy(this.gameObject);
          
        }
        if (collision.gameObject.tag == "Player")
        {
            PlayerScript player = collision.gameObject.GetComponent<PlayerScript>();
            FindObjectOfType<GameManager>().CovidDestroyed(this, player);
            Destroy(this.gameObject);
        }
    }

    private void CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Covid half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;
        half.setTrajectory(Random.insideUnitCircle.normalized);
    }
}

