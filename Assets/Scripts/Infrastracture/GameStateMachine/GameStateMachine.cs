using System;
using System.Collections.Generic;

public class GameStateMachine
{
    private readonly Dictionary<Type, IState> _states;
    private IState _activeState;

    public GameStateMachine(UIFactory uifactory, Wallet wallet)
    {
        _states = new Dictionary<Type, IState>()
        {
            [typeof(BootstrapState)] = new BootstrapState(this, uifactory),
            [typeof(DataLoadState)] = new DataLoadState(uifactory, wallet)
        };
    }

    public void Enter<TState>() where TState : IState
    {
        _activeState = _states[typeof(TState)];
        _activeState.Enter();
    }
}
