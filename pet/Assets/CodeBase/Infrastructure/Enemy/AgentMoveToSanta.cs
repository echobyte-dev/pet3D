using CodeBase.Infrastructure.Factories;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace CodeBase.Infrastructure.Enemy
{
  public class AgentMoveToSanta : Follow
  {
    private const float MinimalDistance = 1;

    [SerializeField] private NavMeshAgent _agent;
    private Transform _playerTransform;
    private IGameFactory _gameFactory;

    [Inject]
    public void Construct(IGameFactory gameFactory)
    {
      _gameFactory = gameFactory;

      if (_gameFactory.SantaGameObject != null)
        InitializeSantaTransform();
      else
      {
        _gameFactory.SantaCreated += SantaCreated;
      }
    }

    private void Update()
    {
      if (Initialized() && PlayerNotReached())
        _agent.destination = _playerTransform.position;
    }

    private bool Initialized() => 
      _playerTransform != null;

    private void SantaCreated() => 
      InitializeSantaTransform();

    private void InitializeSantaTransform() => 
      _playerTransform = _gameFactory.SantaGameObject.transform;

    private bool PlayerNotReached() => 
      Vector3.Distance(_agent.transform.position, _playerTransform.position) >= MinimalDistance;
  }
}