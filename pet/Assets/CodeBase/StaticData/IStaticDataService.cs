using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;

namespace CodeBase.StaticData
{
  public interface IStaticDataService
  {
    void Load();
    MonsterStaticData ForMonster(MonsterTypeId typeId);
    LevelStaticData ForLevel(string sceneKey);
    WindowConfig ForWindow(WindowId shop);
  }
}