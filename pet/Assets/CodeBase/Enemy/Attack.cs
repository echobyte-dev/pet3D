using System.Linq;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using UnityEngine;
using Zenject;
namespace CodeBase.Enemy
{
  [RequireComponent(typeof(EnemyAnimator))]
  public class Attack : MonoBehaviour
  {
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private float _attackCooldown = 3f;
    [SerializeField] private float _cleavage = 0.5f;
    [SerializeField] private float _effectiveDistance = 0.5f;
    [SerializeField] private float _damage = 10f;

    private IGameFactory _gameFactory;
    private Transform _playerTransform;
    private float _checkAttackCooldown;
    private bool _isAttacking;

    private int _layerMask;

    private Collider[] _hits = new Collider[1];

    private bool _attackIsActive;

    [Inject]
    public void Construct(IGameFactory gameFactory)
    {
      _gameFactory = gameFactory;
      _gameFactory.SantaCreated += OnSantaCreated;

      _layerMask = 1 << LayerMask.NameToLayer("Santa");
    }

    private void Update()
    {
      UpdateCooldown();

      if (CanAttack())
        StartAttack();
    }

    private void OnAttack()
    {
      if (Hit(out Collider hit))
      {
        PhysicsDebug.DrawDebug(StartPoint(), _cleavage, 1);
        hit.transform.GetComponent<IHealth>().TakeDamage(_damage);
      }
    }

    private void OnAttackEnded()
    {
      _checkAttackCooldown = _attackCooldown;
      _isAttacking = false;
    }

    public void EnableAttack() => 
      _attackIsActive = true;

    public void DisableAttack() => 
      _attackIsActive = false;

    private bool Hit(out Collider hit)
    {
      int hitCount = Physics.OverlapSphereNonAlloc(StartPoint(), _cleavage, _hits, _layerMask);

      hit = _hits.FirstOrDefault();

      return hitCount > 0;
    }

    private void StartAttack()
    {
      transform.LookAt(_playerTransform);
      _animator.PlayAttack();

      _isAttacking = true;
    }

    private void UpdateCooldown()
    {
      if (!CooldownIsUp())
        _checkAttackCooldown -= Time.deltaTime;
    }

    private Vector3 StartPoint() =>
      new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
      transform.forward * _effectiveDistance;

    private bool CanAttack() =>
      _attackIsActive && CooldownIsUp() && !_isAttacking;

    private bool CooldownIsUp() =>
      _checkAttackCooldown <= 0;

    private void OnSantaCreated() =>
      _playerTransform = _gameFactory.SantaGameObject.transform;
  }
}