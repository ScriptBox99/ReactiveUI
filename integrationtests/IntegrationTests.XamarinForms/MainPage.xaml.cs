﻿// Copyright (c) 2021 .NET Foundation and Contributors. All rights reserved.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Acr.UserDialogs;
using IntegrationTests.Shared;
using ReactiveUI;
using ReactiveUI.XamForms;

namespace IntegrationTests.XamarinForms
{
    /// <summary>
    /// The main page for the application.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class MainPage : ReactiveContentPage<LoginViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            this
               .WhenActivated(
                   disposables =>
                   {
                       this
                           .Bind(ViewModel, vm => vm.UserName, v => v.Username.Text)
                           .DisposeWith(disposables);

                       this
                           .Bind(ViewModel, vm => vm.Password, v => v.Password.Text)
                           .DisposeWith(disposables);

                       this
                           .BindCommand(ViewModel, vm => vm.Login, v => v.Login)
                           .DisposeWith(disposables);
                       this
                           .BindCommand(ViewModel, vm => vm.Cancel, v => v.Cancel)
                           .DisposeWith(disposables);

                       ViewModel
                           .Login
                           .Select(
                               result =>
                               {
                                   if (result)
                                   {
                                       UserDialogs.Instance.Alert("Login Successful", "Welcome!");
                                   }
                                   else
                                   {
                                       UserDialogs.Instance.Alert("Login Failed", "Ah, ah, ah, you didn't say the magic word!");
                                   }

                                   return Unit.Default;
                               })
                           .Subscribe()
                           .DisposeWith(disposables);
                   });
        }
    }
}
