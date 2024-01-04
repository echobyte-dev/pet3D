using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Santa
{
  [RequireComponent(typeof(SantaAnimator), typeof(CharacterController))]
  public class SantaAttack : MonoBehaviour, ISavedProgressReader
  {
    [SerializeField] private SantaAnimator _playerAnimator;
    [SerializeField] private CharacterController _characterController;

    private static int _layerMask;
    private Collider[] _hits = new Collider[3];
    private Stats _stats;
    private IInputService _inputService;

    private void Awake()
    {
      _inputService = BootstrapState.InputService;
      _layerMask = 1 << LayerMask.NameToLayer("Hittable");
    }

    private void Update()
    {
      if (_inputService.IsAttackButtonUp() && !_playerAnimator.IsAttacking())
        _playerAnimator.PlayAttack();
    }

    public void OnAttack()
    {
      for (int i = 0; i < Hit(); i++)
      {
        _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.Damage);
      }
    }

    public void LoadProgress(PlayerProgress progress) => 
      _stats = progress.SantaStats;

    private int Hit() => 
      Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _stats.DamageRadius, _hits, _layerMask);

    private Vector3 StartPoint() =>
      new Vector3(transform.position.x, _characterController.center.y / 2, transform.position.z);
  }
}