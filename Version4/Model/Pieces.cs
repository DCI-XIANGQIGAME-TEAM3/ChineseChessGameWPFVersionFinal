
using Control;

namespace Model
{
    public class ProgramModel
    {

        public Chess[,] setPosition()   //Set chess piece position
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
                Matrix[0, row].side = Chess.Player.black; //The first line is black side
                Matrix[9, row].side = Chess.Player.red; //The ninth line is the Red side
                if (row == 1 || row == 7)
                {
                    Matrix[2, row].side = Chess.Player.black; //black side(set the cannon pieces)
                    Matrix[7, row].side = Chess.Player.red; //red side(set the cannon pieces)
                }
                else if (row % 2 == 0)
                {
                    Matrix[3, row].side = Chess.Player.black; //Place the black side one space apart(set the soldier pieces)
                    Matrix[6, row].side = Chess.Player.red; //Place the red side one space apart(set the soldier pieces)
                }
            }
            for (int col = 0; col < 10; col++)
            {
                if (col == 0 || col == 9) //The first line and the ninth line place chess pieces in turn
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
                else if (col == 2 || col == 7) //Place cannon in the second and seventh rows
                {
                    Matrix[col, 1].type = Chess.Piecetype.pao;
                    Matrix[col, 7].type = Chess.Piecetype.pao;
                }
                else if (col == 3 || col == 6) //The third and sixth rows place soldiers in turn
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
        public Chess[,] SetRoad()          //Initialize and set the path of the chess piece
        {
            Chess[,] road = new Chess[10, 9];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    road[i, j] = new Chess
                    {
                        path = Chess.Piecepath.not  //If the path is initially set to be immovable, it needs to be judged to be movable and then assigned as yes
                    };
                }
            }

            return road;
        }
    }
}
