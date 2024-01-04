using UnityEngine;

namespace CodeBase.Santa
{
  [RequireComponent(typeof(SantaHealth))]
  public class SantaDeath : MonoBehaviour
  {
    [SerializeField] private SantaHealth _health;
    [SerializeField] private SantaMove _move;
    [SerializeField] private SantaAttack _attack;
    [SerializeField] private SantaAnimator _animator;

    [SerializeField] private GameObject _deathFx;
    private bool _isDead;

    private void Start()
    {
      _health.OnHealthChange += HealthChanged;
    }

    private void OnDestroy()
    {
      _health.OnHealthChange -= HealthChanged;
    }

    private void HealthChanged()
    {
      if (!_isDead && _health.CurrentHealth <= 0)
        Die();
    }

    private void Die()
    {
      _isDead = true;
      _move.enabled = false;
      _attack.enabled = false;
      _animator.PlayDeath();

      Instantiate(_deathFx, transform.position, Quaternion.identity);
    }
  }
}