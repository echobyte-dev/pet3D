using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI
{
  public class ActorUI : MonoBehaviour
  {
    [SerializeField] HealthBar _healthBar;

    private IHealth _health;

    public void Construct(IHealth health)
    {
      _health = health;
      _health.OnHealthChange += UpdateHpBar;
    }

    private void Start()
    {
      IHealth health = GetComponent<IHealth>();

      if (health != null)
        Construct(health);
    }

    private void OnDestroy() =>
      _health.OnHealthChange -= UpdateHpBar;

    private void UpdateHpBar()
    {
      _healthBar.SetValue(_health.CurrentHealth, _health.MaxHealth);
    }
  }
}