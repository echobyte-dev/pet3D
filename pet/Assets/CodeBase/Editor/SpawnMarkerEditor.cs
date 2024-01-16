using CodeBase.Logic.EnemySpawners;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
  [CustomEditor(typeof(SpawnMarker))]
  public class SpawnMarkerEditor : UnityEditor.Editor
  {
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(SpawnMarker spawner, GizmoType gizmo)
    {
      CircleGizmo(spawner.transform, 0.5f, Color.blue);
    }

    private static void CircleGizmo(Transform transform, float radius, Color color)
    {
      Gizmos.color = color;
      Vector3 pos = transform.position;
      
      Gizmos.DrawSphere(pos,radius);
    }
  }
}