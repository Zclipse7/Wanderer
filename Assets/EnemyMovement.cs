using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.U2D;

public class EnemyMovement : MonoBehaviour
{
    
    
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator animator;
    private Transform currentPos;
    public float speed;
    public SpriteRenderer sprite;

    public HealthBarBehaviour healthBar;
    private int maxHealth=100;
    private int health = 100;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentPos= pointB.transform;
        sprite = GetComponent<SpriteRenderer>();
        healthBar.SetHealth(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            StartCoroutine(storeCollectedItem("http://localhost/Module2/collectData.php", "enemy", "50"));
        }
        else
        {
            Vector2 point = currentPos.position - transform.position;
            if (currentPos == pointB.transform)
            {
                rb.velocity = new Vector2(speed, 0);
                sprite.flipX = true;

            }
            else
            {
                rb.velocity = new Vector2(-speed, 0);
                sprite.flipX = false;

            }

            if (Vector2.Distance(transform.position, currentPos.position) < 0.5f && currentPos == pointB.transform)
            {
                currentPos = pointA.transform;
            }
            if (Vector2.Distance(transform.position, currentPos.position) < 0.5f && currentPos == pointA.transform)
            {
                currentPos = pointB.transform;
            }
        }
      
    }

    public void Test()
    {
        Debug.Log("TTTTTTTEEEEEEEEEEESSSSSSSSSSSSTTTTTTT");

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health, maxHealth);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    IEnumerator storeCollectedItem(string url, string item, string points)
    {
        WWWForm form = new WWWForm();
        form.AddField("item", item);
        form.AddField("points", points);


        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }



    }
}
