﻿using System;
using System.Windows.Input;

namespace TechHealth.Core
{
    public class RelayCommand:ICommand
    {
        private Action<object> _execute;
        private Func<object,bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object,bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action<object> execute) : this(execute, null) { }

        public bool CanExecute(object parameters)
        {
            return _canExecute == null ||  _canExecute(parameters);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameters)
        {
            _execute(parameters);
        }
    }
}