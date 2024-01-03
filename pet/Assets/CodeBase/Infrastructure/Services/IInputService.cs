using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
  public interface IInputService
  {
    Vector2 Axis { get; }

    bool IsAttackButtonUp();
  }
}