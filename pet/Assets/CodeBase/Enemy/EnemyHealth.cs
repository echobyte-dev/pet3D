using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
  [RequireComponent(typeof(EnemyAnimator))]
  public class EnemyHealth : MonoBehaviour, IHealth
  {
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth;

    public event Action OnHealthChange;

    public float CurrentHealth
    {
      get => _currentHealth;
      set => _currentHealth = value;
    }

    public float MaxHealth
    {
      get => _maxHealth;
      set => _maxHealth = value;
    }

    public void TakeDamage(float damage)
    {
      _currentHealth -= damage;
      _animator.PlayHit();
      OnHealthChange?.Invoke();
    }
  }
}