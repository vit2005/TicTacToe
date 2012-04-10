using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using WindowsPhoneClient.Model;
using WindowsPhoneClient.Model.Gaming;
using WindowsPhoneClient.Model.PreGaming;

namespace WindowsPhoneClient
{
    public partial class MainPage : PhoneApplicationPage
    {
        //fields
        private ClientManager clientManager;
        private GameManager gameManager;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            clientManager = new ClientManager(this);
            gameManager = new GameManager(this);
            clientManager.NewGame += gameManager.NewGame;
        }

        //properties
        public char YourFigure { get; set; }
        public char OpponentFigre { get; set; }

        //methods
        public void DisableButtons()
        {
            foreach (Button button in BattleGrid.Children)
            {
                button.IsEnabled = false;
            }
        }

        public void EnableButtons()
        {
            foreach (Button button in BattleGrid.Children)
            {
                button.IsEnabled = true;
            }
        }

        public void UpdatePlayerListBox(List<Person> personList)
        {
            PlayersList.Items.Clear();
            foreach (Person person in personList)
            {
                PlayersList.Items.Add(person.Name);
            }
        }

        public void CleanButtonsContent()
        {
            foreach (Button button in BattleGrid.Children)
            {
                button.Content = null;
            }
        }

        public void MakeOpponentMove(string move)
        {
            foreach (Button button in BattleGrid.Children)
            {
                if (Regex.IsMatch(button.Name, string.Format("{0}$", move)))
                {
                    button.Content = OpponentFigre;
                }
            }
        }

        internal void StartNonGamingClient()
        {
            clientManager.RunClientThread();
        }

        private void ConnectButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (Regex.IsMatch(ServerIPTextBox.Text, @"^\d{1,3}[.]\d{1,3}[.]\d{1,3}[.]\d{1,3}$")
                && Regex.IsMatch(ServerPortTextBox.Text, @"^\d{1,5}$"))
            {
                if (clientManager.Login(new IPEndPoint(IPAddress.Parse(ServerIPTextBox.Text), int.Parse(ServerPortTextBox.Text)), NameTextBox.Text))
                {
                    clientManager.RunClientThread();
                    ConnectButton.IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("Server has reject you.");
                }
            }
            else
            {
                MessageBox.Show("Incorrect connection data.");
            }
        }

        private void PhoneApplicationPage_Unloaded(object sender, RoutedEventArgs e)
        {
            gameManager.Exit();
            clientManager.Exit();
            Thread.Sleep(1000);
        }

        private void PlayersList_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if ((sender as ListBox).SelectedIndex > -1)
            {
                clientManager.SendInvite((sender as ListBox).SelectedItem.ToString());
            }
        }

        private void BattleButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.Content == null)
            {
                button.Content = YourFigure;
                string pattern = @"\d$";
                Match match = Regex.Match(button.Name, pattern);
                gameManager.Move(int.Parse(match.Value));
            }
        }
    }
}