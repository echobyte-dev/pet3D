using CodeBase.Data.Loot;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Randomizer;
using UnityEngine;

namespace CodeBase.Enemy
{
  public class LootSpawner : MonoBehaviour
  {
    [SerializeField] private EnemyDeath _enemyDeath;
    private IGameFactory _gameFactory;
    private int _lootMin;
    private int _lootMax;
    private IRandomService _random;

    public void Construct(IGameFactory gameFactory, IRandomService random)
    {
      _random = random;
      _gameFactory = gameFactory;
    }

    private void Start()
    {
      _enemyDeath.Happened += SpawnLoot;
    }

    private async void SpawnLoot()
    {
      LootPiece loot = await _gameFactory.CreateLoot();
      loot.transform.position = transform.position;

      var lootItem = GenerateLoot();
      loot.Initialize(lootItem);
    }

    private Loot GenerateLoot()
    {
      return new Loot
      {
        Value = _random.Next(_lootMin, _lootMax)
      };
    }

    public void SetLootValue(int min, int max)
    {
      _lootMin = min;
      _lootMax = max;
    }
  }
}