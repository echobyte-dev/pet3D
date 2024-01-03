using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic
{
  public class SaveTrigger : MonoBehaviour
  {
    private ISaveLoadService _saveLoadService;

    public BoxCollider Collider;

    [Inject]
    public void Construct(ISaveLoadService saveLoadService)
    {
      _saveLoadService = saveLoadService;
    }

    private void OnTriggerEnter(Collider other)
    {
      _saveLoadService.SaveProgress();
      gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
      if (!Collider)
        return;

      Gizmos.color = new Color32(30, 30, 200, 130);
      Gizmos.DrawCube(transform.position + Collider.center, Collider.size);
    }
  }
}