using UnityEngine;

namespace CodeBase.Enemy
{
  public class RotateToSanta : Follow
  {
    [SerializeField] private float _speed;

    private Transform _playerTransform;
    private Vector3 _positionToLook;

    public void Construct(Transform heroTransform)
      => _playerTransform = heroTransform;
    
    private void Update()
    {
      if (_playerTransform)
        RotateTowardsHero();
    }

    private void RotateTowardsHero()
    {
      UpdatePositionToLookAt();

      transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
    }

    private void UpdatePositionToLookAt()
    {
      Vector3 positionDelta = _playerTransform.position - transform.position;
      _positionToLook = new Vector3(positionDelta.x, transform.position.y, positionDelta.z);
    }
    
    private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook) =>
      Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());

    private Quaternion TargetRotation(Vector3 position) =>
      Quaternion.LookRotation(position);

    private float SpeedFactor() =>
      _speed * Time.deltaTime;
  }
}