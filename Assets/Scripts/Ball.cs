using UnityEngine;
using UnityEngine.Serialization;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    private int startSpeed = 10;
    public bool kickStart = true;
    private GameObject player;

    private float currentSpeed ;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        BallReset();
    }

    private void Update()
    {
        if(Manager.Instance.CurrentGameState != Manager.GameState.playing){return;}
        if (kickStart && Input.GetKey(KeyCode.Space)) { transform.SetParent(null); rb.velocity = Vector2.up * startSpeed;  kickStart = false; }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        OnContactPointChange?.Invoke( col.GetContact(0).point);

        if (col.gameObject.CompareTag("Player"))
        {
            float hitDistance = transform.position.x - col.transform.position.x;
            float normalizedHitDistance = hitDistance / col.collider.bounds.extents.x; 

            rb.velocity += new Vector2(0.001f, 0.001f);
            rb.velocity = new Vector2(Mathf.Abs(normalizedHitDistance) * (rb.velocity.x/Mathf.Abs(rb.velocity.x)),rb.velocity.y/Mathf.Abs(rb.velocity.y));
        }
        
        rb.velocity = rb.velocity.normalized * currentSpeed;
    }

    public void BallReset()
    {
        rb.velocity = Vector2.zero;
        currentSpeed = startSpeed;
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1f, 0);
        transform.SetParent(player.transform);
        kickStart = true;
    }

    public void PlayerReset()
    {
        player.transform.position = new Vector3(0, -4, 0);
    }
    
    public delegate void NewContactPoint(Vector3 lastContactPoint);
    public event NewContactPoint OnContactPointChange; 
}
