using UnityEngine;
using Photon.Pun;

public class PlayerScript : MonoBehaviour
{
    public GameManager manager;
    public Sprite[] sprites;
    private SpriteRenderer _spriteRenderer;
    public static int maxHealth = 10;
    public int currentHealth = 10;
    public HealthBar healthBar;
    public float rotateSpeed = 5f;
    public GameObject bulletPrefab;
    public float thrustSpeed = 1.0f;
    public float turnSpeed = 1.0f;
    private Rigidbody2D _rigidbody;
    private bool _thrusting;
    private float _turnDirection;
    private bool _touched = false;

    PhotonView view;

    //this function is called in the spawner to change the color
    public void Color(int players, GameManager m) {
        Debug.Log("COLOR CHANGE CALLED");
        _spriteRenderer.sprite = sprites[players % 4];
        manager = m;
    }

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    private void Update()
    {
        if (view.IsMine) //this is used for photon so that you only control one player
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                var mouse = Input.mousePosition;
                var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
                var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
                var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0, 0, angle - 90);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
                if (touch.phase == TouchPhase.Stationary)
                {
                    _thrusting = true;
                }
                else if (!_touched)
                {
                    _touched = true;
                    _thrusting = false;
                    Shoot();
                }
            }
            else
            {
                _thrusting = false;
                _touched = false;
            }
        }

    }
    private void FixedUpdate()
    {
        if (_thrusting)
        {
            _rigidbody.AddForce(this.transform.up * this.thrustSpeed);
        }
    }

    private void Shoot()
    {
        GameObject bulletobj = PhotonNetwork.Instantiate(this.bulletPrefab.name, this.transform.position, this.transform.rotation);
        BulletScript bullet = bulletobj.GetComponent<BulletScript>();
        bullet.addPlayer(this);
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = 0.0f;
            currentHealth -= 2;
            healthBar.SetHealth(currentHealth);
            if (currentHealth <= 0)
            {
                this.gameObject.SetActive(false);
                manager.PlayerDied();
            }
        }
    }
}
