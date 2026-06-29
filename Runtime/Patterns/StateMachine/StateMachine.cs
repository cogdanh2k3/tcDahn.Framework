using System;
using System.Collections.Generic;

namespace tcDahn
{
    public class StateMachine<TLabel>
    {
        #region Types
        private class State
        {
            #region Public Fields

            public readonly TLabel Label;
            public readonly Action OnStart;
            public readonly Action OnStop;
            public readonly Action OnUpdate;

            #endregion

            #region Constructors

            public State(TLabel label, Action onStart, Action onUpdate, Action onStop)
            {
                this.OnStart = onStart;
                this.OnUpdate = onUpdate;
                this.OnStop = onStop;
                this.Label = label;
            }

            #endregion
        }
        #endregion

        #region Private Fields

        private readonly Dictionary<TLabel, State> _stateDictionary;
        private State _currentState;
        private State _previousState;

        #endregion

        #region Properties
        public TLabel PreviousState => _previousState == null ? _currentState.Label : _previousState.Label;

        public TLabel CurrentState
        {
            get => _currentState.Label;

            set => ChangeState(value);
        }
        #endregion

        #region Constructors
        public StateMachine()
        {
            _stateDictionary = new Dictionary<TLabel, State>();
        }
        #endregion

        #region Public Methods
        public void Update()
        {
            if (_currentState != null && _currentState.OnUpdate != null)
            {
                _currentState.OnUpdate();
            }
        }

        public void AddState(TLabel label)
        {
            _stateDictionary[label] = new State(label, null, null, null);
        }

        public void AddState(TLabel label, IStateMachine stateMachine)
        {
            stateMachine.Init();

            _stateDictionary[label] = new State(label, stateMachine.OnStart, stateMachine.OnUpdate, stateMachine.OnStop);
        }

        public void AddState(TLabel label, Action onStart, Action onUpdate, Action onStop)
        {
            _stateDictionary[label] = new State(label, onStart, onUpdate, onStop);
        }

        public void AddState<TSubStateLabel>(TLabel label, StateMachine<TSubStateLabel> subMachine, TSubStateLabel subMachineStartState)
        {
            AddState(label, () => subMachine.ChangeState(subMachineStartState), subMachine.Update, null);
        }

        public override string ToString()
        {
            return CurrentState.ToString();
        }
        #endregion

        #region Private Methods
        private void ChangeState(TLabel newState)
        {
            _previousState = _currentState;

            if (_currentState != null && _currentState.OnStop != null)
            {
                _currentState.OnStop();
            }

            _currentState = _stateDictionary[newState];

            _currentState.OnStart?.Invoke();

            EventStateChanged?.Invoke(this);
        }
        #endregion

        #region Callback Events
        public event Action<StateMachine<TLabel>> EventStateChanged;
        #endregion
    }
}
