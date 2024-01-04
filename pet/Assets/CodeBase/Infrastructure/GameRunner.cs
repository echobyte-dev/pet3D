using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
  public class GameRunner : MonoBehaviour
  {
    public GameBootstrapper BootstrapperPrefab;
    
    private IGameFactory _gameFactory;
    private IPersistentProgressService _progressService;
    private ISaveLoadService _saveLoadService;
    
    [Inject]
    public void Construct(IGameFactory gameFactory, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
    {
      _gameFactory = gameFactory;
      _progressService = progressService;
      _saveLoadService = saveLoadService;
    }
    
    private void Awake()
    {
      var bootstrapper = FindObjectOfType<GameBootstrapper>();
      
      if(bootstrapper != null) return;

      BootstrapperPrefab.Construct(_gameFactory, _progressService, _saveLoadService);
      Instantiate(BootstrapperPrefab);
    }
  }
}