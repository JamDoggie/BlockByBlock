using BlockByBlockLauncher.ViewModels;
using CmlLib.Core.Auth;
using CmlLib.Core.Auth.Microsoft;
using CmlLib.Core.Auth.Microsoft.Cache;
using CmlLib.Core.Auth.Microsoft.MsalClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlockByBlockLauncher.mojang_auth
{
    public class MicrosoftAuthManager
    {
        public const string AzureClientID = "29a62d6c-3510-40b5-8c34-40f76943a792";

        private IPublicClientApplication _clientApp;

        private JavaEditionLoginHandler _loginHandler;
        
        private MSession? _session;

        public MSession? Session
        {
            internal get => _session;
            set => _session = value;
        }
        
        private MainViewModel _viewModel;

        public MicrosoftAuthManager(MainViewModel viewModel)
        {
            CreateApplication();
            _viewModel = viewModel;
        }

        private async void CreateApplication()
        {
            _clientApp = await MsalMinecraftLoginHelper.BuildApplicationWithCache(AzureClientID);
        }
        
        public async void LogIn()
        {
            // This is C#. Why is this library made like this is Java????
            _loginHandler = new LoginHandlerBuilder()
                .ForJavaEdition()
                .WithMsalOAuth(_clientApp, factory => factory.CreateInteractiveApi())
                .Build();

            try
            {
                _viewModel.AwaitingLogin = true;
                _viewModel.LoggedIn = true;

                JavaEditionSessionCache gameSessionCache = await _loginHandler.LoginFromOAuth();
                Console.WriteLine("Login success!");

                Session = gameSessionCache.GameSession;

                _viewModel.UsernameText = $"Currently logged in as {Session.Username}";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to login; " + ex.ToString());
                _viewModel.LoggedIn = false;
            }
            finally
            {
                _viewModel.AwaitingLogin = false;
            }
        }

        public async void LogOut()
        {
            if (!_viewModel.LoggedIn)
                return;
            
            if (_loginHandler != null)
                await _loginHandler.ClearCache();

            _viewModel.UsernameText = "Logged out. Please log in to continue.";
            _viewModel.LoggedIn = false;

            Session = null;

            Console.WriteLine("Logged out successfully.");
        }
    }
}
