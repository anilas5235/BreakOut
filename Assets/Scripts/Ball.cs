using UnityEngine;
using UnityEngine.Serialization;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public float startSpeed = 10;
    public bool kickStart = true;
    [SerializeField] private GameObject player;

    public float speedMultiplier = 1;
    
    private void Start()
    {
        BallReset();
    }

    private void Update()
    {
        if(Manager.Instance.currentGameState != Manager.GameState.Playing){return;}
        if (kickStart && Input.GetKey(KeyCode.Space))
        { transform.SetParent(null); rb.velocity = Vector2.up * (startSpeed * speedMultiplier);  kickStart = false; UiManager.Instance.ToggleTips(false); }
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
        
        rb.velocity = rb.velocity.normalized * startSpeed * speedMultiplier;
    }

    public void BallReset()
    {
        rb.velocity = Vector2.zero;
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1f, 0);
        transform.SetParent(player.transform);
        kickStart = true;
    }

    public void PlayerReset()
    {
        player.transform.position = new Vector3(0, -4, 0);
    }
    
    public delegate void NewContactPoint(Vector3 lastContactPoint);
    public event NewContactPoint OnContactPointChange; }
