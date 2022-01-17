using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Model;
using Control;
using System.Media;

namespace version4.UserInterface
{
    public partial class mainwindow : Window
    {
        public int OriginalX; // These two variables are the original coordinate of a chosen chess                                             
        public int OriginalY;
        public int currentX;// These two variables are the goal coordinate to move to of a chosen chess
        public int currentY;

        public bool firstClicked=true;//to check if it is the firstclick,is used for telling whether the click aims to choose a chess to move or to move a chosen chess
        public int nbCount = (int)Chess.Player.red;// To tell whether it's red side turn or black side turn
        public int nbCall=0;// The number of round(how many moves have been set)
        public Chess[,] chessMatrix;
        public Chess[,] path;
        ProgramModel Mod = new ProgramModel();// instantiate the model module
        ProgramControl Con = new ProgramControl();// instantiate the control module

        public mainwindow()
        {
            InitializeComponent();
            BackGroundMusic();// 背景音乐
            chessMatrix = Mod.setPosition();                 // initialize the chess board matrix
            path = Mod.SetRoad();                           // initialize the path of the chess
            CreateGrid(chessMatrix);                            // Print out the chess and chess board 
        }

        public void CreateGrid(Chess[,] currentMatrix)
        {
            Grid MotherGrid = new Grid();                                    // create the background grid
            this.Content = MotherGrid;
            Grid boardGrid = new Grid();                                     // create the chess board grid
            MotherGrid.Children.Add(boardGrid);                                  
            boardGrid.HorizontalAlignment = HorizontalAlignment.Left;           // set the position of the chess board grid
            WindowStartupLocation = WindowStartupLocation.CenterScreen;         
            
            for (int i = 0; i < 9; i++)
            {
                boardGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < 10; i++)
            {
                boardGrid.RowDefinitions.Add(new RowDefinition());
            }
            CreateLayout(currentMatrix, boardGrid);                                                // print out the layout, declared below
        }

        public void CreateLayout(Chess[,] currentMatrix, Grid boardGrid)                        // called above
        {
            Button[,] btn = new Button[10, 9];
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri("Resources\\box.png", UriKind.RelativeOrAbsolute));

            for (int col = 0; col < 10; col++)
            {
                for (int row = 0; row < 9; row++)
                {
                    btn[col, row] = new Button();                                   // set the attributes of each chess and set them as buttons
                    btn[col, row].Width = 30;
                    btn[col, row].Height = 30;
                    btn[col, row].Margin = new Thickness(11, 3, -1, 3);
                    btn[col, row].BorderThickness = new Thickness(0, 0, 0, 0);
                    btn[col, row].Background = Brushes.Transparent;         //匹配parent（即前一个）的背景
                    btn[col, row] = LoadPieces(currentMatrix, btn[col, row], col, row);//一个一个加载图片
                    btn[col, row].SetValue(Grid.RowProperty, col);
                    btn[col, row].SetValue(Grid.ColumnProperty, row);
                    // draw the path of where the chosen chess can move to
                    if (path[col, row].path == Chess.Piecepath.yes)
                    {
                        btn[col, row].Background = brush;   
                    }
                    // add the button on the grid
                    boardGrid.Children.Add(btn[col, row]);
                }
            }
            ButtonEvent(btn);         // give the button events     
        }

        public Button LoadPieces(Chess[,] currentMatrix, Button btn, int col, int row)           // To load the images of the chesses
        {
            ImageBrush chessImage = new ImageBrush();
            switch (currentMatrix[col, row].side)
            {
                case Chess.Player.red:
                    switch (currentMatrix[col, row].type)
                    {
                        case Chess.Piecetype.che:
                            chessImage.ImageSource = new BitmapImage(new Uri("Resources\\redRook.png", UriKind.RelativeOrAbsolute));
                            break;
                        case Chess.Piecetype.ma:
                            chessImage.ImageSource = new BitmapImage(new Uri("Resources\\redHorse.png", UriKind.RelativeOrAbsolute));
                            break;
                        case Chess.Piecetype.xiang:
                            chessImage.ImageSource = new BitmapImage(new Uri("Resources\\redElephant.png", UriKind.RelativeOrAbsolute));
                            break;
                        case Chess.Piecetype.shi:
                            chessImage.ImageSource = new BitmapImage(new Uri("Resources\\redAdvisor.png", UriKind.RelativeOrAbsolute));
                            break;
                        case Chess.Piecetype.jiang:
                            chessImage.ImageSource = new BitmapImage(new Uri("Resources\\redGeneral.png", UriKind.RelativeOrAbsolute));
                            break;
                        case Chess.Piecetype.pao:
                            chessImage.ImageSource = new BitmapImage(new Uri("Resources\\redCannon.png", UriKind.RelativeOrAbsolute));
                            break;
                        case Chess.Piecetype.bing:
                            chessImage.ImageSource = new BitmapImage(new Uri("Resources\\redSoldier.png", UriKind.RelativeOrAbsolute));
                            break;
                    }
                    btn.Background = chessImage;
                    return btn;

                case Chess.Player.black:
                    switch (currentMatrix[col, row].type)
                    {
                        case Chess.Piecetype.che:
                            chessImage.ImageSource = new BitmapImage(new Uri("Resources\\blackRook.png", UriKind.RelativeOrAbsolute));
                            break;
                        case Chess.Piecetype.ma:
                            chessImage.ImageSource = new BitmapImage(new Uri("Resources\\blackHorse.png", UriKind.RelativeOrAbsolute));
                            break;
                        case Chess.Piecetype.xiang:
                            chessImage.ImageSource = new BitmapImage(new Uri("Resources\\blackElephant.png", UriKind.RelativeOrAbsolute));
                            break;
                        case Chess.Piecetype.shi:
                            chessImage.ImageSource = new BitmapImage(new Uri("Resources\\blackAdvisor.png", UriKind.RelativeOrAbsolute));
                            break;
                        case Chess.Piecetype.jiang:
                            chessImage.ImageSource = new BitmapImage(new Uri("Resources\\blackGeneral.png", UriKind.RelativeOrAbsolute));
                            break;
                        case Chess.Piecetype.pao:
                            chessImage.ImageSource = new BitmapImage(new Uri("Resources\\blackCannon.png", UriKind.RelativeOrAbsolute));
                            break;
                        case Chess.Piecetype.bing:
                            chessImage.ImageSource = new BitmapImage(new Uri("Resources\\blackSoldier.png", UriKind.RelativeOrAbsolute));
                            break;
                    }
                    btn.Background = chessImage;
                    return btn;
            }
            return btn;
        }

