using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Behavior : CheckersBoard
{
    //Define recursive depth
    readonly int DEPTH = 4; 

    //Define the offensive points 
    readonly int CAPTUREPIECE = 2;
    readonly int CAPTUREKING = 1;
    readonly int CAPTUREDOUBLE = 5;
    readonly int CAPTUREMULTI = 10;
    readonly int MAKEKING = 1;

    //Define defensive points 
    readonly int ATRISK = -3;
    readonly int KINGATRISK = -4;



    //Function that returns all the black pieces on the board as a list
    public List<Piece> GetBlackPieces(Piece[,] board)
    {
        List<Piece> result = new List<Piece>();

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if ((board[x,y] != null) &&(!board[x, y].isWhite))
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
                if ((board[x, y] != null) && (board[x, y].isWhite))
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
    private Move MakeVirtualMove(Piece source, Piece dest, Move thisMove)
    {
        //TODO make score calculation here.
        int newScore = 0; 


        //Make a copy of the board that has been passed in
        Piece[,] newBoard = thisMove.GetBoard();

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


        Move result = new Move(thisMove.GetOrigin(), thisMove.GetDestination(), newBoard);
        result.score = newScore; 
        return result;
    }








    /*****************************************************************************************************************/
    /*********************************AI PORTION ALERT!**************************************************************/






    List<Move> finalList = new List<Move>(); //global variable to store the end board states of the minimax algorithm
    //function to get the best move from a list of Moves
    private Move GetBestMove(List<Move> list)
    {

        Move result = new Move();

        //loop through the list of final board states and return the move with the max score 
        foreach (Move move in list)
        {
            if (move.score > result.score)
            {
                result = move;
            }
        }
        return result;
    }


    private void ChildMove(Move move, bool isWhite, int depth) {

      

        //base case. add all the children moves into the global variable and return  
        if (depth <= 0) {
            finalList.Add(move);
            return;
        }


        //For each board state present in move.children
        foreach (Move Mover in move.GetChildren())
        {

            Piece[,] board = Mover.GetBoard(); 

            //Create a list of pieces of current color
            List<Piece> pieces = new List<Piece>();
            if (isWhite)
            {
                pieces = GetWhitePieces(board);
            }
            else
            {
                pieces = GetBlackPieces(board);
            }


            List<Move> newList = new List<Move>(); //create new list to set the children of each of this current move


            //for each piece of the given color
            foreach (Piece origin in pieces)
            { 
                //Generate a list of all possible moves
                List<Piece> destinations = GetPossibleMoves(origin, board);

                //Once you have a list of all possible moves. Carry out the virtual move on the board
                foreach (Piece destination in destinations)
                {
                    //Make virtual move here and call the recursive function on the following: 
                    Move newMove = MakeVirtualMove(origin, destination, Mover);
                    newList.Add(newMove);
                }
            }
            Mover.SetChildren(newList);

            //call recursive function on each move in newList with depth-1, oppposite color.
            foreach (Move n in newList) {
                ChildMove(n, !isWhite, depth--);
            }
        }

    }




    public int[] GetMove(Piece[,] board) {
        int[] result = new int[4];

        List<Piece> blackPieces = GetBlackPieces(board); //get all black pieces on the board
        List<Move> moves = new List<Move>();//list to store the list of moves

        //For each black piece on the board
        foreach (Piece piece in blackPieces) {

            //get a list of all possible destinations
            List<Piece> destinations = GetPossibleMoves(piece, board);

            //for each destination
            foreach (Piece destination in destinations) {

                //make a new move and carry out a virual move on that board 
                Move newMove = new Move(piece, destination, board);
                Move resultantMove = MakeVirtualMove(piece, destination, newMove);
                moves.Add(resultantMove); //add the virtual move into the board
            }
        }

        //now call the recursive function on each of the child moves carried out 
        foreach (Move thismove in moves) {
            ChildMove(thismove, true, 3); 
        }


        //do the resetting, and returning here

        Move finalMove = GetBestMove(finalList); //getting the best move among the list of moves
        finalList = new List<Move>(); //resetting the global variable 

        result[0] = finalMove.GetOrigin().x;
        result[1] = finalMove.GetOrigin().y;
        result[2] = finalMove.GetDestination().x;
        result[3] = finalMove.GetDestination().y; 

        return result; 
    }


}
