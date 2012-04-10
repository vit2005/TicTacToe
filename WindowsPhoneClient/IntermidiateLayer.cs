using System;
using System.Collections.Generic;
using System.Windows;
using WindowsPhoneClient.Model;

namespace WindowsPhoneClient
{
    public class IntermidiateLayer
    {
        MainPage mainPage;

        public IntermidiateLayer(MainPage mainWindow)
        {
            this.mainPage = mainWindow;
        }

        public void UpdatePlayersList(List<Person> personList)
        {
            mainPage.Dispatcher.BeginInvoke(new Action(
                delegate()
                {
                    mainPage.UpdatePlayerListBox(personList);
                }));
        }

        public bool HandleInvite(string name)
        {
            return MessageBoxResult.OK == MessageBox.Show(string.Format("Do you want to play with {0}", name), "Play invite", MessageBoxButton.OKCancel);
        }

        public void EnableButtons()
        {
            mainPage.Dispatcher.BeginInvoke(new Action(
                delegate()
                {
                    mainPage.EnableButtons();
                }));
        }

        public void DisableButtons()
        {
            mainPage.Dispatcher.BeginInvoke(new Action(
                delegate()
                {
                    mainPage.DisableButtons();
                }));
        }

        public void StartNonGamingClient()
        {
            mainPage.StartNonGamingClient();
        }

        public  void SetFigures(char yourFigure, char opponentFirure)
        {
            mainPage.YourFigure = yourFigure;
            mainPage.OpponentFigre = opponentFirure;
        }

        public  void MakeOpponentMove(string move)
        {
            mainPage.Dispatcher.BeginInvoke(new Action(
                delegate()
                {
                    mainPage.MakeOpponentMove(move);
                }));
        }

        public void Win()
        {
            MessageBox.Show("You win!", "Result", MessageBoxButton.OK);
        }

        public void Lose()
        {
            MessageBox.Show("You lose!", "Result", MessageBoxButton.OK);
        }

        public void CleanButtonsContent()
        {
            mainPage.Dispatcher.BeginInvoke(new Action(
                delegate()
                {
                    mainPage.CleanButtonsContent();
                }));
        }
    }
}
