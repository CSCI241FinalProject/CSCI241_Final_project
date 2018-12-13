using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Behavior : CheckersBoard {


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
    public List<Piece> GetBlackPieces(Piece [,] board)
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

        for (int x = 0; x < 8; x++) {
            for (int y = 0; y < 8; y++) {
                result[x, y] = board[x,y]; 

            }
        }
        return result; 
    }

    //Evaluate all possible moves and store it.
    // private List<Piece> PossibleMoves (Pieces [,] board, Piece p)
    //The piece list are just empty pieces with x and y coordinates set to destinations 


    //Write a function to evaluate the best move (The score) of the a piece 
    //private Piece BestMoveForPiece (Piece p, List<Piece> destinations)


    //Write a function to carry out the virtual move 
    //private Piece[,] VirtualMove ( Piece[,] board, int x1, int x2, int y1, int y2)



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
