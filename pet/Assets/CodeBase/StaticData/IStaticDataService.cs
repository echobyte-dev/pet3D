namespace CodeBase.StaticData
{
  public interface IStaticDataService
  {
    void LoadMonsters();
    MonsterStaticData ForMonster(MonsterTypeId typeId);
  }
}