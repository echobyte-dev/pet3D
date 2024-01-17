using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.StaticData;
using CodeBase.UI;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Factory;

namespace CodeBase.Infrastructure.States
{
  public class GameStateMachine
  {
    private readonly Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory,
      IPersistentProgressService progressService, ISaveLoadService saveLoadService, IStaticDataService staticData,
      IUIFactory uiFactory)
    {
      _states = new Dictionary<Type, IExitableState>()
      {
        [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, staticData),
        [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain, gameFactory, progressService, staticData, uiFactory),
        [typeof(LoadProgressState)] = new LoadProgressState(this, progressService, saveLoadService),
        [typeof(GameLoopState)] = new GameLoopState(this)
      };
    }

    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();

      TState state = GetState<TState>();
      _activeState = state;

      return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState =>
      _states[typeof(TState)] as TState;
  }
}