        public void ButtonClick(object sender, RoutedEventArgs e)              // create the click event
        {
            int btnRow = (int)((Button)sender).GetValue(Grid.RowProperty);        // pass the event by button
            int btnCol = (int)((Button)sender).GetValue(Grid.ColumnProperty);
            ClickEvent(btnRow, btnCol);
        }

        public void ButtonEvent(Button[,] btn)              // add event for the buttons
        {
            for (int col = 0; col < 10; col++)
                for (int row = 0; row < 9; row++)
                    btn[col, row].Click += ButtonClick;
        }

        public void ClickEvent(int btnRow, int btnCol)               // content of the event(what happen when click the button)
        {
            bool result;
            try//这里讲的时候特别申明
            {
                if (firstClicked)   // to choose a chess to move
                {
                    OriginalX = btnRow;  // to tell which chess is chosen
                    OriginalY = btnCol;
                    if (chessMatrix[OriginalX, OriginalY].side == Chess.Player.blank)
                        MessageBox.Show("There is no piece!");
                    if (chessMatrix[OriginalX, OriginalY].side == Chess.Player.red || chessMatrix[OriginalX, OriginalY].side == Chess.Player.black)
                    {
                        if (nbCount != (int)chessMatrix[OriginalX, OriginalY].side)
                            MessageBox.Show("It's not your turn");
                        else
                        {
                            path = Con.Road(OriginalX, OriginalY, chessMatrix);           // path is where the chosen chess can move to
                            firstClicked = false;
                            CreateGrid(chessMatrix);                              // update the chess board grid after choosing the chess
                            path = Mod.SetRoad();                           // set the path as initialiazational value 
                        }
                    }
                }
                else          // to move a chosen chess
                {
                    currentX = btnRow;  // the goal coordinate where the chess will move to
                    currentY = btnCol;

                    if (currentX == OriginalX && currentY == OriginalY)                 // if choose the original position
                    {
                        MessageBox.Show("Please try again!");
                        CreateGrid(chessMatrix);                             // update the chess board grid
                    }
                    else                                                        // sucessful move 
                    {
                        bool move = Con.MovePiece(currentX, currentY, OriginalX, OriginalY, chessMatrix);
                        if (!move) MessageBox.Show("You cannot move to there!");
                        else
                        {
                            nbCall++;// the number of round should plus 1
                            CreateGrid(chessMatrix);      // update the chess board grid after moving the chess                         
                            result = Con.Result(chessMatrix);                         
                            if (result)                                    // to check if the game is over after every two clicks
                            {   // when the game is over, the turn owner is winner
                                if (nbCount == (int)Chess.Player.red) MessageBox.Show("Red Win!!\n" + "You have used " + nbCall + " rounds to be finished."); // red wins
                                else MessageBox.Show("Black Win!!\n" + "You have used " + nbCall + " rounds to be finished.");                                // balck wins
                                Environment.Exit(0);            // game over and exit
                            }
                            if (nbCount == (int)Chess.Player.red) // exchange the turn between red side and black side
                                nbCount = (int)Chess.Player.black;
                            else
                                nbCount = (int)Chess.Player.red;
                        }

                    }
                    firstClicked = true; // if the first click is sucessful(choose a chess to move)
                }
            }
            catch (OverflowException)
            {
                MessageBox.Show("OverflowException");
            }
            
        }
        public void BackGroundMusic()
        {
            SoundPlayer sp = new SoundPlayer();
            sp.SoundLocation = "backgroundmusic\\LittleXiangQI.wav";
            sp.PlayLooping();      // to play the music over and over again    
        }
    }
}
