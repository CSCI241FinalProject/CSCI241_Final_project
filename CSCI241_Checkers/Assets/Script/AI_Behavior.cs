using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Behavior : CheckersBoard
{


    //Define the offensive points 
    readonly int CAPTUREPIECE = 2;
    readonly int CAPTUREKING = 1;
    readonly int CAPTUREDOUBLE = 5;
    readonly int CAPTUREMULTI = 10;
    readonly int MAKEKING = 1;

    //Define defensive points 

    readonly int ATRISK = 3;
    readonly int KINGATRISK = 4;










    //Function that returns all the black pieces on the board as a list
    public List<Piece> GetBlackPieces(Piece[,] board)
    {
        List<Piece> result = new List<Piece>();

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (!board[x, y].isWhite)
                {
                    result.Add(board[x, y]);
                }
            }
        }
        return result;
    }









    //Function that returns all the white pieces on the board as a list
    public List<Piece> GetWhitePieces(Piece[,] board)
    {
        List<Piece> result = new List<Piece>();

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (board[x, y].isWhite)
                {
                    result.Add(board[x, y]);
                }
            }
        }
        return result;
    }










    //Function that copies the board state passed in and returns new board
    private Piece[,] CopyBoard(Piece[,] board)
    {
        Piece[,] result = new Piece[8, 8];

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                result[x, y] = board[x, y];

            }
        }
        return result;
    }









    //Function to get a list of all the possible moves in the passed in board state for the piece 'p'
    private List<Piece> GetPossibleMoves(Piece p, Piece[,] board)
    {
        List<Piece> result = new List<Piece>();


        //First of all, check if the piece is being forced to move.
        if (p.IsForceMovement(board, p.x, p.y))
        {

            if (p.isWhite || p.isKing)
            {
                //can only kill in two conditions

                //going top left
                if (p.CheckMoveValidation(board, p.x, p.y, p.x - 2, p.y + 2))
                {
                    Piece toDieTL = board[p.x - 1, p.y + 1];
                    //if there is a piece here and is not the same color, kill it   
                    if ((toDieTL != null) && (toDieTL.isWhite != p.isWhite))
                    {
                        //Check if the end position is empty or not 
                        if (board[p.x - 2, p.y + 2] == null)
                        {
                            //create a ghost piece and add to result
                            Piece n = new Piece(p.x - 2, p.y + 2);
                            result.Add(n);
                        }
                    }
                }

                //going top right
                if (p.CheckMoveValidation(board, p.x, p.y, p.x + 2, p.y + 2))
                {
                    Piece toDieTR = board[p.x + 1, p.y + 1];
                    //if there is a piece here and is not the same color, kill it   
                    if ((toDieTR != null) && (toDieTR.isWhite != p.isWhite))
                    {
                        //Check if the end position is empty or not 
                        if (board[p.x + 2, p.y + 2] == null)
                        {
                            //Create a ghost piece and add to result 
                            Piece n = new Piece(p.x + 2, p.y + 2);
                            result.Add(n);
                        }
                    }
                }




            }

            if (!p.isWhite || p.isKing)
            {
                //going bottom left
                if (p.CheckMoveValidation(board, p.x, p.y, p.x - 2, p.y - 2))
                {
                    Piece toDieBL = board[p.x - 1, p.y - 1];
                    //if there is a piece here and is not the same color, kill it   
                    if ((toDieBL != null) && (toDieBL.isWhite != p.isWhite))
                    {
                        //Check if the end position is empty or not 
                        if (board[p.x - 2, p.y - 2] == null)
                        {
                            //Create a ghost piece and add to result 
                            Piece n = new Piece(p.x - 2, p.y - 2);
                            result.Add(n);
                        }
                    }
                }


                //going bottom right
                if (p.CheckMoveValidation(board, p.x, p.y, p.x + 2, p.y - 2))
                {
                    Piece toDieBR = board[p.x + 1, p.y - 1];
                    //if there is a piece here and is not the same color, kill it   
                    if ((toDieBR != null) && (toDieBR.isWhite != p.isWhite))
                    {
                        //Check if the end position is empty or not 
                        if (board[p.x + 2, p.y - 2] == null)
                        {
                            //Create a ghost piece and add to result
                            Piece n = new Piece(p.x + 2, p.y - 2);
                            result.Add(n);
                        }
                    }
                }
            }

            return result;
        }


        //If its NOT forced movement
        if (p.isKing)
        {
            //Checking bottom left
            if (p.CheckMoveValidation(board, p.x, p.y, p.x - 1, p.y - 1))
            {
                Piece n = new Piece(p.x - 1, p.y - 1);
                result.Add(n);
            }

            //Checking top left
            if (p.CheckMoveValidation(board, p.x, p.y, p.x - 1, p.y + 1))
            {
                Piece n = new Piece(p.x - 1, p.y + 1);
                result.Add(n);
            }

            //checking top right
            if (p.CheckMoveValidation(board, p.x, p.y, p.x + 1, p.y + 1))
            {
                Piece n = new Piece(p.x + 1, p.y + 1);
                result.Add(n);
            }

            //checking bottom right
            if (p.CheckMoveValidation(board, p.x, p.y, p.x + 1, p.y - 1))
            {
                Piece n = new Piece(p.x + 1, p.y - 1);
                result.Add(n);
            }
        }

        else
        {
            //If p is not the king, then need to check two adjacent squares based on the color.

            if (p.isWhite)
            {
                //if the piece is white, need to go forward, i.e the y co-ordinates increase
                if (p.CheckMoveValidation(board, p.x, p.y, p.x - 1, p.y + 1))
                {
                    Piece n = new Piece(p.x - 1, p.y + 1);
                    result.Add(n);
                }
                if (p.CheckMoveValidation(board, p.x, p.y, p.x + 1, p.y + 1))
                {
                    Piece n = new Piece(p.x + 1, p.y + 1);
                    result.Add(n);
                }
            }
            else
            {
                //if the piece is black, need to go backward, i.e. the y co-ordinates decrease 
                if (p.CheckMoveValidation(board, p.x, p.y, p.x - 1, p.y - 1))
                {
                    Piece n = new Piece(p.x - 1, p.y - 1);
                    result.Add(n);
                }
                if (p.CheckMoveValidation(board, p.y, p.y, p.x + 1, p.y - 1))
                {
                    Piece n = new Piece(p.x + 1, p.y - 1);
                    result.Add(n);
                }

            }
        }
        return result;
    }








    //Function to carry out a virtual move in the virtual board 
    private Piece[,] MakeVirtualMove(Piece source, Piece dest, Piece[,] board)
    {

        //Make a copy of the board that has been passed in
        Piece[,] newBoard = CopyBoard(board);

        //Check if the move is valid 
        if (source.CheckMoveValidation(newBoard, source.x, source.y, dest.x, dest.y))
        {

            //If the move is valid, carry it out in the virtual board
            //source co-ordiantes
            int x1 = source.x;
            int y1 = source.y;
            //dest coordinates
            int x2 = dest.x;
            int y2 = dest.y;

            //Check if the move is a killmove 
            if (Mathf.Abs(x1 - x2) == 2)
            {
                Piece dead = newBoard[((x1 + x2) / 2), ((y1 + y2) / 2)]; //position of dead piece
                if (dead != null)
                {
                    boardPieces[((x1 + x2) / 2), ((y1 + y2) / 2)] = null;
                }
            }

            //move the piece
            newBoard[x2, y2] = source;
            newBoard[x1, y1] = null;

            //Update the x and y vaules of the source piece
            source.x = x2;
            source.y = y2;
        }
        return newBoard;
    }









    //AI Algorithm. 
    /*
     * Generate a list of all the possible pieces of the given color.
     * For each piece in the color generate list of all possible moves (check here if there is forced movement, function is in CheckersBoard) 
     *      choose the best move for this current piece into a list of best moves for each piece. Linked list? With each node having source, destination and finalscore? 
     *                  Might have to write a new class LinkedList. Not too complicated I think. 
     * Once you have the best move from the list of best moves in the current board state. Create a virtual move for the BEST move from the list of best moves. Pass in virtual board state
     * Repeat the algorithm (for the opposite color) until reached max depth or won game. 
     *      
     *      Return the best move from the final list of best moves. 
     */


    //Public Piece ChildMoves(Piece iniOrg, Piece curOrg, Piece finDest, Pieces[,] board, int depth){}


    //Public Piece MinMax(){}
    //Makes call to childmoves. Childmoves is the recursive shit. 
}
