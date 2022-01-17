using System;
using Model;


namespace Control
{
    class Horse : Chess
    {
        public override bool ChessMovingRule(int CurrentX, int CurrentY, int OriginalX, int OriginalY, Chess[,] Matrix)
        {
            //ProgramControl con = new ProgramControl();
            //横着走(曰)
            Boolean YMoving = Math.Abs(OriginalX - CurrentX) == 1 && Math.Abs(OriginalY - CurrentY) == 2; //if it satisfied then it is true or will be false
            //竖着走（日）
            Boolean XMoving = Math.Abs(OriginalX - CurrentX) == 2 && Math.Abs(OriginalY - CurrentY) == 1; //if it satisfied then it is true or will be false

            switch (XMoving || YMoving)
            {
                case true:
                    if (XMoving)
                    {
                        if (Matrix[(OriginalX + CurrentX) / 2, OriginalY].side != Chess.Player.blank) //有棋子挡住时不能走
                        {
                            return false;
                        }
                    }
                    if (YMoving)
                    {
                        if (Matrix[OriginalX, (OriginalY + CurrentY) / 2].side != Chess.Player.blank) //有棋子挡住时不能走
                        {
                            return false;
                        }
                    }
                    break;
                case false:
                    return false;
            }

            //不能吃掉自己方的棋子
            if (Matrix[OriginalX, OriginalY].side == Matrix[CurrentX, CurrentY].side)
            {
                return false;
            }

            ProgramControl.SetMove(CurrentX, CurrentY, OriginalX, OriginalY, Matrix);

            return true;
        }

    }
}