using System;
using CodeBase.Data;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Santa
{
  public class SantaHealth : MonoBehaviour, ISavedProgress, IHealth
  {
    [SerializeField] private SantaAnimator _animator;
    private State _progressState;
    
    public event Action OnHealthChange;

    public float MaxHealth 
    {
      get => _progressState.MaxHP;
      set => _progressState.MaxHP = value;
    }


    public float CurrentHealth
    {
      get => _progressState.CurrentHP;
      set
      {
        if (_progressState.CurrentHP != value)
        {
          _progressState.CurrentHP = value;
          OnHealthChange?.Invoke();
        }
      }
    }

    public void LoadProgress(PlayerProgress progress)
    {
      _progressState = progress.SantaState;
      OnHealthChange?.Invoke();
    }

    public void UpdateProgress(PlayerProgress progress)
    {
      progress.SantaState.CurrentHP = CurrentHealth;
      progress.SantaState.MaxHP = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
      if (CurrentHealth <= 0) return;
      CurrentHealth -= damage;
      _animator.PlayHit();
    }
  }
}