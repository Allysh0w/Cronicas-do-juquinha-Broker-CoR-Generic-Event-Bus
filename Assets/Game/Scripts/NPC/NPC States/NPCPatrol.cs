using System.Collections;
using UnityEngine;

public class NPCPatrol : MonoBehaviour
{

    public Vector2[] patrolPoints;
    public float speed = 2;
    public float pauseDuration = 1.5f;

    private Rigidbody2D  rb;
    private bool isPaused;
    private int currentPatrolIndex;
    private Vector2 target;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
        //target = patrolPoints[currentPatrolIndex];
        StartCoroutine(SetPatrolPoint());
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        Vector2 direction = ((Vector3)target - transform.position).normalized;
        FlipNPC(direction);
        rb.linearVelocity = direction * speed;
        if(Vector2.Distance(transform.position, target) < 0.1f)
            StartCoroutine(SetPatrolPoint());
    }

    private void FlipNPC(Vector2 direction)
    {
        if(direction.x < 0 && transform.localScale.x > 0 || direction.x > 0 && transform.localScale.x < 0 )
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.y);
        }
    }

    IEnumerator SetPatrolPoint()
    {
        isPaused = true;
        yield return new WaitForSeconds(pauseDuration);

        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        target = patrolPoints[currentPatrolIndex];
        isPaused = false;
    }
}
