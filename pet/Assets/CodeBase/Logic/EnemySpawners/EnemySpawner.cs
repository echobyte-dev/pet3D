using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Factory;
using CodeBase.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.EnemySpawners
{
  public class EnemySpawner : MonoBehaviour, ISavedProgress
  {
    public MonsterTypeId MonsterTypeId;

    private IGameFactory _gameFactory;
    private EnemyDeath _enemyDeath;

    private string _id;
    private bool _slain;

    [Inject]
    public void Construct(IGameFactory gameFactory)
    {
      _gameFactory = gameFactory;
    }

    private void Awake() =>
      _id = GetComponent<UniqueId>().Id;

    private void OnDestroy()
    {
      if (_enemyDeath != null)
        _enemyDeath.Happened -= Slay;
    }

    public void LoadProgress(PlayerProgress progress)
    {
      if (progress.KillData.ClearedSpawners.Contains(_id))
        _slain = true;
      else
        Spawn();
    }

    public void UpdateProgress(PlayerProgress progress)
    {
      if (_slain)
        progress.KillData.ClearedSpawners.Add(_id);
    }

    private void Spawn()
    {
      GameObject monster = _gameFactory.CreateMonster(MonsterTypeId, transform);
      _enemyDeath = monster.GetComponent<EnemyDeath>();
      _enemyDeath.Happened += Slay;
    }

    private void Slay()
    {
      if (_enemyDeath != null)
        _enemyDeath.Happened -= Slay;

      _slain = true;
    }
  }
}