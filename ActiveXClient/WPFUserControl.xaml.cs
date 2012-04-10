using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ActiveXClient.Model;
using ActiveXClient.Model.Gaming;
using ActiveXClient.Model.PreGaming;

namespace ActiveXClient
{
    
    public partial class WPFUserControl : UserControl
    {
        //fields
        ClientManager clientManager;
        GameManager gameManager;

        //constructors
        public WPFUserControl()
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
        private void ConnectButton_Click(object sender, RoutedEventArgs e)
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (gameManager != null)
            {
                gameManager.Exit();
            }
            clientManager.Exit();
            Thread.Sleep(1000);
        }

        public void UpdatePlayerListBox(List<Person> personList)
        {
            PlayersListBox.Items.Clear();
            foreach (Person person in personList)
            {
                PlayersListBox.Items.Add(person.Name);
            }
        }

        private void PlayersListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((sender as ListBox).SelectedIndex > -1)
            {
                clientManager.SendInvite((sender as ListBox).SelectedItem.ToString());
            }
        }

        public void EnableButtons()
        {
            foreach (Button button in BattleGrid.Children)
            {
                button.IsEnabled = true;
            }
        }

        public void DisableButtons()
        {
            foreach (Button button in BattleGrid.Children)
            {
                button.IsEnabled = false;
            }
        }

        public void StartNonGamingClient()
        {
            clientManager.RunClientThread();
        }

        private void GameButtonClick(object sender, RoutedEventArgs e)
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

        public void CleanButtonsContent()
        {
            foreach (Button button in BattleGrid.Children)
            {
                button.Content = null;
            }
        }

        
    }

}
