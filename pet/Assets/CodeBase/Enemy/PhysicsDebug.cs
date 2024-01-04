using UnityEngine;

namespace CodeBase.Enemy
{
  public static class PhysicsDebug
  {
    public static void DrawDebug(Vector3 worldPos, float radius, float seconds)
    {
      Debug.DrawRay(worldPos, radius * Vector3.up, Color.blue, seconds);
      Debug.DrawRay(worldPos, radius * Vector3.down, Color.blue, seconds);
      Debug.DrawRay(worldPos, radius * Vector3.left, Color.blue, seconds);
      Debug.DrawRay(worldPos, radius * Vector3.right, Color.blue, seconds);
      Debug.DrawRay(worldPos, radius * Vector3.forward, Color.blue, seconds);
      Debug.DrawRay(worldPos, radius * Vector3.back, Color.blue, seconds);
    }
  }
}