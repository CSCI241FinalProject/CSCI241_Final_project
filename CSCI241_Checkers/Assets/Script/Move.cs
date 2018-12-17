using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : ScriptableObject
{


    private FPiece org = new FPiece(0,0);
    private FPiece dest= new FPiece(0,0);
    private FPiece[,] board;
    private int score;
    private List<Move> children;


    //Constructor function 
    public Move() {
        this.org = new FPiece(0, 0);
        this.dest = new FPiece(0, 0);
        this.board = new FPiece[8, 8];
        this.score = 0;
        this.children = new List<Move>(); 

    }


    //Constructor function.
    public Move(FPiece Origin, FPiece Destination, FPiece[,] NewBoard) {
        this.org = Origin;
        this.dest = Destination;
        this.board = NewBoard;
        this.score = 0; 
        this.children = new List<Move>(); 
    }

    //Constructor function.
    public Move(FPiece Origin, FPiece Destination, FPiece [,] NewBoard, List<Move> Child)
    {
        this.org = Origin;
        this.dest = Destination;
        this.children = new List<Move>();
        this.score = 0; 
        this.board = NewBoard;
        this.children = Child; 
    }


    //Get the original position function
    public FPiece GetOrigin() {
        return org; 
    }


    //Get the original destination
    public FPiece GetDestination() {
        return dest; 
    }

    //function to set the new board
    public void SetBoard(FPiece[,] newBoard) {
        this.board = newBoard; 
    }

    //function to get the board in current Move
    public FPiece[,] GetBoard() {
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

    public int GetScore() {
        return this.score;
    }

    public void SetOrigin(int xx, int yy) {
        this.org.x = xx;
        this.org.y = yy;
    }

    public void SetDestination(int xx, int yy) {
        this.dest.x = xx;
        this.dest.y = yy;
    }

}