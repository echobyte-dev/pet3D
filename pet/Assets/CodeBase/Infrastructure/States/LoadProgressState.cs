using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;

namespace CodeBase.Infrastructure.States
{
  public class LoadProgressState : IState
  {
    private readonly GameStateMachine _gameStateMachine;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadProgress;

    public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadProgress)
    {
      _gameStateMachine = gameStateMachine;
      _progressService = progressService;
      _saveLoadProgress = saveLoadProgress;
    }

    public void Enter()
    {
      LoadProgressOrInitNew();

      _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
    }

    public void Exit()
    {
    }

    private void LoadProgressOrInitNew()
    {
      _progressService.Progress = 
        _saveLoadProgress.LoadProgress() 
        ?? NewProgress();
    }

    private PlayerProgress NewProgress()
    {
      var progress = new PlayerProgress("Level1");

      progress.SantaState.MaxHP = 50;
      progress.SantaStats.Damage = 10f;
      progress.SantaStats.DamageRadius = 0.5f;
      progress.SantaState.ResetHP();

      return progress;
    }
  }
}