using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.SaveLoad
{
  public interface ISaveLoadService
  {
    void SaveProgress();
    PlayerProgress LoadProgress();
  }
}