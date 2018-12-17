using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AI_Behavior : CheckersBoard
{
    //Define the offensive points 
    private readonly int CAPTUREPIECE = 2;
    private readonly int CAPTUREKING = 4;
    private readonly int CAPTUREDOUBLE = 5;
    private readonly int MAKEKING = 4;
    private readonly int OPPATRISK = 1;
    private readonly int OPPKINGATRISK = 3;

    //Define defensive points 
    private readonly int ATRISK = -1;
    private readonly int KINGATRISK = -3;
    private readonly int KINGDEAD = -4;
    private readonly int LOSTPIECE = -2;
    private readonly int MAKEOPPKING = -3;

    //Used for debugging purposes:
    //Total number of nodes used in the current AI turn
    int nodeCount = 0;


    //NOTES ON ALPHA BETA NODE USEAGE:
    //Using a Move node to store the alpha beta pruning values and depth. 
    //Using org.x for max val (alpha) 
    //Using dest.x for min val (beta)
    //Using score for depth 
    List<Move> abList = new List<Move>(); 
  
     int MAXDEPTH = 3; 

   
    //Function that copies the original board and creates a copy in Fpiece
    public FPiece[,] CopyMainBoard(Piece[,] board)
    {
        //New board to store the result in
        FPiece[,] result = new FPiece[8, 8];


        //Initialize the new board
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                result[i, j] = null;
            }
        }


        //Copy everything from the old board onto new board
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (board[x, y] != null)
                {

                    FPiece current = new FPiece
                    {
                        x = board[x, y].x,
                        y = board[x, y].y,
                        isKing = board[x, y].isKing,
                        isWhite = board[x, y].isWhite
                    };
                    result[x, y] = current;

                }
            }
        }

        return result;
    }
    
    
    
    
    
    
    
    
    //Function that creates a copy of FPiece[,]
    public FPiece[,] CopyBoard(FPiece[,] board)
    //This function was necessary along with the first one to make virtual moves possible
    {
        //New board to copy onto
        FPiece[,] result = new FPiece[8, 8];


        //Initialize new board
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                result[i, j] = null;
            }
        }


        //Copy everything from the old board onto new board
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (board[x, y] != null)
                {

                    FPiece current = new FPiece
                    {
                        x = board[x, y].x,
                        y = board[x, y].y,
                        isKing = board[x, y].isKing,
                        isWhite = board[x, y].isWhite
                    };
                    result[x, y] = current;

                }
            }
        }


        return result;
    }







    //Function that returns all the black pieces on the board as a list
    public List<FPiece> GetBlackPieces(FPiece[,] board)
    {
        //Resultant list to add the black pieces of the board onto
        List<FPiece> result = new List<FPiece>();


        //Loop through the board and find all the black pieces
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

        //Return the list
        return result;
    }







    //Function that returns all the white pieces on the board as a list
    public List<FPiece> GetWhitePieces(FPiece[,] board)
    {
        //List onto which all the white pieces of the board is stored
        List<FPiece> result = new List<FPiece>();


        //Loop through the board and find all the white pieces
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


        //Return the resultant list
        return result;
    }







    //Function to get a list of all the possible moves in the passed in board state for the piece 'p'
    private List<FPiece> GetPossibleMoves(FPiece p, FPiece[,] board)
    {

        //Resultant list of faux pieces onto which the destinations will be saved
        List<FPiece> result = new List<FPiece>();


        //Check if the movement of the passed-in piece has been restricted
        if (p.IsForceMovement(board, p.x, p.y))
        {

            //If the piece is a king. 
            if (p.isKing)
            {

                //Check the four corners around it

                //Bottom right
                if (p.CheckMoveValidation(board, p.x, p.y, p.x + 2, p.y - 2))
                {
                    FPiece n = new FPiece(p.x + 2, p.y - 2);
                    result.Add(n);
                }

                //Bottom left 
                if (p.CheckMoveValidation(board, p.x, p.y, p.x - 2, p.y - 2))
                {
                    FPiece n = new FPiece(p.x - 2, p.y - 2);
                    result.Add(n);
                }

                //Top right
                if (p.CheckMoveValidation(board, p.x, p.y, p.x + 2, p.y + 2))
                {
                    FPiece n = new FPiece(p.x + 2, p.y + 2);
                    result.Add(n);
                }

                //Top left 
                if (p.CheckMoveValidation(board, p.x, p.y, p.x - 2, p.y + 2))
                {
                    FPiece n = new FPiece(p.x - 2, p.y + 2);
                    result.Add(n);
                }

                //Checked all possible places for king, return list
                return result;
            }

            //Else if its not a king but is white 
            if (p.isWhite)
            {
                //Check top right corner
                if (p.CheckMoveValidation(board, p.x, p.y, p.x + 2, p.y + 2))
                {
                    FPiece n = new FPiece(p.x + 2, p.y + 2);
                    result.Add(n);
                }

                //Check top left corner
                if (p.CheckMoveValidation(board, p.x, p.y, p.x - 2, p.y + 2))
                {
                    FPiece n = new FPiece(p.x - 2, p.y + 2);
                    result.Add(n);
                }

                //All possible locations checked. Return result.
                return result;
            }


            //Finally, if it's black
            if (!p.isWhite)
            {
                //Check bottom right corner
                if (p.CheckMoveValidation(board, p.x, p.y, p.x + 2, p.y - 2))
                {
                    FPiece n = new FPiece(p.x + 2, p.y - 2);
                    result.Add(n);
                }

                //Check bottom left corner
                if (p.CheckMoveValidation(board, p.x, p.y, p.x - 2, p.y - 2))
                {
                    FPiece n = new FPiece(p.x - 2, p.y - 2);
                    result.Add(n);
                }

                //All possible positions checked, return result.
                return result;
            }

        }


        //Else, if the movement isn't forced. Check for three cases : King, white, black
        else
        {

            //If its NOT forced movement and is king
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


            //If p is not the king, then need to check for white and black

            //If its white
            if (p.isWhite)
            {
                //Check top left
                if (p.CheckMoveValidation(board, p.x, p.y, p.x - 1, p.y + 1))
                {
                    FPiece n = new FPiece(p.x - 1, p.y + 1);
                    result.Add(n);
                }

                //Check top right
                if (p.CheckMoveValidation(board, p.x, p.y, p.x + 1, p.y + 1))
                {
                    FPiece n = new FPiece(p.x + 1, p.y + 1);
                    result.Add(n);
                }

                //All places checked, return result
                return result;
            }

            if (!p.isWhite)
            {
                //if the piece is black

                //Check bottom left
                if (p.CheckMoveValidation(board, p.x, p.y, p.x - 1, p.y - 1))
                {
                    if (board[p.x - 1, p.y - 1] == null)
                    {
                        FPiece n = new FPiece(p.x - 1, p.y - 1);
                        result.Add(n);
                    }
                }

                //Check bottom right
                if (p.CheckMoveValidation(board, p.x, p.y, p.x + 1, p.y - 1))
                {
                    if (board[p.x + 1, p.y - 1] == null)
                    {
                        FPiece n = new FPiece(p.x + 1, p.y - 1);
                        result.Add(n);
                    }
                }

                //All possible places checked, return result
                return result;
            }
        }

        //If it reaches here, there aren't any possible moves. Return empty list
        return result;
    }







    //Function to make a virtual move on a virtual board 
    //Function also scores the current board state, maximizing the score of black piece and minimizing the score of the white one.
    private Move MakeVirtualMove(FPiece source, FPiece dest, Move thisMove)
    {
        //Variable to store the score colleceted by the virtual move made
        int newScore = 0;

        //Store the king status of the passes in piece
        bool wasKing;
        if (source.isKing)
        {
            wasKing = true;
        }
        else
        {
            wasKing = false;
        }

        //Make a copy of the board that has been passed in
        FPiece[,] newBoard = CopyBoard(thisMove.GetBoard());

        //Check if the move is valid 
        if (source.CheckMoveValidation(newBoard, source.x, source.y, dest.x, dest.y))
        //If the move is valid, carry it out in the virtual board
        {
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
                //Check if you actually kill something
                if (dead != null)
                {
                    newBoard[((x1 + x2) / 2), ((y1 + y2) / 2)] = null;
                    //Check if the king was killed and implement score correspondingly
                    if (dead.isKing)
                    {
                        //Check if AI killed white
                        if (dead.isWhite)
                        {
                            newScore = newScore + CAPTUREKING;
                        }
                        else
                        {
                            //IF AIKING was killed
                            newScore = newScore + KINGDEAD;
                        }
                    }
                    else
                    //If the king wasn't killed, a regular piece was
                    {
                        if (dead.isWhite)
                        {
                            //IF AI piece killed white piece
                            newScore = newScore + CAPTUREPIECE;
                        }
                        else
                        {
                            //IF AIpiece was killed by white
                            newScore = newScore + LOSTPIECE;
                        }
                    }
                }
                else
                //If the function reaches here, there was some error. Send out debug message and return
                {
                    Debug.Log("Error in Make virtual move > Kill move.");
                    return thisMove;
                }
            }

            //Create a new faux piece that is a duplicate of the source piece to move
            FPiece newSource = new FPiece(source.x, source.y, source.isWhite, source.isKing);

            //Move the piece onto the new board
            newBoard[x2, y2] = newSource;
            newBoard[x1, y1] = null;

            //Update the x and y vaules of the new piece
            newSource.x = x2;
            newSource.y = y2;

            //check to see if the source piece needs to be turned into a king 
            //check for white piece 
            if ((newSource.isWhite) && (y2 == 7))
            {
                newSource.isKing = true;
            }
            //check for black piece
            else if ((!newSource.isWhite) && (y2 == 0))
            {
                newSource.isKing = true;
            }

            //If the newSource wasn't king before, but is now, append score 
            if (!wasKing && newSource.isKing)
            {

                if (!newSource.isWhite)
                {
                    newScore = newScore + MAKEKING;
                }
                else
                {
                    newScore = newScore + MAKEOPPKING;
                }
            }

            //Now do the rest of Score checking for the current board status starting here: TODO
            //REMEMBER AI IS BLACK IN COLOR so only take into account the score for black color. 
            //NOW ALL WE NEED TO DO IS CALCULATE "AT-RISK" FACTORS.
            //if the piece that made a move is a while piece, check to see if it can be taken by black pieces
            if (newSource.isWhite)
            {
                // check index out of bounds
                if (!(((x2 + 1) < 0) || ((x2 + 1) > 7) || ((y2 + 1) < 0) || ((y2 + 1) > 7)) && (newBoard[x2 + 1, y2 + 1] != null))
                {
                    //if there's a black piece to the top right and an empty spot to the bottom left, it can be taken
                    if (!newBoard[x2 + 1, y2 + 1].isWhite)
                    {
                        // check index out of bounds for bottom left
                        if (!(((x2 - 1) < 0) || ((x2 - 1) > 7) || ((y2 - 1) < 0) || ((y2 - 1) > 7)))
                        {
                            if (newBoard[x2 - 1, y2 - 1] == null)
                            {
                                if (newSource.isKing)
                                {
                                    newScore = newScore + OPPKINGATRISK;
                                }
                                else
                                {
                                    newScore = newScore + OPPATRISK;
                                }
                            }
                        }

                    }
                }

                // check index out of bounds
                if (!(((x2 - 1) < 0) || ((x2 - 1) > 7) || ((y2 + 1) < 0) || ((y2 + 1) > 7)) && (newBoard[x2 - 1, y2 + 1] != null))
                {
                    //if there's a black piece to the top left and an empty spot to the bottom right, it can be taken
                    if (!newBoard[x2 - 1, y2 + 1].isWhite)
                    {
                        // check index out of bounds
                        if (!(((x2 + 1) < 0) || ((x2 + 1) > 7) || ((y2 - 1) < 0) || ((y2 - 1) > 7)))
                        {
                            if (newBoard[x2 + 1, y2 - 1] == null)
                            {
                                if (newSource.isKing)
                                {
                                    newScore = newScore + OPPKINGATRISK;
                                }
                                else
                                {
                                    newScore = newScore + OPPATRISK;
                                }
                            }
                        }
                    }
                }

                // check index out of bounds
                if (!(((x2 - 1) < 0) || ((x2 - 1) > 7) || ((y2 - 1) < 0) || ((y2 - 1) > 7)) && (newBoard[x2 - 1, y2 - 1] != null))
                {
                    //if there's a black king to the bottom left and an empty spot to the top right, it can be taken
                    if (!newBoard[x2 - 1, y2 - 1].isWhite && newBoard[x2 - 1, y2 - 1].isKing)
                    {
                        // check index out of bounds
                        if (!(((x2 + 1) < 0) || ((x2 + 1) > 7) || ((y2 + 1) < 0) || ((y2 + 1) > 7)))
                        {
                            if (newBoard[x2 + 1, y2 + 1] == null)
                            {
                                if (newSource.isKing)
                                {
                                    newScore = newScore + OPPKINGATRISK;
                                }
                                else
                                {
                                    newScore = newScore + OPPATRISK;
                                }
                            }
                        }
                    }
                }

                // check index out of bounds
                if (!(((x2 + 1) < 0) || ((x2 + 1) > 7) || ((y2 - 1) < 0) || ((y2 - 1) > 7)) && (newBoard[x2 + 1, y2 - 1] != null))
                {
                    //if there's a black king to the bottom right and an empty spot to the top left, it can be taken
                    if (!newBoard[x2 + 1, y2 - 1].isWhite && newBoard[x2 + 1, y2 - 1].isKing)
                    {
                        // check index out of bounds
                        if (!(((x2 - 1) < 0) || ((x2 - 1) > 7) || ((y2 + 1) < 0) || ((y2 + 1) > 7)))
                        {
                            if (newBoard[x2 - 1, y2 + 1] == null)
                            {
                                if (newSource.isKing)
                                {
                                    newScore = newScore + OPPKINGATRISK;
                                }
                                else
                                {
                                    newScore = newScore + OPPATRISK;
                                }
                            }
                        }
                    }
                }
            }

            //if the piece  that made a move is a black piece, check to see if it can be taken by white pieces
            if (!newSource.isWhite)
            {
                // check index out of bounds
                if (!(((x2 + 1) < 0) || ((x2 + 1) > 7) || ((y2 - 1) < 0) || ((y2 - 1) > 7)) && (newBoard[x2 + 1, y2 - 1] != null))
                {
                    //if there's a white piece to the bottom right and an empty spot to the top left, it can be taken
                    if (newBoard[x2 + 1, y2 - 1].isWhite)
                    {
                        // check index out of bounds
                        if (!(((x2 - 1) < 0) || ((x2 - 1) > 7) || ((y2 + 1) < 0) || ((y2 + 1) > 7)))
                        {
                            if (newBoard[x2 - 1, y2 + 1] == null)
                            {
                                if (newSource.isKing)
                                {
                                    newScore = newScore + KINGATRISK;
                                }
                                else
                                {
                                    newScore = newScore + ATRISK;
                                }
                            }
                        }

                    }
                }

                // check index out of bounds
                if (!(((x2 - 1) < 0) || ((x2 - 1) > 7) || ((y2 - 1) < 0) || ((y2 - 1) > 7)) && (newBoard[x2 - 1, y2 - 1] != null))
                {
                    //if there's a white piece to the bottom left and an empty spot to the top right, it can be taken
                    if (newBoard[x2 - 1, y2 - 1].isWhite)
                    {
                        // check index out of bounds
                        if (!(((x2 + 1) < 0) || ((x2 + 1) > 7) || ((y2 + 1) < 0) || ((y2 + 1) > 7)))
                        {
                            if (newBoard[x2 + 1, y2 + 1] == null)
                            {
                                if (newSource.isKing)
                                {
                                    newScore = newScore + KINGATRISK;
                                }
                                else
                                {
                                    newScore = newScore + ATRISK;
                                }
                            }
                        }
                    }
                }

                // check index out of bounds
                if (!(((x2 - 1) < 0) || ((x2 - 1) > 7) || ((y2 + 1) < 0) || ((y2 + 1) > 7)) && (newBoard[x2 - 1, y2 + 1] != null))
                {
                    //if there's a white king to the top left and an empty spot to the bottom right, it can be taken
                    if (newBoard[x2 - 1, y2 + 1].isWhite && newBoard[x2 - 1, y2 + 1].isKing)
                    {
                        // check index out of bounds
                        if (!(((x2 + 1) < 0) || ((x2 + 1) > 7) || ((y2 - 1) < 0) || ((y2 - 1) > 7)))
                        {
                            if (newBoard[x2 + 1, y2 - 1] == null)
                            {
                                if (newSource.isKing)
                                {
                                    newScore = newScore + KINGATRISK;
                                }
                                else
                                {
                                    newScore = newScore + ATRISK;
                                }
                            }
                        }
                    }
                }

                // check index out of bounds
                if (!(((x2 + 1) < 0) || ((x2 + 1) > 7) || ((y2 + 1) < 0) || ((y2 + 1) > 7)) && (newBoard[x2 + 1, y2 + 1] != null))
                {
                    //if there's a white king to the top right and an empty spot to the bottom left, it can be taken
                    if (newBoard[x2 + 1, dest.y + 1].isWhite && newBoard[x2 + 1, y2 + 1].isKing)
                    {
                        // check index out of bounds
                        if (!(((x2 - 1) < 0) || ((x2 - 1) > 7) || ((y2 - 1) < 0) || ((y2 - 1) > 7)))
                        {
                            if (newBoard[x2 - 1, y2 - 1] == null)
                            {
                                if (newSource.isKing)
                                {
                                    newScore = newScore + KINGATRISK;
                                }
                                else
                                {
                                    newScore = newScore + ATRISK;
                                }
                            }
                        }
                    }
                }
            }

        }

        //Finally, create a new Move with the score for this movement, the new board and the original Starting moves
        Move result = new Move(thisMove.GetOrigin(), thisMove.GetDestination(), newBoard);
        result.AddScore(newScore);

        //Return the move
        return result;
    }







    List<Move> finalList = new List<Move>(); //global variable to store the end board states of the minimax algorithm
    //function to get the best move from a list of Moves
    private Move GetBestMove(List<Move> list)
    {


        Move result = new Move();
        result.AddScore(-999999);

        bool sameScore = true; //Variable to check to see if all moves have same score
        int length = list.Count;

        if (length == 1)
        {
            return list[0];
        }

        if (length != 1 && length != 0)
        {
            for (int i = 0; i < length - 1; i++)
            {
                if (list[i].GetScore() != list[i + 1].GetScore())
                {
                    sameScore = false;
                }
            }

            if (sameScore)
            {
                //if all nodes have same score, return a random node among the list 
                int rand = Random.Range(0, length);
                return list[rand];
            }
        }

        //loop through the list of final board states and return the move with the max score 
        foreach (Move move in list)
        {
            if ((move.GetScore() >= result.GetScore()))
            {
                result = move;
            }
        }

        return result;
    }







    //Function to prune the list that has been passed in 
    private List<Move> PruneList(List<Move> list, int depth)
    {

        List<Move> result = new List<Move>(); //resulant list of possible moves to pass out after pruning
        Move abNode = new Move(); //Surrogate piece for further processing

        //Find the node which contains the alpha and beta (max and min) values for the current depth.
        {
            if (abList.Count > depth)
            {
                abNode = abList[depth];
            }
            else if (abList.Count == depth)
            {
                //If this node is being visited for the first time, create a node and 
                //add onto the list of depth nodes to compare in subsequent turns
                abNode = new Move();
                abNode.AddScore(depth);
                abList.Add(abNode);

                //determining the max and min vals for this current turn
                {
                    int mi = 9999;
                    int ma = -9999;

                    foreach (Move item in list)
                    {
                        if (item.GetScore() < mi)
                        {
                            mi = item.GetScore();
                        }
                        if (item.GetScore() > ma)
                        {
                            ma = item.GetScore();
                        }
                    }
                    abNode.SetOrigin(ma, 0);
                    abNode.SetDestination(mi, 0);
                    return list;
                }
            }
        }
        
        //loop through the list and find the min and the max scores for the current list
        int minCur = 9999;
        int maxCur = -9999;
        int max = abNode.GetOrigin().x;
        int min = abNode.GetDestination().x;

        foreach (Move item in list) {
            if (item.GetScore() < minCur) {
                minCur = item.GetScore();
            }
            if (item.GetScore() > maxCur) {
                maxCur = item.GetScore(); 
            }
        }

        // 1) if new list is within range
        {
            if ((max >= maxCur) && (maxCur >= min) && (minCur <= max) && (minCur <= min))
            {
                //loop through list and add only those that have max vals more than min
                foreach (Move item in list)
                {
                    if (item.GetScore() >= min)
                    {
                        result.Add(item);

                    }
                }
                return result;
            }
        }

        // 2) if current max is more than overall max 
        {
            if (maxCur > max)
            {

                //if mincur is also more than max
                if (minCur > max)
                {
                    abNode.SetOrigin(maxCur, 0);
                    abNode.SetDestination(minCur, 0);
                }
                else
                {
                    abNode.SetOrigin(maxCur, 0);
                    abNode.SetDestination(max, 0);
                }

                int newMax = abNode.GetOrigin().x;
                int newMin = abNode.GetDestination().x;

                foreach (Move item in list)
                {
                    if (item.GetScore() >= newMin)
                    {
                        result.Add(item);
                    }
                }
                return result;
            }
        }

        // 3) If the max val for current list is less than overall min, discard everything 
        if (maxCur < min) {
            return result;
        }

        // 4) if the current min is more than overall min, change the overall min val 
        {
            if ((minCur > min) && (maxCur < max))
            {
                //then do the regular processing
                //loop through list and add only those that have max vals more than min
                foreach (Move item in list)
                {
                    if (item.GetScore() >= minCur)
                    {
                        result.Add(item);
                    }

                }
                return result;
            }
        }
        return list; 
    }







    //Main recursive function of the bunch
    private void ChildMove(Move move, bool isWhite, int depth)
    {
       //TODO: Child move runs for a fixed time. Base case should be this time elapsed? 
        //base case. add all the children moves into the global variable and return  
        if (depth == MAXDEPTH)
        {
            finalList.Add(move);
            nodeCount++;
            return;
        }

        FPiece[,] board = move.GetBoard();

        //Create a list of pieces of current color
        List<FPiece> pieces = new List<FPiece>();
        //get a list of origin pieces
        if (isWhite)
        {
            List<FPiece> tempWhites = GetWhitePieces(board);

            //check if the board is forced. Go through each piece to see if they are forced. 
            bool wasForced = false;
            foreach (FPiece x in tempWhites)
            {
                if (x.IsForceMovement(board, x.x, x.y))
                {
                    pieces.Add(x);
                    wasForced = true;
                }
            }
        
            //if the white pieces werent forced at all
            if (!wasForced)
            {
                pieces = tempWhites;
            }
        }
        else
        {
            List<FPiece> tempBlacks = GetBlackPieces(board);

            //check if the board is forced. Go through each piece to see if they are forced. 
            bool wasForced = false;
            foreach (FPiece x in tempBlacks)
            {
                if (x.IsForceMovement(board, x.x, x.y))
                {
                    pieces.Add(x);
                    wasForced = true;
                }
            }

            //if the white pieces werent forced at all
            if (!wasForced)
            {
                pieces = tempBlacks;
            }
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
                newM.AddScore(move.GetScore());
                //Make virtual move here and call the recursive function on the following: 
                Move newMove = MakeVirtualMove(origin, destination, newM);
                newList.Add(newMove);
            }
        }

        newList = PruneList(newList, depth);
        //call recursive function on each move in newList with depth-1, oppposite color.
        foreach (Move n in newList)
        {
            //Check to see if the resultant board have colors of the board or not
            if (GetBlackPieces(n.GetBoard()).Count == 0)
            {
                ChildMove(n, !isWhite, MAXDEPTH);

            }
            else if (GetWhitePieces(n.GetBoard()).Count == 0)
            {
                ChildMove(n, !isWhite, MAXDEPTH);
            }
            else
            { ChildMove(n, !isWhite, depth + 1); }
        }

    }







    //Main function of the algorithm
    public int[] GetMove(Piece[,] mainBoard)
    {

        int[] result = new int[4];

        FPiece[,] board = CopyMainBoard(mainBoard);



        //Get a list of moveable black pieces
        List<FPiece> blackPieces = new List<FPiece>();
        List<FPiece> tempBlacks = GetBlackPieces(board);
        bool wasForced = false;
        foreach (FPiece x in tempBlacks)
        {
            if (x.IsForceMovement(board, x.x, x.y))
            {
                blackPieces.Add(x);
                wasForced = true;
            }
        }

        //if the black pieces werent forced at all
        if (!wasForced)
        {
            blackPieces = tempBlacks;
        }
        else
        {
            //BASE CASE 1 : check if it was forced and now only one white is left on board, then game won
            {
                List<FPiece> whitePieces = GetWhitePieces(board);
                if (whitePieces.Count == 1)
                {

                    List<FPiece> done = GetPossibleMoves(blackPieces[0], board);


                    result[0] = blackPieces[0].x;
                    result[1] = blackPieces[0].y;
                    result[2] = done[0].x;
                    result[3] = done[0].y;

                    return result;

                }
            }

        }


        //BASE CASE 2 : Check to see if there is any possible moves for the white left
        {
            List<FPiece> wDest = new List<FPiece>();
            List<FPiece> whiteP = GetWhitePieces(board);
            foreach (FPiece piece in whiteP)
            {
                List<FPiece> destinations = GetPossibleMoves(piece, board);
                wDest.AddRange(destinations);
            }
            if (wDest.Count == 0)
            {
                //There aren't any possile moves left for white, so black won
                //BASE CASE 2 CONT: No possible moves. White won
                Debug.Log("White Won!");
                SceneManager.LoadScene("BlackWins");
            }
        }



        //BASE CASE 3: Check to see if there is only one destination
        {
            FPiece Moveable = new FPiece();
            List<FPiece> totDest = new List<FPiece>();
            foreach (FPiece piece in blackPieces)
            {
                //Get all possible moves for black pieces
                List<FPiece> destinations = GetPossibleMoves(piece, board);
                if (destinations.Count != 0)
                {
                    Moveable = piece; //Store the piece that can move for future use
                }
                totDest.AddRange(destinations);
            }
            if (totDest.Count == 1)
            {
                //Find the piece that can move
                //Return that piece
                result[0] = Moveable.x;
                result[1] = Moveable.y;
                result[2] = totDest[0].x;
                result[3] = totDest[0].y;
                return result;
            }
            else if (totDest.Count == 0)
            {
                //BASE CASE 3 CONT: No possible moves. White won
                Debug.Log("White Won!");
                SceneManager.LoadScene("WhiteWins");
            }
        }


        List<Move> moves = new List<Move>();//list to store the list of moves

        //For each black piece on the board
        foreach (FPiece piece in blackPieces)
        {
           
            List<FPiece> destinations = GetPossibleMoves(piece, board);


            //for each destination
            foreach (FPiece destination in destinations)
            {
                FPiece[,] newBoard = CopyBoard(board);
                //make a new move and carry out a virual move on that board 


                //generate a phantom piece for the origin 
                //FPiece origin = new FPiece(piece.x, piece.y);

                Move newMove = new Move(piece, destination, newBoard);
                Move resultantMove = MakeVirtualMove(piece, destination, newMove);
                moves.Add(resultantMove); //add the virtual move into the board
            }
        }

         moves = PruneList(moves, 0);
        //now call the recursive function on each of the child moves carried out 
        foreach (Move n in moves)
        {
            //Check to see if the resultant board have colors of the board or not
            if (GetBlackPieces(n.GetBoard()).Count == 0)
            {
                ChildMove(n, false, MAXDEPTH);

            }
            else if (GetWhitePieces(n.GetBoard()).Count == 0)
            {
                ChildMove(n, false, MAXDEPTH);
            }
            else
            { ChildMove(n, true, 1); }
        }

        //do the resetting, and returning here
        Move finalMove = GetBestMove(finalList); //getting the best move among the list of moves
        finalList = null; //resetting the global variable 
        abList = null; //resetting the pruning list

        result[0] = finalMove.GetOrigin().x;
        result[1] = finalMove.GetOrigin().y;
        result[2] = finalMove.GetDestination().x;
        result[3] = finalMove.GetDestination().y;

        print(nodeCount); //Debugging purposes. 
        return result;
    }
}
