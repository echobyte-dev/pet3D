using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
  public class Aggro : MonoBehaviour
  {
    [SerializeField] private TriggerObserver _triggerObserver;
    [SerializeField] private Follow _follow;

    [SerializeField] private float Cooldown;
    private bool _hasAggroTarget;

    private Coroutine _aggroCoroutine;
    private WaitForSeconds _switchFollowOffAfterCooldown;

    private void Start()
    {
      _switchFollowOffAfterCooldown = new WaitForSeconds(Cooldown);

      _triggerObserver.TriggerEnter += TriggerEnter;
      _triggerObserver.TriggerExit += TriggerExit;

      _follow.enabled = false;
    }

    private void TriggerEnter(Collider obj)
    {
      if(_hasAggroTarget) return;

      StopAggroCoroutine();

      SwitchFollowOn();
    }

    private void TriggerExit(Collider obj)
    {
      if(!_hasAggroTarget) return;

      _aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
    }

    private void StopAggroCoroutine()
    {
      if(_aggroCoroutine == null) return;

      StopCoroutine(_aggroCoroutine);
      _aggroCoroutine = null;
    }

    private IEnumerator SwitchFollowOffAfterCooldown()
    {
      yield return _switchFollowOffAfterCooldown;

      SwitchFollowOff();
    }

    private void SwitchFollowOn()
    {
      _hasAggroTarget = true;
      _follow.enabled = true;
    }

    private void SwitchFollowOff()
    {
      _follow.enabled = false;
      _hasAggroTarget = false;
    }

    private void OnDestroy()
    {
      _triggerObserver.TriggerEnter -= TriggerEnter;
      _triggerObserver.TriggerExit -= TriggerExit;
    }
  }
}