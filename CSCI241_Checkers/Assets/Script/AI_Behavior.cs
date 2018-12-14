using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
    public List<FPiece> GetBlackPieces(FPiece[,] board)
    {
        List<FPiece> result = new List<FPiece>();

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if ((board[x, y] != null) && (board[x, y].isWhite == false))
                {
                    result.Add(board[x, y]);
                }
            }
        }


        Debug.Log("0000000000000000000000000000000000000000000000000000000000000000000000000000000000");
        /*debug*/
        Debug.Log("Black pieces:");
        foreach (FPiece x in result)
        {
            Debug.Log(x.x);
            Debug.Log(x.y);
            Debug.Log(" ");
        }
        Debug.Log("0000000000000000000000000000000000000000000000000000000000000000000000000000000000");

        return result;
    }







    //Function that copies the original board and creates a copy in Fpiece
    public FPiece[,] CopyMainBoard(Piece[,] board)
    {

        print("-----------------------------------------------------------");
        print("Copying Board");

        FPiece[,] result = new FPiece[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                result[i, j] = null;
            }
        }


        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (board[x, y] != null)
                {

                    //print("Getting here");
                    FPiece current = new FPiece
                    {
                        x = x,
                        y = y,
                        isKing = board[x, y].isKing,
                        isWhite = board[x, y].isWhite
                    };
                    result[x, y] = current;

                }
            }
        }


        /*debug*/
        print("-----------------------------------------------------------");
        print("Board Copied");
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (result[i, j] != null)
                {
                    print(result[i, j].x);
                    print(result[i, j].y);
                    print(" ");
                }
                else
                {
                    print("null");
                    print(" ");
                }


            }
        }
        



        return result;
    }




    //Function that creates a copy of FPiece[,]
    public FPiece[,] CopyBoard(FPiece[,] board)
    {
        print("-----------------------------------------------------------");
        print("-----------------------------------------------------------");
        print("-----------------------------------------------------------");
        print("-----------------------------------------------------------");
        print("-----------------------------------------------------------");
        print("-----------------------------------------------------------");
        print("-----------------------------------------------------------");
        print("-----------------------------------------------------------");
        print("-----------------------------------------------------------");
        print("Copying Board");

        FPiece[,] result = new FPiece[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                result[i, j] = null;
            }
        }


        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (board[x, y] != null)
                {

                    //print("Getting here");
                    FPiece current = new FPiece
                    {
                        x = x,
                        y = y,
                        isKing = board[x, y].isKing,
                        isWhite = board[x, y].isWhite
                    };
                    result[x, y] = current;

                }
            }
        }


        /*debug*/
        print("-----------------------------------------------------------");
        print("Board Copied");
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (result[i, j] != null)
                {
                    print(result[i, j].x);
                    print(result[i, j].y);
                    print(" ");
                }
                else
                {
                    print("null");
                    print(" ");
                }


            }
        }
        print("-----------------------------------------------------------");
        print("-----------------------------------------------------------");
        print("-----------------------------------------------------------");
        print("-----------------------------------------------------------");
        print("-----------------------------------------------------------");
        print("-----------------------------------------------------------");
        print("-----------------------------------------------------------");
        print("-----------------------------------------------------------");
        print("-----------------------------------------------------------");



        return result;
    }





    //Function that returns all the white pieces on the board as a list
    public List<FPiece> GetWhitePieces(FPiece[,] board)
    {
        List<FPiece> result = new List<FPiece>();

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

        /*debug*/
        /*Debug.Log("white pieces:");
        foreach (Piece x in result)
        {
            Debug.Log(x.x);
            Debug.Log(x.y);
            Debug.Log(" ");
        }*/


        return result;
    }







    //Function to get a list of all the possible moves in the passed in board state for the piece 'p'
    private List<FPiece> GetPossibleMoves(FPiece p, FPiece[,] board)
    {

        /*Debug.Log("*************current piece:*********************");
        Debug.Log(p.x);
        Debug.Log(p.y);
       */



        List<FPiece> result = new List<FPiece>();

        //If its a forced movenent
        if (p.IsForceMovement(board, p.x, p.y)) {


            //If its king. 


            //If its white 


            //if its black





        }





        //If its NOT forced movement
        if (p.isKing)
        {
            //Checking bottom left
            if (p.CheckMoveValidation(board, p.x, p.y, p.x - 1, p.y - 1))
            {
                FPiece n = new FPiece(p.x - 1, p.y - 1);
                result.Add(n);
            }

            //Checking top left
            if (p.CheckMoveValidation(board, p.x, p.y, p.x - 1, p.y + 1))
            {
                FPiece n = new FPiece(p.x - 1, p.y + 1);
                result.Add(n);
            }

            //checking top right
            if (p.CheckMoveValidation(board, p.x, p.y, p.x + 1, p.y + 1))
            {
                FPiece n = new FPiece(p.x + 1, p.y + 1);
                result.Add(n);
            }

            //checking bottom right
            if (p.CheckMoveValidation(board, p.x, p.y, p.x + 1, p.y - 1))
            {
                FPiece n = new FPiece(p.x + 1, p.y - 1);
                result.Add(n);
            }

            return result;
        }


        //If p is not the king, then need to check two adjacent squares based on the color.
        if (p.isWhite)
        {
            //if the piece is white, need to go forward, i.e the y co-ordinates increase
            if (p.CheckMoveValidation(board, p.x, p.y, p.x - 1, p.y + 1))
            {
                FPiece n = new FPiece(p.x - 1, p.y + 1);
                result.Add(n);
            }
            if (p.CheckMoveValidation(board, p.x, p.y, p.x + 1, p.y + 1))
            {
                FPiece n = new FPiece(p.x + 1, p.y + 1);
                result.Add(n);
            }
            return result;
        }
        else
        {
            //if the piece is black, need to go backward, i.e. the y co-ordinates decrease 
            if ((p.x - 1 >= 0) && (p.y - 1 >= 0) && (p.x - 1 <= 7) && (p.y - 1 <= 7))
            {
                if (board[p.x - 1, p.y - 1] == null)
                {
                    FPiece n = new FPiece(p.x - 1, p.y - 1);
                    result.Add(n);
                }
            }

            if ((p.x + 1 >= 0) && (p.y - 1 >= 0) && (p.x + 1 <= 7) && (p.y - 1 <= 7))
            {
                if (board[p.x + 1, p.y - 1] == null)
                {
                    FPiece n = new FPiece(p.x + 1, p.y - 1);
                    result.Add(n);
                }
            }
            return result;
        }
    }







    //Function to carry out a virtual move in the virtual board 
    private Move MakeVirtualMove(FPiece source, FPiece dest, Move thisMove)
    {
        //TODO make score calculation here.
        int newScore = 0;


        //Make a copy of the board that has been passed in
        FPiece[,] newBoard = thisMove.GetBoard();

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
                FPiece dead = newBoard[((x1 + x2) / 2), ((y1 + y2) / 2)]; //position of dead piece
                if (dead != null)
                {
                    newBoard[((x1 + x2) / 2), ((y1 + y2) / 2)] = null;
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
            if (move.score >= result.score)
            {
                result = move;
            }
        }
        return result;
    }








    private void ChildMove(Move move, bool isWhite, int depth)
    {


        //base case. add all the children moves into the global variable and return  
        if (depth <= 0)
        {
            finalList.Add(move);
            return;
        }




        FPiece[,] board = move.GetBoard();

        //Create a list of pieces of current color
        List<FPiece> pieces = new List<FPiece>();
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
        foreach (FPiece origin in pieces)
        {

            //Generate a list of all possible moves
            List<FPiece> destinations = GetPossibleMoves(origin, board);

            //Once you have a list of all possible moves. Carry out the virtual move on the board
            foreach (FPiece destination in destinations)
            {
                //copy the board
                FPiece[,] newBoard = CopyBoard(board);
                //make a new move and carry out a virual move on that board 
                Move newM = new Move(move.GetOrigin(), move.GetDestination(), newBoard);
                //Make virtual move here and call the recursive function on the following: 
                Move newMove = MakeVirtualMove(origin, destination, newM);
                newList.Add(newMove);
            }
        }


        //call recursive function on each move in newList with depth-1, oppposite color.
        foreach (Move n in newList)
        {
            ChildMove(n, !isWhite, depth-1);
        }

    }





    public int[] GetMove(Piece[,] mainBoard)
    {

        int[] result = new int[4];


        FPiece[,] board = CopyMainBoard(mainBoard);




        List<FPiece> blackPieces = GetBlackPieces(board); //get all black pieces on the board
        List<Move> moves = new List<Move>();//list to store the list of moves

        //For each black piece on the board
        foreach (FPiece piece in blackPieces)
        {

            Debug.Log("*************current piece:*********************");
            Debug.Log(piece.x);
            Debug.Log(piece.y);


            //get a list of all possible destinations
            List<FPiece> destinations = GetPossibleMoves(piece, board);


            Debug.Log("--------------------------------------------");
            Debug.Log("destinations:");



            //for each destination1
            foreach (FPiece destination in destinations)
            {
                Debug.Log(destination.x);
                Debug.Log(destination.y);


                FPiece[,] newBoard = CopyBoard(board);
                //make a new move and carry out a virual move on that board 


                //generate a phantom piece for the origin 
                FPiece origin = new FPiece(piece.x, piece.y);

                Move newMove = new Move(origin, destination, newBoard);
                Move resultantMove = MakeVirtualMove(piece, destination, newMove);
                moves.Add(resultantMove); //add the virtual move into the board
            }

            Debug.Log("#########################################");
        }

        //now call the recursive function on each of the child moves carried out 
        foreach (Move thismove in moves)
        {
            ChildMove(thismove, true, 1);
        }


        //do the resetting, and returning here

        Move finalMove = GetBestMove(finalList); //getting the best move among the list of moves
        finalList = null; //resetting the global variable 

        result[0] = finalMove.GetOrigin().x;
        result[1] = finalMove.GetOrigin().y;
        result[2] = finalMove.GetDestination().x;
        result[3] = finalMove.GetDestination().y;


        Debug.Log("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
        Debug.Log("BestMove: ");
        Debug.Log(result[0]);
        Debug.Log(result[1]);
        Debug.Log("TO: ");
        Debug.Log(result[2]);
        Debug.Log(result[3]);
        Debug.Log("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");




        return result;
    }
}
