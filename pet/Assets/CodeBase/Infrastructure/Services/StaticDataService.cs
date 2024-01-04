using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
  public class StaticDataService : IStaticDataService
  {
    private const string MonstersDataPath = "StaticData/Monsters";
    private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;

    public StaticDataService() => 
      LoadMonsters();

    public void LoadMonsters()
    {
      _monsters = Resources
        .LoadAll<MonsterStaticData>(MonstersDataPath)
        .ToDictionary(x => x.MonsterTypeId, x => x);
    }

    public MonsterStaticData ForMonster(MonsterTypeId monsterTypeId) => 
      _monsters.TryGetValue(monsterTypeId, out MonsterStaticData staticData) 
        ? staticData 
        : null;
  }
}