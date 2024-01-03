using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Santa
{
  [RequireComponent(typeof(CharacterController))]
  public class SantaMove : MonoBehaviour, ISavedProgress
  {
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _movementSpeed;

    private IInputService _inputService;

    private void Awake() =>
      _inputService = BootstrapState.InputService;

    private void Update() => 
      CalculateMoveVector();

    public void UpdateProgress(PlayerProgress progress) =>
      progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());

    public void LoadProgress(PlayerProgress progress)
    {
      if (CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
      {
        Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
        if (savedPosition != null)
          Warp(to: savedPosition);
      }
    }

    private void CalculateMoveVector()
    {
      Vector3 movementVector = Vector3.zero;

      if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
      {
        movementVector = Camera.main.transform.TransformDirection(_inputService.Axis);
        movementVector.y = 0;
        movementVector.Normalize();

        transform.forward = movementVector;
      }

      movementVector += Physics.gravity;
      _characterController.Move(_movementSpeed * movementVector * Time.deltaTime);
    }

    private void Warp(Vector3Data to)
    {
      _characterController.enabled = false;
      transform.position = to.AsUnityVector().AddY(_characterController.height);
      _characterController.enabled = true;
    }

    private static string CurrentLevel() =>
      SceneManager.GetActiveScene().name;
  }
}