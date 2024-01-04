using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
  public class AgentMoveToSanta : Follow
  {
    public NavMeshAgent Agent;
    private Transform _santaTransform;
    private const float _minimalDistance = 1;

    public void Construct(Transform heroTransform) =>
      _santaTransform = heroTransform;

    private void Update()
    {
      SetDestinationForAgent();
    }

    private void SetDestinationForAgent()
    {
      if (HeroNotReached()) return;
      Agent.destination = _santaTransform.position;
    }

    private bool HeroNotReached()
    {
      if (Vector3.Distance(transform.position, _santaTransform.position) <= _minimalDistance) 
        return true;
      
      return false;
    }
  }
}