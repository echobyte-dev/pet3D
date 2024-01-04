using CodeBase.Logic;
using UnityEngine;

public class SantaAnimator : MonoBehaviour
{
  private static readonly int MoveHash = Animator.StringToHash("Walking");
  private static readonly int AttackHash = Animator.StringToHash("Fire");
  private static readonly int HitHash = Animator.StringToHash("Hit");
  private static readonly int DieHash = Animator.StringToHash("Die");

  [SerializeField] private Animator _animator;
  [SerializeField] private CharacterController CharacterController;

  public AnimatorState State { get; private set; }
  public bool IsAttacking() => State == AnimatorState.Attack;

  private void Update() =>
    _animator.SetFloat(MoveHash, CharacterController.velocity.magnitude, 0.1f, Time.deltaTime);

  public void PlayHit() => _animator.SetTrigger(HitHash);
  public void PlayAttack() => _animator.SetTrigger(AttackHash);
  public void PlayDeath() => _animator.SetTrigger(DieHash);
}