using System;
using Model;


namespace Control
{
    public class ProgramControl
    {
        public bool Result(Chess[,] Matrix)
        {
            int count = 0;
            //traverse the upper grid where the piece general can move
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 3; j <= 5; j++)
                {
                    if (Matrix[i, j].type == Chess.Piecetype.jiang)
                        count++;
                }
            }
            //traverse the lower grid where the piece general can move
            for (int i = 7; i <= 9; i++)
            {
                for (int j = 3; j <= 5; j++)
                {
                    if (Matrix[i, j].type == Chess.Piecetype.jiang)
                        count++;
                }
            }
            if (count == 2)
                return false;
            else
                return true;
        }


        public bool MovePiece(int CurrentX, int CurrentY, int OriginalX, int OriginalY, Chess[,] Matrix)
        //define the rules of how each specific piece moves
        {
            Advisor advisor = new();
            Cannon cannon = new();
            Elephant elephant = new();
            General general = new();
            Horse horse = new();
            Chariot chariot = new();
            Soldier soldier = new();

            bool Move;

            switch (Matrix[OriginalX, OriginalY].type)
            {
                case Chess.Piecetype.che:
                    Move = chariot.ChessMovingRule(CurrentX, CurrentY, OriginalX, OriginalY, Matrix);
                    return Move;
                case Chess.Piecetype.ma:
                    Move = horse.ChessMovingRule(CurrentX, CurrentY, OriginalX, OriginalY, Matrix);
                    return Move;
                case Chess.Piecetype.xiang:
                    Move = elephant.ChessMovingRule(CurrentX, CurrentY, OriginalX, OriginalY, Matrix);
                    return Move;
                case Chess.Piecetype.shi:
                    Move = advisor.ChessMovingRule(CurrentX, CurrentY, OriginalX, OriginalY, Matrix);
                    return Move;
                case Chess.Piecetype.jiang:
                    Move = general.ChessMovingRule(CurrentX, CurrentY, OriginalX, OriginalY, Matrix);
                    return Move;
                case Chess.Piecetype.pao:
                    Move = cannon.ChessMovingRule(CurrentX, CurrentY, OriginalX, OriginalY, Matrix);
                    return Move;
                case Chess.Piecetype.bing:
                    Move = soldier.ChessMovingRule(CurrentX, CurrentY, OriginalX, OriginalY, Matrix);
                    return Move;
            }

            return false;
        }


        public Chess[,] Road(int chozenX, int chozenY, Chess[,] Matrix)
        {
            ProgramModel mod = new();                  
            Chess[,] road = mod.SetRoad();            
            Chess[,] trans = new Chess[10, 9];         
            bool feasibleRoad;

            for (int col = 0; col < 10; col++)                
            {
                for (int row = 0; row < 9; row++)
                {
                    trans[col, row] = new Chess();            
                }
            }

            for (int col = 0; col < 10; col++) {           
                for (int row = 0; row < 9; row++)
                {
                    trans[col, row].side = Matrix[col, row].side;           
                    trans[col, row].type = Matrix[col, row].type;              
                    trans[chozenX, chozenY].side = Matrix[chozenX, chozenY].side;      
                    trans[chozenX, chozenY].type = Matrix[chozenX, chozenY].type;      
                    feasibleRoad = MovePiece(col, row, chozenX, chozenY, Matrix);                 

                    if (feasibleRoad)
                    {
                        road[col, row].path = Chess.Piecepath.yes;      
                    }

                    Matrix[col, row].side = trans[col, row].side;           
                    Matrix[col, row].type = trans[col, row].type;
                    Matrix[chozenX, chozenY].side = trans[chozenX, chozenY].side;
                    Matrix[chozenX, chozenY].type = trans[chozenX, chozenY].type;

                }
            }

            return road;
        }

        public static void SetMove(int CurrentX, int CurrentY, int OriginalX, int OriginalY, Chess[,] Matrix)
        //basic rules of movements
        {
            Matrix[CurrentX, CurrentY].side = Matrix[OriginalX, OriginalY].side;
            Matrix[CurrentX, CurrentY].type = Matrix[OriginalX, OriginalY].type;   //eat piece
            Matrix[OriginalX, OriginalY].side = Chess.Player.blank;
            Matrix[OriginalX, OriginalY].type = Chess.Piecetype.blank;  //put the piece on the empty grid
        }


    }

}
