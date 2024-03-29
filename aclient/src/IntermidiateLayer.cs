﻿using System;
using System.Collections.Generic;
using System.Windows;
using DesktopClient.Model;

namespace DesktopClient
{
    public class IntermidiateLayer
    {
        private MainWindow mainWindow;

        public IntermidiateLayer(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public void UpdatePlayersList(List<Person> personList)
        {
            mainWindow.Dispatcher.Invoke(new Action(
                delegate()
                {
                    mainWindow.UpdatePlayerListBox(personList);
                }));
        }

        public bool HandleInvite(string name)
        {
            return MessageBoxResult.Yes == MessageBox.Show(string.Format("Do you want to play with {0}", name), "Play invite", MessageBoxButton.YesNo);
        }

        public void EnableButtons()
        {
            mainWindow.Dispatcher.Invoke(new Action(
                delegate()
                {
                    mainWindow.EnableButtons();
                }));
        }

        public void DisableButtons()
        {
            mainWindow.Dispatcher.Invoke(new Action(
                delegate()
                {
                    mainWindow.DisableButtons();
                }));
        }

        public void StartNonGamingClient()
        {
            mainWindow.StartNonGamingClient();
        }

        public void SetFigures(char yourFigure, char opponentFirure)
        {
            mainWindow.YourFigure = yourFigure;
            mainWindow.OpponentFigre = opponentFirure;
        }

        public void MakeMove(string position, Move.MoveEnum move)
        {
            mainWindow.Dispatcher.Invoke(new Action(
                delegate()
                {
                    mainWindow.MakeMove(position, move);
                }));
        }

        public void Win()
        {
            MessageBox.Show("You win!", "Result");
        }

        public void Lose()
        {
            MessageBox.Show("You lose!", "Result");
        }

        public void CleanButtonsContent()
        {
            mainWindow.Dispatcher.Invoke(new Action(
                delegate()
                {
                    mainWindow.CleanButtonsContent();
                }));
        }
    }
}
