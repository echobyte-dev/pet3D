using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
  [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
  public class EnemyDeath : MonoBehaviour
  {
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private EnemyAnimator _animator;

    [SerializeField] private GameObject _deathFx;

    public event Action Happaned;

    private void Start()
    {
      _health.OnHealthChange += OnOnHealthChange;
    }

    private void OnDestroy()
    {
      _health.OnHealthChange -= OnOnHealthChange;
    }

    private void OnOnHealthChange()
    {
      if (_health.CurrentHealth <= 0)
        Die();
    }

    private void Die()
    {
      _health.OnHealthChange -= OnOnHealthChange;

      _animator.PlayDeath();
      SpawnDeathFx();

      StartCoroutine(DestroyTimer());

      Happaned?.Invoke();
    }

    private void SpawnDeathFx()
    {
      Instantiate(_deathFx, transform.position, Quaternion.identity);
    }

    private IEnumerator DestroyTimer()
    {
      yield return new WaitForSeconds(3);
      Destroy(gameObject);
    }
  }
}