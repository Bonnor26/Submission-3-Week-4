using UnityEngine;

// Moves ONLY along X and clamps to the camera's visible area.
public class PlayerController : MonoBehaviour
{
    public float speed = 8f;
    public float margin = 0.5f;   // roughly half the sprite width so it doesn't clip the edge

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // -1 (left/A) to +1 (right/D)
        float input = Input.GetAxis("Horizontal");

        // New X position: only X changes, Y and Z stay the same
        float newX = transform.position.x + input * speed * Time.deltaTime;

        // Camera bounds (orthographic camera): half the visible width
        float halfWidth = cam.orthographicSize * cam.aspect;
        float camX = cam.transform.position.x;

        newX = Mathf.Clamp(newX, camX - halfWidth + margin, camX + halfWidth - margin);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
