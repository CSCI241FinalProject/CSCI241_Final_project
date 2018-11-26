using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

    public bool isWhite;
    public bool isKing;

 

    //Function to check if there is a case where the player is forced to make a killing move
    public bool IsForceMovement(Piece[,] board, int x, int y) {

        if (isWhite || isKing)
        {
            //can only kill in two conditions

            //going top left
            if (x >= 2 && y <= 5)
            {
                Piece toDie = board[x - 1, y + 1];
                //if there is a piece here and is not the same color, kill it   
                if ((toDie != null) && (toDie.isWhite != isWhite))
                {
                    //Check if the end position is empty or not 
                    if (board[x - 2, y + 2] == null)
                    {
                        return true;
                    }
                }

            }

            //going top right
            if (x <= 5 && y <= 5)
            {
                Piece toDie = board[x + 1, y + 1];
                //if there is a piece here and is not the same color, kill it   
                if ((toDie != null) && (toDie.isWhite != isWhite))
                {
                    //Check if the end position is empty or not 
                    if (board[x + 2, y + 2] == null)
                    {
                        return true;
                    }
                }

            }


        }
        if(!isWhite || isKing) {
            //going bottom left
            if ( (x >= 2) && (y >= 2) )
            {
                Piece toDie = board[x - 1, y - 1];
                //if there is a piece here and is not the same color, kill it   
                if ((toDie != null) && (toDie.isWhite != isWhite))
                {
                    //Check if the end position is empty or not 
                    if (board[x - 2, y - 2] == null)
                    {
                        return true;
                    }
                }

            }

            //going bottom right
            if ( (x <= 5) && (y >= 2) )
            {
                Piece toDie = board[x + 1, y - 1];
                //if there is a piece here and is not the same color, kill it   
                if ((toDie != null) && (toDie.isWhite != isWhite))
                {
                    //Check if the end position is empty or not 
                    if (board[x + 2, y - 2] == null)
                    {
                        return true;
                    }
                }

            }

        }
        return false;
    }


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
