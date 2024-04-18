using UnityEngine;

public class ElementalMovement : MonoBehaviour
{
    public float speed = 3.0f;
    private bool isMoving = true;

    void Update()
    {
        if (isMoving)
        {
            // Movement logic here
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    public void StartMoving()
    {
        isMoving = true;
    }
}

