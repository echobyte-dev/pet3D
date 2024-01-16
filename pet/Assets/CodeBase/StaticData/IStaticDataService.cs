namespace CodeBase.StaticData
{
  public interface IStaticDataService
  {
    MonsterStaticData ForMonster(MonsterTypeId typeId);
    LevelStaticData ForLevel(string sceneKey);
  }
}