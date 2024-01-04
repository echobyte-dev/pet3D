using System.Linq;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
  [RequireComponent(typeof(EnemyAnimator))]
  public class Attack : MonoBehaviour
  {
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private float _attackCooldown = 3f;

    private Transform _playerTransform;
    private float _checkAttackCooldown;
    private bool _isAttacking;
    private int _layerMask;
    private Collider[] _hits = new Collider[1];
    private bool _attackIsActive;
    
    public float Cleavage = 0.5f;
    public float EffectiveDistance = 0.5f;
    public float Damage = 10f;
    
    public void Construct(Transform playerTransform) => 
      _playerTransform = playerTransform;

    private void Awake() => 
      _layerMask = 1 << LayerMask.NameToLayer("Santa");

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
        PhysicsDebug.DrawDebug(StartPoint(), Cleavage, 1);
        hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
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
      int hitCount = Physics.OverlapSphereNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);

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
      transform.forward * EffectiveDistance;

    private bool CanAttack() =>
      _attackIsActive && CooldownIsUp() && !_isAttacking;

    private bool CooldownIsUp() =>
      _checkAttackCooldown <= 0;
  }
}