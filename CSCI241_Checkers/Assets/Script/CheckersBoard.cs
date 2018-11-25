using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckersBoard : MonoBehaviour {

    //This is the array that stores the board 
    public Piece[,] boardPieces = new Piece [8, 8];
    public GameObject whitePiecePrefab; //Reference to the white piece on the board
    public GameObject blackPiecePrefab; //Reference to the black piece on the board

    private Vector2 cursorPosition; //Vecotr variable to keep track of where the cursor is pointing currently
    private Piece selectedPiece; //Variable to keep track of current selected piece
    private Vector2 startDrag;
    private Vector2 endDrag;

    private List<Piece> forcedPieces; //List to store forced movement of pieces


    public bool playerWhite; //varaible to keep track of player piece color
    private bool isWhiteTurn; //variable to keep track if it is currently white turn
    private bool killMove; //variable to keep track if the current move killed a piece

    //Offsets for placing the players
    private Vector3 boardOffset = new Vector3(-4.0f, 0, -4.0f);
    private Vector3 pieceOffset = new Vector3(0.5f, 0, 0.5f);
    
    private void Start()
    {
        isWhiteTurn = true;
        forcedPieces = new List<Piece>();   
        CreateBoard();
    }

    //Update the state of the board
    private void Update()
    {
        UpdateCursor();
        //Debug.Log(cursorPosition);

        //If it is player turn
        {
            //Store the current cursor position
            int x = (int)cursorPosition.x;
            int y = (int)cursorPosition.y;

            if (selectedPiece !=null) {
                UpdatePieceDrag(selectedPiece);
            }

            if (Input.GetMouseButtonDown(0)) {
                //If the player left clicks at current cursor position, select the current piece
                SelectPiece(x, y);
            }

            //check for when the player releases the button
            if (Input.GetMouseButtonUp(0)) {
                //Try to move the piece there
                TryMove((int)startDrag.x, (int)startDrag.y, x, y);
            }
        }

    }
    //Function to update the cursor position
    private void UpdateCursor() {

        //Do some initial checks 

        //1) is there a main camera present? 
        if (!Camera.main) {
            //Set a debug message
            Debug.Log("Main camera not present");
            return;
        }

        
        RaycastHit touch; //Place to see where the cursor is currently pointing
        //If the cursor hits the "board"
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out touch, 25.0f, LayerMask.GetMask("Board")))
        {
            cursorPosition.x = (int)(touch.point.x - boardOffset.x);
            cursorPosition.y = (int)(touch.point.z - boardOffset.z);  //The board is on the z axis
        }
        else {
            cursorPosition.x =-1;
            cursorPosition.y = -1;
        }




    }

    private void UpdatePieceDrag(Piece p)
    {
        if (!Camera.main)
        {
            Debug.Log("Unable to find main camera");
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("Board")))
        {
            p.transform.position = hit.point + Vector3.up;
        }
    }

    
    //Fuction that is used to select the clicked on piece
    private void SelectPiece(int x, int y) {

        //Check if the position passed in is out of bounds 
        if ((x < 0) || (x > 7) || (y < 0) || (y > 7)) {
            //Return because error
            return;
        }

        Piece current = boardPieces[x, y];

        if ((current != null) && (current.isWhite == playerWhite))
        {

            if (forcedPieces.Count == 0)
            {
                selectedPiece = current;
                startDrag = cursorPosition;
                //Debug.Log(selectedPiece);
            }
            else {
                //Look for the piece in the forcedpieces list
                if (forcedPieces.Find(currPiece => currPiece == current) == null) {
                    return;
                }
                selectedPiece = current;
                startDrag = cursorPosition;
            }
        }
    }

    //Function to move the piece from (x1,y1) to (x2,y2)
    private void TryMove(int x1, int y1, int x2, int y2) {

        //First off, check if there is any piece that is being forced to move
        forcedPieces = ScanForceMovement();
        
        
        //For two player support
        startDrag = new Vector2(x1, y1);
        endDrag = new Vector2(x2, y2);
        selectedPiece = boardPieces[x1, y1];

        //Check for out of bounds
        if ((x2 < 0) || (x2 > 7) || (y2 < 0) || (y2 > 7))
        {
            //This is trying to move to a space outside the board
            //So reset the values
            //Return the selected piece to original position
            if (selectedPiece != null) {
                MovePiece(selectedPiece, x1, y1);
            }
            startDrag = Vector2.zero;
            selectedPiece = null;
            return;
        }

        //Is there a selected piece
        if (selectedPiece != null) {
            //If moving to same location, do nothing
            if (endDrag == startDrag) {
                MovePiece(selectedPiece,x1, y1);
                startDrag = Vector2.zero;
                selectedPiece = null;
                return;
            }


            //check if the move is actually valid or not
            if (selectedPiece.CheckMoveValidation(boardPieces, x1, y1, x2, y2))
            {

                //Check if we killed anything at all (if the move is a jump)

                if (Mathf.Abs(x1 - x2) == 2)
                {
                    Piece dead = boardPieces[((x1 + x2) / 2), ((y1 + y2) / 2)]; //position of dead piece
                    if (dead != null)
                    {
                        boardPieces[((x1 + x2) / 2), ((y1 + y2) / 2)] = null;
                        Destroy(dead.gameObject);
                        killMove = true; //setting the kill variable to true
                    }
                }

                //Check if we were actually supposed to kill anything 
                if ((forcedPieces.Count != 0) && (!killMove)) {
                    //Had somepieces we needed to kill, but we didn't kill anything
                    MovePiece(selectedPiece, x1, y1);
                    startDrag = Vector2.zero;
                    selectedPiece = null;
                    return;
                }

                //Update the array values
                boardPieces[x2, y2] = selectedPiece;
                boardPieces[x1, y1] = null;
                MovePiece(selectedPiece, x2, y2);

                EndCurrentTurn();

            }
            else {
                MovePiece(selectedPiece, x1, y1);
                startDrag = Vector2.zero;
                selectedPiece = null;
                return;
            }

        }


    }

    //Function to end current turn
    private void EndCurrentTurn() {
        selectedPiece = null;
        startDrag = Vector2.zero;

        isWhiteTurn = !isWhiteTurn;
        killMove = false; //resetting the kill move
        checkVictory();
    }

    private bool checkVictory() {
        return false;
    }

    private List<Piece> ScanForceMovement() {
        forcedPieces = new List<Piece>();

        //Check all the pieces one by one if they are forced to move or not
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                if ( (boardPieces[i, j] != null) && (boardPieces[i,j].isWhite == isWhiteTurn)){
                    if (boardPieces[i, j].IsForceMovement(boardPieces, i, j)) {
                        forcedPieces.Add(boardPieces[i, j]);
                    }
                }
            }
        }
        return forcedPieces;
    } 
    //This function is used to set the board in the beginning of the game
    private void CreateBoard()
    {

        //Each player has 12 pieces with 4 peice on alternating boxes on three lines

        //First loop through and set up the pieces for the white player
        //Iterate through the y-coordinate boxes 
        for (int y = 0; y < 3; y++)
        {

            bool odd = false;

            //Check to see if the current row is odd
            if ((y % 2) == 0)
            {
                odd = true;
            }
            //Iterate through x-coordinate boxes
            for (int x = 0; x < 8; x += 2)
            {
                //Now generate the piece at this box
                if (odd == true)
                {
                    GeneratePiece(x, y);
                }
                else
                {
                    GeneratePiece(x + 1, y);
                }
            }

        }



        //Now loop through and set up the pieces for the black player
        //Iterate through the y-coordinate boxes 
        for (int y = 7; y > 4; y--)
        {

            bool odd = false;

            //Check to see if the current row is odd
            if ((y % 2) == 0)
            {
                odd = true;
            }
            //Iterate through x-coordinate boxes
            for (int x = 0; x < 8; x += 2)
            {
                //Now generate the piece at this box
                if (odd == true)
                {
                    GeneratePiece(x, y);
                }
                else
                {
                    GeneratePiece(x + 1, y);
                }
            }

        }

    }

    //Function to generate a single piece on the board (spawn it and put it into array)
    private void GeneratePiece(int x, int y) {

        bool isWhite = false;

        if (y < 3) {
            isWhite = true;
        }

        if (isWhite)
        {
            //If it is white, do placing for white piece
            GameObject current = Instantiate(whitePiecePrefab) as GameObject;
            current.transform.SetParent(transform);

            Piece putThis = current.GetComponent<Piece>(); //Getting the components of current
            boardPieces[x, y] = putThis; //Putting the current piece into the array of board state
            MovePiece(putThis, x, y); //Move the Piece into its correct position
        }
        else {
            //Else do placing for the black piece
            GameObject current = Instantiate(blackPiecePrefab) as GameObject;
            current.transform.SetParent(transform);

            Piece putThis = current.GetComponent<Piece>(); //Getting the components of current
            boardPieces[x, y] = putThis; //Putting the current piece into the array of board state
            MovePiece(putThis, x, y); //Move the Piece into its correct position
        }
       
    }

    //Function to place the pieces into the right places of the board 
    private void MovePiece(Piece p, int x, int y) {
        p.transform.position = (Vector3.right *x)+ (Vector3.forward *y) +boardOffset +pieceOffset;
    }
}
