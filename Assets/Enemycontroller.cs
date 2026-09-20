using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Speed (changes with distance)")]
    public float nearSpeed = 1.5f;
    public float farSpeed = 4f;
    public float maxRange = 10f;

    [Header("Orbit")]
    public float minOrbitRadius = 2f;
    public float maxOrbitRadius = 6f;
    public float pullStrength = 1.5f;

    [Header("Facing")]
    public float turnSpeed = 8f;
    public float spriteAngleOffset = -90f;

    [Header("Collision avoidance")]
    public bool avoidOthers = true;
    public float avoidRadius = 1.5f;
    public float avoidStrength = 3f;

    float orbitDirection;
    float orbitRadius;

    static List<EnemyController> allEnemies = new List<EnemyController>();

    void OnEnable() { allEnemies.Add(this); }
    void OnDisable() { allEnemies.Remove(this); }

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        orbitDirection = Random.value < 0.5f ? 1f : -1f;
        orbitRadius = Random.Range(minOrbitRadius, maxOrbitRadius);
    }

    void Update()
    {
        Vector3 toPlayer = player.position - transform.position;
        toPlayer.z = 0f;

        float sqrDist = toPlayer.sqrMagnitude;
        if (sqrDist < 0.0001f) return;

        Vector3 dir = toPlayer.normalized;

        // Speed from distance (sqrMagnitude, no square root)
        float t = Mathf.Clamp01(sqrDist / (maxRange * maxRange));
        float speed = Mathf.Lerp(nearSpeed, farSpeed, t);

        // Orbit: sideways direction plus a pull toward this enemy's radius
        Vector3 tangent = Vector3.Cross(dir, Vector3.forward) * orbitDirection;
        float radialError = Mathf.Clamp(toPlayer.magnitude - orbitRadius, -1f, 1f);
        Vector3 velocity = (tangent + dir * radialError * pullStrength).normalized * speed;

        // Collision avoidance: push away from nearby enemies
        if (avoidOthers)
        {
            Vector3 push = Vector3.zero;
            foreach (EnemyController other in allEnemies)
            {
                if (other == this) continue;

                Vector3 away = transform.position - other.transform.position;
                away.z = 0f;
                float sq = away.sqrMagnitude;

                if (sq < avoidRadius * avoidRadius && sq > 0.0001f)
                {
                    float weight = 1f - Mathf.Sqrt(sq) / avoidRadius;
                    push += away.normalized * weight;
                }
            }
            velocity += push * avoidStrength;
        }

        transform.position += velocity * Time.deltaTime;

        // Face the player without LookAt or LookRotation
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + spriteAngleOffset;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}