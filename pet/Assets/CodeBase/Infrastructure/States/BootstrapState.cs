using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.States
{
  public class BootstrapState : IState
  {
    private const string Initial = "Initial";
    public static IInputService InputService;
    
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
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

    private static void RegisterServices()
    {
      InputService = RegisterInputService();
    }

    private static IInputService RegisterInputService() =>
      new StandaloneInputService();
  }
}