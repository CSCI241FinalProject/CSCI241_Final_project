using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheckersBoard : MonoBehaviour {

    //This is the array that stores the board 
    public Piece[,] boardPieces = new Piece [8, 8];
    public GameObject whitePiecePrefab; //Reference to the white piece on the board
    public GameObject blackPiecePrefab; //Reference to the black piece on the board

    private Vector3 boardOffset = new Vector3(-6.0f, 0, -6.0f);
    private Vector3 pieceOffset = new Vector3(-0.5f, 0, -0.4f);
    
    private void Start()
    {
        CreateBoard();
    }

        //This function is used to set the board in the beginning of the game
        private void CreateBoard() {

        //Each player has 12 pieces with 4 peice on alternating boxes on three lines

        //First loop through and set up the pieces for the white player
        //Iterate through the y-coordinate boxes 
        for (int y = 0; y < 3; y++) {

            bool odd = false;

            //Check to see if the current row is odd
            if ((y % 2) == 0) {
                odd = true;
            }
            //Iterate through x-coordinate boxes
            for (int x = 0; x < 8; x += 2) {
                //Now generate the piece at this box
                if (odd == true)
                {
                    GeneratePiece(x, y);
                }
                else {
                    GeneratePiece(x+1, y);
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
        p.transform.position = (Vector3.right * (x*1.85f))+ (Vector3.forward *(1.85f*y)) +boardOffset +pieceOffset;
    }
}
