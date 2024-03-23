using System.Collections;
using UnityEngine;

public class fallingplat : MonoBehaviour
{
    [SerializeField] private float fallDelay = .75f;
    [SerializeField] private float destroyDelay = 2f;
    [SerializeField] private float returnDelay = 3f; // Delay before returning to original position
    [SerializeField] private float fallSpeed = 10f; // Variable for fall speed

    private bool falling = false;
    private Vector3 originalPosition;
    private Rigidbody2D rb;

    private void Start()
    {
        originalPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Avoid calling the coroutine multiple times if it's already been called (falling)
        if (falling)
            return;

        // If the player landed on the platform, start falling
        if (collision.transform.CompareTag("Player"))
        {
            StartCoroutine(StartFall());
        }
    }

    private IEnumerator StartFall()
    {
        falling = true;

        // Wait for a few seconds before dropping
        yield return new WaitForSeconds(fallDelay);

        // Enable rigidbody and set fall speed
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = new Vector2(0, -fallSpeed); // Set fall speed

        // Destroy after a few seconds
        yield return new WaitForSeconds(destroyDelay);

        // Return to original position after a delay
        yield return new WaitForSeconds(returnDelay);
        ReturnToOriginalPosition();
    }

    private void ReturnToOriginalPosition()
    {
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
        transform.position = originalPosition;
        falling = false;
    }
}
