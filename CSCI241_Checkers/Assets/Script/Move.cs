using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{


    private Piece org;
    private Piece dest;
    private Piece[,] board;
    public int score;
    private List<Move> children;


    //Constructor function 
    public Move() {
        this.org = new Piece();
        this.dest = new Piece();
        this.board = new Piece[8, 8];
        this.score = 0;
        this.children = new List<Move>(); 

    }


    //Constructor function.
    public Move(Piece Origin, Piece Destination, Piece[,] NewBoard) {
        this.org = Origin;
        this.dest = Destination;
        this.board = NewBoard;
        this.score = 0; 
        this.children = new List<Move>(); 
    }

    //Constructor function.
    public Move(Piece Origin, Piece Destination, Piece [,] NewBoard, List<Move> Child)
    {
        this.org = Origin;
        this.dest = Destination;
        this.children = new List<Move>();
        this.score = 0; 
        this.board = NewBoard;
        this.children = Child; 
    }


    //Get the original position function
    public Piece GetOrigin() {
        return org; 
    }


    //Get the original destination
    public Piece GetDestination() {
        return dest; 
    }

    //function to set the new board
    public void SetBoard(Piece[,] newBoard) {
        this.board = newBoard; 
    }

    //function to get the board in current Move
    public Piece[,] GetBoard() {
        return this.board; 
    }

    //Set the children 
    public void SetChildren(List<Move> Child) {
        this.children = Child; 
    }

    //get the children 
    public List<Move> GetChildren() {
        return this.children; 
    }

 

    //function to update score; 
    public void AddScore(int ScoreUpdate) {
        this.score = this.score + ScoreUpdate;
    }

}