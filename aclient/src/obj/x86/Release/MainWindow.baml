   M S B A M L   `   `   `  ���� H  DDesktopClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:DesktopClient.ViewModelDesktopClient.ViewModel      DesktopClient.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     4vm%clr-namespace:DesktopClient.ViewModel     -     � Title$  TicTacToe Game��$	��321��$	��559��.��� .+�� -   $��
ConfigGrid��$
��Left=�$��	12,12,0,0q�$	��Top=�$	��150�� � ColumnDefinitions �� $
��Auto ��� $��75 � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -   $��	NameLabel��$��0��$��0��$��Name:���� -   $��IPLabel��$��0��$��1��$��
Server IP:���� -   $��	PortLabel��$��0��$��2��$��Server port:���� -   $��NameTextBox��$��1��$��0��$��DesktopClient���� -   $��ServerIPTextBox��$��1��$��1��$��	127.0.0.1���� -   $��ServerPortTextBox��$��1��$��2��$��10060���� -	   $��ConnectButton��$��0��$��3��$��2��$��Connect������ *	Connect+�� -
   $��PlayersListBox��$
��Left=�$��12,118,0,12q�$	��150��� -   $��
BattleGrid��$��168,13,12,12q� �� $��1* ��� $��1* ��� $��1* � �� $��1* ��� $��1* ��� $��1* �.+��� -   $��GameButton0��$��0��$��0��{�.  �� -   $��GameButton1��$��0��$��1��{�.  �� -   $��GameButton2��$��0��$��2��{�.  �� -   $��GameButton3��$��1��$��0��{�.  �� -   $��GameButton4��$��1��$��1��{�.  �� -   $��GameButton5��$��1��$��2��{�.  �� -   $��GameButton6��$��2��$��0��{�.  �� -   $��GameButton7��$��2��$��1��{�.  �� -   $��GameButton8��$��2��$��2��{�.  