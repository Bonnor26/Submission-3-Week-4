# Week 4 Lab - Orbiting Enemies (Unity 6)

## Group Members
- Connor Hewitt

## Description
2D top-down space game. The player ship moves only along X and is clamped
to the camera bounds. Enemies orbit the player, face the player, and change
speed based on their distance to the player.

## Controls
- A / D or Left / Right arrows: move the ship

## Implementation Notes
- PlayerController: X-only movement, Mathf.Clamp to camera bounds
- EnemyController:
  - Orbit: Vector3.Cross tangent plus a radial correction toward a per-enemy radius
  - Facing: Mathf.Atan2, Rad2Deg, Quaternion.Euler, Quaternion.Slerp (no LookAt / LookRotation)
  - Speed: sqrMagnitude compared to range squared, then Mathf.Lerp
  - Collision avoidance (stretch): separation push from nearby enemies
- EnemySpawner: spawns enemies at random positions around the player

## Unity Version
Unity 6 (6000.0.71f1)

