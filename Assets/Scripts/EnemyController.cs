using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10.0f;
    [SerializeField]
    private float health = 100f;
    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    public float sightDistance = 8.0f;

    private float rotateSpeed = 720.0f;

    private Rigidbody2D rb;

    private Transform target;

    [SerializeField]
    private Color colorLowHealth = Color.white;
    [SerializeField]
    private Color colorMedHealth = Color.white;
    [SerializeField]
    private Color colorHighHealth = Color.white;

    public bool isAlive = true;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        // Find player's position (Transform)
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        //Debug.Log(gameObject.name + " health = " + health);
        if(health <= 0 && isAlive)
        {
            if(GetComponent<EnemySpider>())
            {
                GetComponent<EnemySpider>().OnDeath();
                GetComponent<EnemySpider>().enabled = false;
            }
            if(GetComponent<Animator>())
            {
                //GetComponent<Animator>().Stop();
                GetComponent<Animator>().Play("OnDeath", -1, 0f);
            }
            if(GetComponent<Collider2D>())
            {
                GetComponent<Collider2D>().enabled = false;
            }
            if (GetComponent<Rigidbody2D>())
            {
                GetComponent<Rigidbody2D>().Sleep();
            }
            if (GetComponent<SpriteRenderer>())
            {
                GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
                GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
            isAlive = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive)
        {
            return;
        }

        // Update color of Enemy based on remaining health
        if (health <= maxHealth * 0.75)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = colorHighHealth;
        }
        else
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
        if (health <= maxHealth * 0.50)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = colorMedHealth;
        }
        if (health <= maxHealth * 0.25)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = colorLowHealth;
        }
    }

    private void FixedUpdate()
    {
        if (!isAlive)
        {
            return;
        }

        // Find and move towards Target
        Vector2 moveVector = (target.position - transform.position);
        if(moveVector.magnitude <= sightDistance)
        {
            moveVector = moveVector.normalized;
            //rb.AddForce(moveVector.normalized * moveSpeed * Time.deltaTime);
            rb.velocity = new Vector2(moveVector.x * moveSpeed, moveVector.y * moveSpeed);
        }
        // faces direction of movement (by rotating sprite renderer child
        if(GetComponentInChildren<SpriteRenderer>())
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, moveVector);
            GetComponentInChildren<Transform>().transform.rotation = 
                Quaternion.RotateTowards(GetComponentInChildren<Transform>().transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }
    }
}
