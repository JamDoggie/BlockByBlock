using BlockByBlockLauncher.mojang_auth;
using ReactiveUI;
using System;
using System.Reactive;

namespace BlockByBlockLauncher.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private MicrosoftAuthManager _authManager;

        private bool _loggedIn = false;
        public bool LoggedIn
        {
            get => _loggedIn;
            set => this.RaiseAndSetIfChanged(ref _loggedIn, value);
        }

        private string _loadingProgress = string.Empty;
        public string LoadingProgress
        {
            get => _loadingProgress;
            set => this.RaiseAndSetIfChanged(ref _loadingProgress, value);
        }

        private float _loadingProgressValue = 0;
        public float LoadingProgressValue
        {
            get => _loadingProgressValue;
            set => this.RaiseAndSetIfChanged(ref _loadingProgressValue, value);
        }

        private bool _awaitingLogin = false;
        public bool AwaitingLogin
        {
            get => _awaitingLogin;
            set => this.RaiseAndSetIfChanged(ref _awaitingLogin, value);
        }

        private string _usernameText = "Please log in to continue.";
        public string UsernameText
        {
            get => _usernameText;
            set => this.RaiseAndSetIfChanged(ref _usernameText, value);
        }

        public ReactiveCommand<Unit, Unit> LogInCommand { get; }
        public ReactiveCommand<Unit, Unit> LogOutCommand { get; }

        public MainViewModel()
        {
            LogInCommand = ReactiveCommand.Create(LogIn);
            LogOutCommand = ReactiveCommand.Create(LogOut);

            _authManager = new(this);
        }

        void LogIn()
        {
            if (!LoggedIn)
            {
                _authManager.LogIn();
                AwaitingLogin = true;
            }
        }

        void LogOut()
        {
            if (LoggedIn)
            {
                _authManager.LogOut();
            }
        }

        void Launch()
        {
            
        }
    }
}