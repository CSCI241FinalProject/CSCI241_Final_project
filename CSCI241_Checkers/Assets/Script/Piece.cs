using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

    public bool isWhite;
    public bool isKing;


    public bool CheckMoveValidation(Piece[,] board, int x1, int y1, int x2, int y2) {

        //Check if you are moving on top of another piece
        if (board[x2, y2] != null) {
            return false;
        }


        int moveLengthX = Mathf.Abs(x1 - x2);
        int moveLengthY = (y2-y1);
        //If the piece is white or is king
        if (isWhite || isKing) {

            if (moveLengthX == 1)
            {
                //normal jump
                if (moveLengthY == 1) {
                    return true;
                }
            }
            else if (moveLengthX == 2) {
                //Kill jump
                if (moveLengthY == 2) {
                    Piece dead = board[ ((x1+x2)/2) , ((y1+y2)/2) ]; //position of dead piece

                    //If there is a 'dead' piece and is not of the same color as the moving piece
                    if ( (dead != null) && (dead.isWhite != isWhite)) {
                        return true;
                    }
                }
            }
        }


        //If it is balck or isKing
        if (!isWhite || isKing)
        {

            if (moveLengthX == 1)
            {
                //normal jump
                if (moveLengthY == -1)
                {
                    return true;
                }
            }
            else if (moveLengthX == 2)
            {
                //Kill jump
                if (moveLengthY == -2)
                {
                    Piece dead = board[((x1 + x2) / 2), ((y1 + y2) / 2)]; //position of dead piece

                    //If there is a 'dead' piece and is not of the same color as the moving piece
                    if ((dead != null) && (dead.isWhite != isWhite))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

}
