using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardLayout : MonoBehaviour
{
    public LayoutRow[] allRows;
    public Gems[,] GetLLayout()
    {
        Gems[,] theLayout=new Gems[allRows[0].gemsInRows.Length,allRows.Length];
        for(int y=0; y<allRows.Length;y++)
        {
            for(int x = 0; x < allRows[y].gemsInRows.Length;x++)
            {
                if(x<theLayout.GetLength(0))
                {
                    if (allRows[y].gemsInRows[x] != null)
                    {
                        theLayout[x, allRows.Length - 1 - y] = allRows[y].gemsInRows[x];
                    }
                }
            }
        }




        return theLayout;
    }
}
[System.Serializable]
public class LayoutRow
{
    public Gems[] gemsInRows;
}
