using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MatchFinder : MonoBehaviour
{
    private Board board;
    public List<Gems> currentMatches = new List<Gems>();
    private void Awake()
    {
        board = FindObjectOfType<Board>();
    }
    public void FindAllMatches()
    {
        currentMatches.Clear();
        currentMatches.Clear();
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                Gems currentGem = board.allGems[i, j];
                if(currentGem != null)
                {
                    if(i > 0 && i < board.width - 1)
                    {
                        Gems leftGem = board.allGems[i-1, j];
                        Gems rightGem = board.allGems[i+1, j];
                        if(leftGem != null &&rightGem != null)
                        {
                            if(leftGem.type==currentGem.type && rightGem.type==currentGem.type && currentGem.type != Gems.GemType.stone)
                            {
                                leftGem.ISMatched = true;
                                rightGem.ISMatched = true;
                                currentGem.ISMatched = true;
                                currentMatches.Add(currentGem);
                                currentMatches.Add(rightGem);
                                currentMatches.Add(leftGem);
                            }
                        }
                    }
                    if (j > 0 && j < board.height - 1)
                    {
                        Gems aboveGem = board.allGems[i , j + 1];
                        Gems belowGem = board.allGems[i , j - 1];
                        if (aboveGem != null && belowGem != null)
                        {
                            if (aboveGem.type == currentGem.type && belowGem.type == currentGem.type && currentGem.type != Gems.GemType.stone)
                            {
                                aboveGem.ISMatched = true;
                                belowGem.ISMatched = true;
                                currentGem.ISMatched = true;
                                currentMatches.Add(currentGem); 
                                currentMatches.Add(aboveGem); 
                                currentMatches.Add(belowGem);
                            }
                        }
                    }
                }
            }
        }
        if(currentMatches.Count > 0)
        {
            currentMatches = currentMatches.Distinct().ToList();

        }
        CheckForBombs();
    }
    private void CheckForBombs()
    {
        for(int i=0;i<currentMatches.Count;i++) 
        {
            Gems gems = currentMatches[i];
            int x = gems.position.x;
            int y = gems.position.y;
            if(x > 0 )
            {
                if (board.allGems[x-1,y]!=null)
                {
                    if (board.allGems[x-1, y].type==Gems.GemType.bomb)
                    {
                        MarkBombArea(new Vector2Int(x - 1, y), board.allGems[x-1,y]);
                    }
                }
            }
            if (x < (board.width-1))
            {
                if (board.allGems[x + 1, y] != null)
                {
                    if (board.allGems[x + 1, y].type == Gems.GemType.bomb)
                    {
                        MarkBombArea(new Vector2Int(x + 1, y), board.allGems[x + 1, y]);
                    }
                }
            }
            if (y > 0)
            {
                if (board.allGems[x , y - 1] != null)
                {
                    if (board.allGems[x , y - 1].type == Gems.GemType.bomb)
                    {
                        MarkBombArea(new Vector2Int(x , y - 1), board.allGems[x , y - 1]);
                    }
                }
            }
            if (y < (board.height - 1))
            {
                if (board.allGems[x , y + 1] != null)
                {
                    if (board.allGems[x , y + 1].type == Gems.GemType.bomb)
                    {
                        MarkBombArea(new Vector2Int(x , y + 1), board.allGems[x , y + 1]);
                    }
                }
            }
        }
    }
    public void MarkBombArea(Vector2Int bombPosition,Gems bomb)
    {
        for(int x=bombPosition.x-bomb.blastSize;x<=bombPosition.x+bomb.blastSize;x++)
        {
            for(int  y=bombPosition.y-bomb.blastSize;y<=bombPosition.y+bomb.blastSize;y++)
            {
                if(x >= 0 && x < board.width && y >= 0 && y < board.height)
                {
                    if(board.allGems[x, y] != null)
                    {
                        board.allGems[x, y].ISMatched = true;
                        currentMatches.Add(board.allGems[x, y]);
                    }
                }
            }
        }
        currentMatches = currentMatches.Distinct().ToList();
    }
}
