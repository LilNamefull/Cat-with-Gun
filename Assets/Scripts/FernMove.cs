using UnityEngine;

public class FernMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float moveHeight = 3f;
    private Vector3 startPosition;
    private bool initialized = false; 

    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        startPosition = transform.position;
        initialized = true;
    }

    void Update()
    {
        // If not initialized, initialize first
        if (!initialized) Initialize();

        if (transform.position.x > 4.1f) 
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        else
        {
            // Use PingPong to create a smooth up-and-down movement
            float verticalMove = Mathf.PingPong(Time.timeSinceLevelLoad * moveSpeed, moveHeight) - (moveHeight / 2f);

            // Check for NaN before applying the position change
            if (!float.IsNaN(verticalMove))
            {
                transform.position = new Vector3(4f, startPosition.y + verticalMove, transform.position.z);
            }
        }
    }
}
