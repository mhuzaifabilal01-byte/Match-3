using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width;
    public int height;
    [SerializeField] private GameObject tileBackground;
    [SerializeField] Gems[] gems;
    public Gems[,] allGems;
    public float gemSpeed;
    [HideInInspector] public MatchFinder matchFinder;
    public enum BoardState { rest, move }
    public BoardState currentState = BoardState.move;
    public Gems bomb;
    [SerializeField]
    private float bombChance = 2f;
    [HideInInspector] public RoundManager roundManager;
    private float bonusMultiplier;
    private float bonusAmount = 0.5f;
    public BoardLayout boardLayout;
    public Gems[,] layoutStore;
    private void Awake()
    {
        matchFinder = FindObjectOfType<MatchFinder>();
        roundManager = FindObjectOfType<RoundManager>();
        boardLayout = GetComponent<BoardLayout>();
    }
    private void Start()
    {
        allGems = new Gems[width, height];
        layoutStore = new Gems[width, height];
        SetUP();
    }
    private void Update()
    {
        //matchFinder.FindAllMatches();
        if(Input.GetKeyUp(KeyCode.S)) 
        {
            ShuffleBoard();
        }
    }
    private void SetUP()
    {

        if(boardLayout!= null)
        {
            layoutStore = boardLayout.GetLLayout();
        }
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 position = new Vector2(i, j);
                GameObject backGroundTile = Instantiate(tileBackground, position, Quaternion.identity);
                backGroundTile.transform.parent = transform;
                if (layoutStore[i, j] != null)
                {
                    SpawnGem(new Vector2Int(i, j), layoutStore[i, j]);
                }
                else
                {
                    int gemtoSpawn = Random.Range(0, gems.Length);
                    int iterations = 0;
                    while (MatchesAt(new Vector2Int(i, j), gems[gemtoSpawn]) && iterations < 100)
                    {
                        gemtoSpawn = Random.Range(0, gems.Length);
                        iterations++;
                    }
                    SpawnGem(new Vector2Int(i, j), gems[gemtoSpawn]);
                }
            }
        }
    }
    private void SpawnGem(Vector2Int position, Gems gemstoSpawn)
    {
        if (Random.Range(0f, 100f) < bombChance)
        {
            gemstoSpawn = bomb;
        }
        Gems gem = Instantiate(gemstoSpawn, new Vector3(position.x, position.y + height, 0f), Quaternion.identity);
        gem.transform.parent = this.transform;
        allGems[position.x, position.y] = gem;
        gem.SetUPGem(position, this);
    }
    private bool MatchesAt(Vector2Int positionToCheck, Gems gemsToCheck)
    {
        if (positionToCheck.x > 1)
        {
            if (allGems[positionToCheck.x - 1, positionToCheck.y].type == gemsToCheck.type && allGems[positionToCheck.x - 2, positionToCheck.y].type == gemsToCheck.type)
            {
                return true;
            }

        }
        if (positionToCheck.y > 1)
        {
            if (allGems[positionToCheck.x, positionToCheck.y - 1].type == gemsToCheck.type && allGems[positionToCheck.x, positionToCheck.y - 2].type == gemsToCheck.type)
            {
                return true;
            }

        }
        return false;
    }
    private void DestroyGemAt(Vector2Int position)
    {
        if (allGems[position.x, position.y] != null)
        {
            if (allGems[position.x, position.y].ISMatched)
            {
                if (allGems[position.x,position.y].type==Gems.GemType.bomb)
                {
                    SFXManager.instance.BombExplosion();
                }
                else if (allGems[position.x,position.y].type==Gems.GemType.stone)
                {
                    SFXManager.instance.StoneBreak();
                }
                else
                {
                    SFXManager.instance.GemBreak();
                }
                GameObject cloneObject = Instantiate(allGems[position.x, position.y].destroyEffect, new Vector2(position.x, position.y), Quaternion.identity);
                Destroy(allGems[position.x, position.y].gameObject);
                StartCoroutine(DestroyCloneCoroutine(cloneObject));
                allGems[position.x, position.y] = null;
            }
        }
    }
    public void DestroyGem()
    {
        for (int i = 0; i < matchFinder.currentMatches.Count; i++)
        {
            if (matchFinder.currentMatches != null)
            {
                ScoreCheck(matchFinder.currentMatches[i]);               
                DestroyGemAt(matchFinder.currentMatches[i].position);
            }
        }
        StartCoroutine(DecreaseRowCoroutine());
    }

    private IEnumerator DecreaseRowCoroutine()
    {
        yield return new WaitForSeconds(.2f);
        int nullCounter = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allGems[x, y] == null)
                {
                    nullCounter++;
                }
                else if (nullCounter > 0)
                {
                    allGems[x, y].position.y -= nullCounter;
                    allGems[x, y - nullCounter] = allGems[x, y]; allGems[x, y] = null;
                    allGems[x, y] = null;
                }
            }
            nullCounter = 0;
        }
        StartCoroutine(FillBoardCoroutine());
    }
    private IEnumerator FillBoardCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        RefillBoard();
        yield return new WaitForSeconds(0.5f);
        matchFinder.FindAllMatches();
        if (matchFinder.currentMatches.Count > 0)
        {
            bonusMultiplier++;
            yield return new WaitForSeconds(0.5f);
            DestroyGem();
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            currentState = BoardState.move;
            bonusMultiplier = 0f;
        }


    }
    private void RefillBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allGems[x, y] == null)
                {
                    int gemsToUse = Random.Range(0, gems.Length);
                    SpawnGem(new Vector2Int(x, y), gems[gemsToUse]);
                }
            }
        }
    }
    private void FindMisplacedGems()
    {
        List<Gems> foundGems = new List<Gems>();
        foundGems.AddRange(FindObjectsOfType<Gems>());
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (foundGems.Contains(allGems[x, y]))
                {
                    foundGems.Remove(allGems[x, y]);
                }
            }
        }
        foreach (Gems gems in foundGems)
        {
            Destroy(gems.gameObject);
        }
    }
    public void ShuffleBoard()
    {
        if (currentState != BoardState.rest)
        {
            currentState = BoardState.rest;
            List<Gems> gemsFromBoard = new List<Gems>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    gemsFromBoard.Add(allGems[x, y]);
                    allGems[x, y] = null;
                }
            }
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int gemsToUse=Random.Range(0, gemsFromBoard.Count);
                    int iterations = 0;
                    while(MatchesAt(new Vector2Int(x, y), gemsFromBoard[gemsToUse]) && iterations < 100 && gemsFromBoard.Count>1)
                    {
                        gemsToUse=Random.Range(0,gemsFromBoard.Count);
                        iterations++;
                    }
                    gemsFromBoard[gemsToUse].SetUPGem(new Vector2Int(x, y), this);
                    allGems[x, y] = gemsFromBoard[gemsToUse];
                    gemsFromBoard.RemoveAt(gemsToUse);
                }
            }
            StartCoroutine(FillBoardCoroutine());
        }
    }
    private IEnumerator DestroyCloneCoroutine(GameObject cloneObject)
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(cloneObject);
    }
    public void ScoreCheck(Gems gemsToCheck)
    {
        roundManager.scoreValue += gemsToCheck.gemValue;
        if(bonusMultiplier>0)
        {
            float bonusToAdd = gemsToCheck.gemValue * bonusMultiplier * bonusAmount;
            roundManager.scoreValue += Mathf.RoundToInt(bonusToAdd);
        }
    }
}
