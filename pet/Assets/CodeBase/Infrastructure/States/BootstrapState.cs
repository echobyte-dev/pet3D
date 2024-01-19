using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services;
using CodeBase.StaticData;

namespace CodeBase.Infrastructure.States
{
  public class BootstrapState : IState
  {
    private const string Initial = "Initial";
    public static IInputService InputService;
    
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly IStaticDataService _staticData;
    private readonly IAssetProvider _assetProvider;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, IStaticDataService staticData, IAssetProvider assetProvider)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _staticData = staticData;
      _assetProvider = assetProvider;
    }

    public void Enter()
    {
      RegisterServices();
      _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
    }

    private void EnterLoadLevel() => 
      _stateMachine.Enter<LoadProgressState>();

    public void Exit()
    {
    }

    private void RegisterServices()
    {
      InputService = RegisterInputService();
      _staticData.Load();
      _assetProvider.Initialize();
    }

    private static IInputService RegisterInputService() =>
      new StandaloneInputService();
  }
}