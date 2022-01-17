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
        public int OriginalX; //      原来坐标                                              //声明需要的全局变量，与实例化
        public int OriginalY;
        public int currentX;//移动后的坐标
        public int currentY;

        public bool firstClicked=true;//to check if it is the firstclick
        public int nbCount = (int)Chess.Player.red;//回合方
        public int nbCall=0;//回合数
        public Chess[,] chessMatrix;
        public Chess[,] path;
        ProgramModel Mod = new ProgramModel();//实例化model模块
        ProgramControl Con = new ProgramControl();//实例化控制模块

        //主函数
        public mainwindow()
        {
            InitializeComponent();
            BackGroundMusic();// 背景音乐
            chessMatrix = Mod.setPosition();                 //初始化当前的矩阵，即实际的棋盘按钮矩阵
            path = Mod.SetRoad();                           //初始化棋子的路径
            CreateGrid(chessMatrix);                            //打印棋盘与棋子
        }

        public void CreateGrid(Chess[,] currentMatrix)
        {
            Grid MotherGrid = new Grid();                                    //创建背景的boardGrid 1
            this.Content = MotherGrid;
            Grid boardGrid = new Grid();                                     //创建棋盘的boardGrid 1
            MotherGrid.Children.Add(boardGrid);                                  //在totalGrid里添加boardGrid 1
            boardGrid.HorizontalAlignment = HorizontalAlignment.Left;           //使boardGrid打开在左方 1
            WindowStartupLocation = WindowStartupLocation.CenterScreen;         //让窗口在屏幕中央打开 1
            
            for (int i = 0; i < 9; i++)
            {
                boardGrid.ColumnDefinitions.Add(new ColumnDefinition());//列的初始化
            }

            for (int i = 0; i < 10; i++)
            {
                boardGrid.RowDefinitions.Add(new RowDefinition());//行的初设化
            }
            CreateLayout(currentMatrix, boardGrid);                                                //打印布局，每次都要重新布局
        }

        public void CreateLayout(Chess[,] currentMatrix, Grid boardGrid)                        //布局，布置棋子
        {
            Button[,] btn = new Button[10, 9];
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri("Resources\\box.png", UriKind.RelativeOrAbsolute));

            for (int col = 0; col < 10; col++)
            {
                for (int row = 0; row < 9; row++)
                {
                    btn[col, row] = new Button();                                   //设置棋子按钮的各种属性，棋盘中的每个棋子都是一个按钮
                    btn[col, row].Width = 30;
                    btn[col, row].Height = 30;
                    btn[col, row].Margin = new Thickness(11, 3, -1, 3);
                    btn[col, row].BorderThickness = new Thickness(0, 0, 0, 0);
                    btn[col, row].Background = Brushes.Transparent;         //匹配parent（即前一个）的背景
                    btn[col, row] = LoadPieces(currentMatrix, btn[col, row], col, row);//一个一个加载图片
                    btn[col, row].SetValue(Grid.RowProperty, col);//这个我也不清楚，可以网上查查setvalue的作用
                    btn[col, row].SetValue(Grid.ColumnProperty, row);
                    // 画出路径
                    if (path[col, row].path == Chess.Piecepath.yes)
                    {
                        btn[col, row].Background = brush;   //画刷，即使之能画出路径
                    }
                    // 把一个一个按钮加到boardgrid中
                    boardGrid.Children.Add(btn[col, row]);
                }
            }
            ButtonEvent(btn);         //赋予按钮事件      
        }

        public Button LoadPieces(Chess[,] currentMatrix, Button btn, int col, int row)           //加载各个棋子的图片
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

        public void ButtonClick(object sender, RoutedEventArgs e)              //创建点击事件
        {
            int btnRow = (int)((Button)sender).GetValue(Grid.RowProperty);//传递鼠标点击事件，具体可以网上看看getvalue是怎么用的
            int btnCol = (int)((Button)sender).GetValue(Grid.ColumnProperty);
            ClickEvent(btnRow, btnCol);
        }

        public void ButtonEvent(Button[,] btn)              //给每个按钮添加这些点击事件
        {
            for (int col = 0; col < 10; col++)
                for (int row = 0; row < 9; row++)
                    btn[col, row].Click += ButtonClick;
        }

        public void ClickEvent(int btnRow, int btnCol)               //点击发生时进行事件的内容
        {
            bool result;
            try//这里讲的时候特别申明，可能有加分
            {
                if (firstClicked)                                                            //第一次点击即选择棋子
                {
                    OriginalX = btnRow;// 看选中哪个棋子
                    OriginalY = btnCol;
                    if (chessMatrix[OriginalX, OriginalY].side == Chess.Player.blank)
                        MessageBox.Show("There is no piece!");
                    if (chessMatrix[OriginalX, OriginalY].side == Chess.Player.red || chessMatrix[OriginalX, OriginalY].side == Chess.Player.black)
                    {
                        if (nbCount != (int)chessMatrix[OriginalX, OriginalY].side)
                            MessageBox.Show("It's not your turn");//在己方回合选择对方棋子
                        else
                        {
                            path = Con.Road(OriginalX, OriginalY, chessMatrix);           //显示当前棋子可行路径
                            firstClicked = false;
                            CreateGrid(chessMatrix);                              //更新boardGrid
                            path = Mod.SetRoad();                           //初始化路径       
                        }
                    }
                }
                else          //选中了棋子开始选择移动时（第二次点击）
                {
                    currentX = btnRow;//移动后的按钮位置
                    currentY = btnCol;

                    if (currentX == OriginalX && currentY == OriginalY)                 //选中自己
                    {
                        MessageBox.Show("Please try again!");
                        CreateGrid(chessMatrix);                             //更新boardGrid
                    }
                    else                                                        //成功选中
                    {
                        bool move = Con.MovePiece(currentX, currentY, OriginalX, OriginalY, chessMatrix);
                        if (!move) MessageBox.Show("You cannot move to there!");
                        else
                        {
                            nbCall++;//回合数加一
                            CreateGrid(chessMatrix);      //更新棋盘                         
                            result = Con.Result(chessMatrix);                         //检查游戏是否结束
                            if (result)                                    //判断结果，每次点击两次，即有棋子成功移动后就要进行判断，是否游戏已经结束了
                            {   //游戏结束时若是红方回合，即证明胜利方为红方
                                if (nbCount == (int)Chess.Player.red) MessageBox.Show("Red Win!!\n" + "You have used " + nbCall + " rounds to be finished."); //红方赢
                                else MessageBox.Show("Black Win!!\n" + "You have used " + nbCall + " rounds to be finished.");                                         //黑方赢
                                Environment.Exit(0);            //游戏结束自动退出程序
                            }
                            if (nbCount == (int)Chess.Player.red)//每有棋子移动后就要进行回合的交换
                                nbCount = (int)Chess.Player.black;
                            else
                                nbCount = (int)Chess.Player.red;
                        }

                    }
                    firstClicked = true;//第一次点击后成功选中棋子
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
            sp.PlayLooping();      //循坏播放      
        }
    }
}