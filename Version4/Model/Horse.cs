using System;
using Model;


namespace Control
{
    class Horse : Chess
    {
        public override bool ChessMovingRule(int CurrentX, int CurrentY, int OriginalX, int OriginalY, Chess[,] Matrix)
        {
            //Walk Horizontally
            Boolean YMoving = Math.Abs(OriginalX - CurrentX) == 1 && Math.Abs(OriginalY - CurrentY) == 2; //if it satisfied then it is true or will be false
            //Walk Vertically
            Boolean XMoving = Math.Abs(OriginalX - CurrentX) == 2 && Math.Abs(OriginalY - CurrentY) == 1; //if it satisfied then it is true or will be false

            switch (XMoving || YMoving)
            {
                case true:
                    if (XMoving)
                    {
                        if (Matrix[(OriginalX + CurrentX) / 2, OriginalY].side != Chess.Player.blank) //You can't walk when there are chess pieces in the way
                        {
                            return false;
                        }
                    }
                    if (YMoving)
                    {
                        if (Matrix[OriginalX, (OriginalY + CurrentY) / 2].side != Chess.Player.blank) //You can't walk when there are chess pieces in the way
                        {
                            return false;
                        }
                    }
                    break;
                case false:
                    return false;
            }

            //can't eat your own chess pieces
            if (Matrix[OriginalX, OriginalY].side == Matrix[CurrentX, CurrentY].side)
            {
                return false;
            }

            ProgramControl.SetMove(CurrentX, CurrentY, OriginalX, OriginalY, Matrix);

            return true;
        }

    }
}
