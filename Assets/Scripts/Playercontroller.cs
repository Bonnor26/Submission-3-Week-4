using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 8f;
    public float margin = 0.5f;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // -1 (left/A) to +1 (right/D)
        float input = Input.GetAxis("Horizontal");

        // Only X changes
        float newX = transform.position.x + input * speed * Time.deltaTime;

        // Clamp to the camera's visible width
        float halfWidth = cam.orthographicSize * cam.aspect;
        float camX = cam.transform.position.x;
        newX = Mathf.Clamp(newX, camX - halfWidth + margin, camX + halfWidth - margin);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}