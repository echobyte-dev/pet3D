using UnityEngine;

namespace CodeBase.Enemy
{
  public class EnemyAnimator : MonoBehaviour
  {
    private static readonly int Die = Animator.StringToHash("Die");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    private Animator _animator;

    private void Awake() =>
      _animator = GetComponent<Animator>();

    public void PlayHit() => _animator.SetTrigger(Hit);
    public void PlayDeath() => _animator.SetTrigger(Die);

    public void Move(float speed)
    {
      _animator.SetBool(IsMoving, true);
      _animator.SetFloat(Speed, speed);
    }

    public void StopMoving() => _animator.SetBool(IsMoving, false);
    public void PlayAttack() => _animator.SetTrigger(Attack);
  }
}