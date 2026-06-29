using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Gems : MonoBehaviour
{
    [HideInInspector]
    public Vector2Int position;
    private Board board;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private float degree = 0f;
    private bool mousepressed=false;
    private Gems otherGem;
    public bool ISMatched;
    private Vector2Int previousPosition;
    public enum GemType
    {
        blue,
        green,  
        red,
        yellow,
        purple,
        pink,
        silver,
        bomb,
        stone
    }
    public GemType type;
    public GameObject destroyEffect;
    public int blastSize = 2;
    public int gemValue = 10;
    private void Update()
    {
        if(Vector2.Distance(transform.position,position)>0.01f)
        {
            transform.position = Vector2.Lerp(transform.position, position, board.gemSpeed*Time.deltaTime);
        }
        else
        {
            transform.position = new Vector3(position.x, position.y, 0);
            board.allGems[position.x, position.y] = this;
        }
        if(mousepressed && Input.GetMouseButtonUp(0))
        {
            mousepressed = false;
            if (board.currentState == Board.BoardState.move && board.roundManager.roundTime > 0)
            {

                finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                CalculateAngle();
            }

        }
    }
    public void SetUPGem(Vector2Int position,Board board)
    {
        this.position = position;
        this.board = board;
    }
    private void OnMouseDown()
    {
        if (board.currentState == Board.BoardState.move && board.roundManager.roundTime > 0)
        {


            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepressed = true;
        }
    }
    private void CalculateAngle()
    {
        degree=Mathf.Atan2(finalTouchPosition.y-firstTouchPosition.y,finalTouchPosition.x-firstTouchPosition.x);
        degree=degree*180/Mathf.PI;
        if(Vector3.Distance(firstTouchPosition,finalTouchPosition) > .5f)
        {
            MovePieces();
        }
    }
    private void MovePieces()
    {
        previousPosition = position;
        if (degree < 45 && degree > -45 && position.x <( board.width - 1))
        {
           
            otherGem = board.allGems[position.x + 1, position.y];
            otherGem.position.x--;
            position.x++;

            board.allGems[position.x, position.y] = this;
            board.allGems[otherGem.position.x, otherGem.position.y] = otherGem;
        }
        else if (degree > 45 && degree <= 135 && position.y < (board.height - 1))
        {
          
            otherGem = board.allGems[position.x, position.y+1];
            otherGem.position.y--;
            position.y++;

            board.allGems[position.x, position.y] = this;
            board.allGems[otherGem.position.x, otherGem.position.y] = otherGem;
        }
        else if ((degree > 135 || degree < -135) && position.x > 0)
        {
            otherGem = board.allGems[position.x - 1, position.y];
            otherGem.position.x++;
            position.x--;
            board.allGems[position.x, position.y] = this;
            board.allGems[otherGem.position.x, otherGem.position.y] = otherGem;


        }
        else if (degree < -45 && degree >= -135 && position.y > 0 )
        {
          
            otherGem = board.allGems[position.x, position.y - 1];
            otherGem.position.y++;
            position.y--;
            board.allGems[position.x, position.y] = this;
            board.allGems[otherGem.position.x, otherGem.position.y] = otherGem;

        }
        StartCoroutine(CheckMoveCouritine());


    }
    IEnumerator CheckMoveCouritine()
    {
        board.currentState = Board.BoardState.rest;
        yield return new WaitForSeconds(0.5f);
        board.matchFinder.FindAllMatches();
        if(otherGem!= null )
        {
            if(!ISMatched && !otherGem.ISMatched)
            {
                otherGem.position = position;
                position = previousPosition;
                board.allGems[position.x, position.y] = this;
                board.allGems[otherGem.position.x, otherGem.position.y] = otherGem;
                yield return new WaitForSeconds(0.5f);
                board.currentState = Board.BoardState.move;
            }
            else
            {
                board.DestroyGem();
            }
        }
    }
    }
