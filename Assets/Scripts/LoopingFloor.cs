using UnityEngine;

public class LoopingFloor : MonoBehaviour
{
    // Speed at which the floor scrolls
    public float scrollSpeed = 1f;

    // Reference to the character's transform
    public Transform character;

    // Width of a single floor tile
    private float tileWidth;

    // Start position of the floor
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Calculate the width of a single floor tile
        tileWidth = GetComponentInChildren<SpriteRenderer>().bounds.size.x;

        // Store the start position of the floor
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the offset based on character's movement
        float offset = character.position.x * scrollSpeed;

        // Calculate the position where the tile should loop
        float newPosition = (offset / tileWidth) * tileWidth;

        // Calculate the difference in position
        float difference = newPosition - transform.position.x;

        // Move the floor horizontally
        transform.position += new Vector3(difference, 0f, 0f);

        // If the floor has moved more than its width, reset its position
        if (Mathf.Abs(transform.position.x - startPosition.x) >= tileWidth)
        {
            // Calculate the amount to move the floor back
            float resetAmount = Mathf.Floor((transform.position.x - startPosition.x) / tileWidth) * tileWidth;

            // Move the floor back to its original position
            transform.position = new Vector3(startPosition.x + resetAmount, transform.position.y, transform.position.z);
        }
    }
}