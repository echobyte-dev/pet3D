using UnityEngine;

public class SantaAnimator : MonoBehaviour
{
  private static readonly int MoveHash = Animator.StringToHash("Walking");
  private static readonly int AttackHash = Animator.StringToHash("Fire");
  private static readonly int HitHash = Animator.StringToHash("Hit");
  private static readonly int DieHash = Animator.StringToHash("Die");

  [SerializeField] private Animator Animator;
  [SerializeField] private CharacterController CharacterController;

  private void Update() =>
    Animator.SetFloat(MoveHash, CharacterController.velocity.magnitude, 0.1f, Time.deltaTime);

  public void PlayHit() => Animator.SetTrigger(HitHash);
  public void PlayAttack() => Animator.SetTrigger(AttackHash);
  public void PlayDeath() => Animator.SetTrigger(DieHash);
}