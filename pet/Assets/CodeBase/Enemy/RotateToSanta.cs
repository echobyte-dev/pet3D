using CodeBase.Infrastructure.Factory;
using UnityEngine;
using Zenject;

namespace CodeBase.Enemy
{
  public class RotateToSanta : Follow
  {
    public float Speed;

    private Transform _heroTransform;
    private IGameFactory _gameFactory;
    private Vector3 _positionToLook;

    [Inject]
    public void Construct(IGameFactory gameFactory)
    {
      _gameFactory = gameFactory;
    }

    private void Start()
    {
      if (IsPlayerExist())
        InitializePlayerTransform();
      else
        _gameFactory.SantaCreated += PlayerCreated;
    }

    private void Update()
    {
      if (IsInitialized())
        RotateTowardsPlayer();
    }

    private bool IsPlayerExist() => 
      _gameFactory.SantaGameObject != null;

    private void RotateTowardsPlayer()
    {
      UpdatePositionToLookAt();

      transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
    }

    private void UpdatePositionToLookAt()
    {
      Vector3 positionDelta = _heroTransform.position - transform.position;
      _positionToLook = new Vector3(positionDelta.x, transform.position.y, positionDelta.z);
    }

    private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook) =>
      Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());

    private Quaternion TargetRotation(Vector3 position) =>
      Quaternion.LookRotation(position);

    private float SpeedFactor() =>
      Speed * Time.deltaTime;

    private bool IsInitialized() => 
      _heroTransform != null;

    private void PlayerCreated() =>
      InitializePlayerTransform();

    private void InitializePlayerTransform() =>
      _heroTransform = _gameFactory.SantaGameObject.transform;

    private void OnDestroy()
    {
      if(_gameFactory != null)
        _gameFactory.SantaCreated -= PlayerCreated;
    }
  }
}