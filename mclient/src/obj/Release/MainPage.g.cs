﻿#pragma checksum "D:\Dropbox\Education\Visual Studio 2010\Projects\TicTacToe\mclient\src\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "88137D81026149BEA44D1101AF1BB9F1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.261
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace WindowsPhoneClient {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBox ServerIPTextBox;
        
        internal System.Windows.Controls.TextBox ServerPortTextBox;
        
        internal System.Windows.Controls.TextBox NameTextBox;
        
        internal System.Windows.Controls.Button ConnectButton;
        
        internal System.Windows.Controls.ListBox PlayersList;
        
        internal System.Windows.Controls.Grid BattleGrid;
        
        internal System.Windows.Controls.Button BattleButton0;
        
        internal System.Windows.Controls.Button BattleButton1;
        
        internal System.Windows.Controls.Button BattleButton2;
        
        internal System.Windows.Controls.Button BattleButton3;
        
        internal System.Windows.Controls.Button BattleButton4;
        
        internal System.Windows.Controls.Button BattleButton5;
        
        internal System.Windows.Controls.Button BattleButton6;
        
        internal System.Windows.Controls.Button BattleButton7;
        
        internal System.Windows.Controls.Button BattleButton8;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/WindowsPhoneClient;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.ServerIPTextBox = ((System.Windows.Controls.TextBox)(this.FindName("ServerIPTextBox")));
            this.ServerPortTextBox = ((System.Windows.Controls.TextBox)(this.FindName("ServerPortTextBox")));
            this.NameTextBox = ((System.Windows.Controls.TextBox)(this.FindName("NameTextBox")));
            this.ConnectButton = ((System.Windows.Controls.Button)(this.FindName("ConnectButton")));
            this.PlayersList = ((System.Windows.Controls.ListBox)(this.FindName("PlayersList")));
            this.BattleGrid = ((System.Windows.Controls.Grid)(this.FindName("BattleGrid")));
            this.BattleButton0 = ((System.Windows.Controls.Button)(this.FindName("BattleButton0")));
            this.BattleButton1 = ((System.Windows.Controls.Button)(this.FindName("BattleButton1")));
            this.BattleButton2 = ((System.Windows.Controls.Button)(this.FindName("BattleButton2")));
            this.BattleButton3 = ((System.Windows.Controls.Button)(this.FindName("BattleButton3")));
            this.BattleButton4 = ((System.Windows.Controls.Button)(this.FindName("BattleButton4")));
            this.BattleButton5 = ((System.Windows.Controls.Button)(this.FindName("BattleButton5")));
            this.BattleButton6 = ((System.Windows.Controls.Button)(this.FindName("BattleButton6")));
            this.BattleButton7 = ((System.Windows.Controls.Button)(this.FindName("BattleButton7")));
            this.BattleButton8 = ((System.Windows.Controls.Button)(this.FindName("BattleButton8")));
        }
    }
}

