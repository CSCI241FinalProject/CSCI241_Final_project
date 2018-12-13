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

        //If P is king, need to check the 4 adjacent squares
        if (p.isKing)
        {

        }
        else
        {
            //If p is not the king, then need to check two adjacent squares based on the color.

            if (p.isWhite)
            {
                //if the piece is white, need to go forward, i.e the x and y co-ordinates increase
            }
            else
            {
                //if the piece is black, need to go backward, i.e. the x and y co-ordinates decrease 

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
