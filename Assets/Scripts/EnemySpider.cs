using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpider : MonoBehaviour
{
    private Transform target;
    private float targetDistance;
    private Vector3 targetDirection;

    [SerializeField]
    public float timeBetweenWebs = 0.25f;
    public float timeBetweenAttacks = 1f;
    private float webTimer = 0f;
    private float attackTimer = 0f;
    [SerializeField]
    private float throwWebDistance = 5f;

    [SerializeField]
    public Transform attackPoint;
    [SerializeField]
    public float attackRange = 1f;
    [SerializeField]
    public float attackForce = 2.0f;
    [SerializeField]
    public float attackDamage = 15.0f;
    [SerializeField]
    public LayerMask playerLayers;

    [SerializeField]
    private GameObject ropeObject;
    private GameObject[] ropeArray;
    private GameObject[] poisonBallArray;

    [SerializeField]
    public int maxWebStrings = 6;
    private int numWebStrings = 0;

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip spiderAttack;
    [SerializeField]
    private AudioClip spiderDeath;
    [SerializeField]
    private AudioClip throwWebSound;

    // Start is called before the first frame update
    void Start()
    {
        // Find player's position (Transform)
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        // Make Spider not collide with web, and web not collide with web
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("SpiderWebLayer"), LayerMask.NameToLayer("Enemy"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("SpiderWebLayer"), LayerMask.NameToLayer("SpiderWebLayer"));

        // Rope array of size maxWebStrings
        ropeArray = new GameObject[maxWebStrings];
        poisonBallArray = new GameObject[maxWebStrings];

        AudioSource audio = GetComponent<AudioSource>();
    }

    void ThrowWeb()
    {
        if (webTimer > 0)
        {
            return;
        }
        if(numWebStrings >= maxWebStrings)
        {
            return;
        }
        
        // Reset Attack Timer
        webTimer = timeBetweenWebs;

        if (audioSource)
        {
            audioSource.clip = throwWebSound;
            audioSource.Play();
        }

        // Spawn web string at spider
        GameObject webRope = Instantiate(ropeObject, new Vector3(transform.position.x, 
            transform.position.y, transform.position.z), Quaternion.identity);

        // Store web rope in an array
        ropeArray[numWebStrings] = webRope;
        ++numWebStrings;

        //Debug.Log("Created rope with children: " + webRope.transform.childCount);
        // Store poison ball in an array
        for(int i = 0; i < webRope.transform.childCount; ++i)
        {
            if(webRope.transform.GetChild(i).GetComponent<SpiderPoisonBall>())
            {
                poisonBallArray[numWebStrings] = webRope.transform.GetChild(i).gameObject;
                //Debug.Log("Found poisonball: " + poisonBallArray[numWebStrings]);
            }
        }

        // Get the Rope script object of the webRope GameObject
        // This allows us to access the public member variables/functions
        // lastSegment is the Poison Ball's RigidBody
        Rope webRopeRopeObject = webRope.GetComponent<Rope>();


        // Web should not collide with spider (This currently seems to do nothing)
        Physics2D.IgnoreCollision(webRopeRopeObject.prefabRopeSegment.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(webRopeRopeObject.poisonBall.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        // Attach one end to spider - set the hook's parent as the Spider
        //webRopeRopeObject.hook.transform.parent = gameObject.transform;

        // Set the hook's hingejoint's anchor to the Spider's position
        // FYI: This does not work! Do not do this! Doing this causes the webs to float around, half-heartedly trying to follow the spider
        //webRopeRopeObject.hook.GetComponent<HingeJoint2D>().anchor = gameObject.transform.position;

        // Set the hook's connected body as the Spider's rigidbody (This works great *chef's kiss*)
        //webRopeRopeObject.hook.GetComponent<HingeJoint2D>().connectedBody = gameObject.GetComponent<Rigidbody2D>();
        //webRopeRopeObject.hook.transform.position = gameObject.transform.position;
    }

    void Attack()
    {
        if(attackTimer > 0)
        {
            return;
        }

        if(gameObject.GetComponent<ParticleSystem>())
        {
            // Play attack animation
            gameObject.GetComponent<ParticleSystem>().Play();
            
        }

        

        // Detect enemies in range that are in the "Enemy" Layer
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

        // Apply damage to enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            // Apply damage to Player Controllers
            if (enemy.GetComponent<PlayerController>())
            {
                enemy.GetComponent<PlayerController>().TakeDamage(attackDamage);
                GetComponent<Animator>().Play("Spider_bite", -1, 0f);
                if (GetComponent<AudioSource>())
                {
                    GetComponent<AudioSource>().Play();
                }
            }
            // Apply force to Rigidbodies
            if (enemy.GetComponent<Rigidbody2D>())
            {
                Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
                enemyRb.AddForce(transform.up * attackForce, ForceMode2D.Impulse);
            }

        }
        attackTimer = timeBetweenAttacks;
    }

    public void OnDeath()
    {
        foreach (GameObject rope in ropeArray)
        {
            for(int i = 0; i < rope.transform.childCount; ++i)
            {
                if(rope.transform.GetChild(i).GetComponent<SpiderPoisonBall>())
                {
                    Destroy(rope.transform.GetChild(i).gameObject);
                    //Debug.Log("Trying to destroy " + rope.transform.GetChild(i).gameObject);
                }
            }
        }

        if (audioSource)
        {
            audioSource.clip = spiderDeath;
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        targetDistance = Vector3.Distance(target.transform.position, gameObject.transform.position);
        targetDirection = (target.transform.position - gameObject.transform.position).normalized;

        if (targetDistance < throwWebDistance)
        {
            ThrowWeb();
        }

        if(targetDistance < 3)
        {
            Attack();
        }

        if (webTimer > 0)
        {
            webTimer -= Time.deltaTime;  // Decrement webTimer
        }
        else
        {
            webTimer = 0;
        }

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;  // Decrement attackTimer
        }
        else
        {
            attackTimer = 0;
        }

        // Keep the webRope hooks attached to Spider
        foreach (GameObject rope in ropeArray)
        {
            if (rope != null)
            {
                rope.transform.position = transform.position;
                rope.GetComponent<Rope>().hook.GetComponent<HingeJoint2D>().connectedBody = gameObject.GetComponent<Rigidbody2D>();
                rope.GetComponent<Rope>().hook.transform.position = gameObject.transform.position;
            }
        }
    }
}
