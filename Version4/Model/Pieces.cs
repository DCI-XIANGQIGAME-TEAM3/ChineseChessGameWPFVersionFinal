
using Control;

namespace Model
{
    public class ProgramModel
    {

        public Chess[,] setPosition()   //设置棋子位置
        {
            Chess[,] Matrix = new Chess[10, 9];
            for (int col = 0; col < 10; col++)
            {
                for (int row = 0; row < 9; row++)  //initialization
                {
                    Matrix[col, row] = new Chess
                    {
                        side = Chess.Player.blank,
                        type = Chess.Piecetype.blank
                    };
                }
            }
            for (int row = 0; row < 9; row++)
            {
                Matrix[0, row].side = Chess.Player.black; //第一行是黑方
                Matrix[9, row].side = Chess.Player.red; //第九行是红方
                if (row == 1 || row == 7)
                {
                    Matrix[2, row].side = Chess.Player.black; //黑方(的炮)
                    Matrix[7, row].side = Chess.Player.red; //红方(的炮)
                }
                else if (row % 2 == 0)
                {
                    Matrix[3, row].side = Chess.Player.black; //隔一个格放置黑方(的兵)
                    Matrix[6, row].side = Chess.Player.red; //隔一个格放置红方(的兵)
                }
            }
            for (int col = 0; col < 10; col++)
            {
                if (col == 0 || col == 9) //第一行和第九行放置依次放置棋子
                {
                    Matrix[col, 0].type = Chess.Piecetype.che;
                    Matrix[col, 1].type = Chess.Piecetype.ma;
                    Matrix[col, 2].type = Chess.Piecetype.xiang;
                    Matrix[col, 3].type = Chess.Piecetype.shi;
                    Matrix[col, 4].type = Chess.Piecetype.jiang;
                    Matrix[col, 5].type = Chess.Piecetype.shi;
                    Matrix[col, 6].type = Chess.Piecetype.xiang;
                    Matrix[col, 7].type = Chess.Piecetype.ma;
                    Matrix[col, 8].type = Chess.Piecetype.che;
                }
                else if (col == 2 || col == 7) //第二行和第七行放置炮
                {
                    Matrix[col, 1].type = Chess.Piecetype.pao;
                    Matrix[col, 7].type = Chess.Piecetype.pao;
                }
                else if (col == 3 || col == 6) //第三行和第六行依次放置兵
                {
                    for (int row = 0; row < 9; row++)
                    {
                        if (row % 2 == 0)
                        {
                            Matrix[col, row].type = Chess.Piecetype.bing;
                        }
                    }
                }
            }
            return Matrix;
        }
        public Chess[,] SetRoad()          //初始化并设置棋子的路径
        {
            Chess[,] road = new Chess[10, 9];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    road[i, j] = new Chess
                    {
                        path = Chess.Piecepath.not  //初设路径为不可移动，需判断可移动后在赋值为yes
                    };
                }
            }

            return road;
        }
    }
}