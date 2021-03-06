﻿using UnityEngine;
using UnityEditor;



public class FPiece : ScriptableObject
{
    public int x;
    public int y;

    public bool isWhite;
    public bool isKing;

    public FPiece()
    {
        this.x = 0;
        this.y = 0;
    }


    public FPiece(int xx, int yy)
    {
        x = xx;
        y = yy;
    }

    public FPiece(int xx, int yy, bool white, bool king)
    {
        x = xx;
        y = yy;
        isKing = king;
        isWhite = white;
    }

    //Function to check if there is a case where the player is forced to make a killing move
    public bool IsForceMovement(FPiece[,] board, int x, int y)
    {

        if (isWhite || isKing)
        {
            //can only kill in two conditions

            //going top left
            if (x >= 2 && y <= 5)
            {
                FPiece toDie = board[x - 1, y + 1];
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
                FPiece toDie = board[x + 1, y + 1];
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
        if (!isWhite || isKing)
        {
            //going bottom left
            if ((x >= 2) && (y >= 2))
            {
                FPiece toDie = board[x - 1, y - 1];
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
            if ((x <= 5) && (y >= 2))
            {
                FPiece toDie = board[x + 1, y - 1];
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


    public bool CheckMoveValidation(FPiece[,] board, int x1, int y1, int x2, int y2)
    {

        //check index out of bounds
        if ((x2 < 0) || (x2 > 7) || (y2 < 0) || (y2 > 7))
        {
            return false;
        }


        //Check if you are moving on top of another piece
        if (board[x2, y2] != null)
        {
            return false;
        }

        int moveLengthX = Mathf.Abs(x1 - x2);
        int moveLengthY = (y2 - y1);
        //If the piece is white or is king
        if (isWhite || isKing)
        {

            if (moveLengthX == 1)
            {
                //normal jump
                if (moveLengthY == 1)
                {
                    return true;
                }
            }
            else if (moveLengthX == 2)
            {
                //Kill jump
                if (moveLengthY == 2)
                {
                    FPiece dead = board[((x1 + x2) / 2), ((y1 + y2) / 2)]; //position of dead piece

                    //If there is a 'dead' piece and is not of the same color as the moving piece
                    if ((dead != null) && (dead.isWhite != isWhite))
                    {
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
                    FPiece dead = board[((x1 + x2) / 2), ((y1 + y2) / 2)]; //position of dead piece

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
