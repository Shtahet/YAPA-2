﻿using System;
using System.Windows.Input;
using YAPA.Shared.Contracts;

namespace YAPA.Shared.Common
{
    public class StartCommand : ICommand
    {
        private readonly IPomodoroEngine _engine;
        public StartCommand(IPomodoroEngine engine)
        {
            _engine = engine;
            _engine.PropertyChanged += _engine_PropertyChanged;
        }

        private void _engine_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_engine.Phase))
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _engine.Phase != PomodoroPhase.Work && _engine.Phase != PomodoroPhase.Break;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _engine.Start();
        }

    }
}
