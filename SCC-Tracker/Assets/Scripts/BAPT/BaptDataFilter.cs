using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaptDataFilter : MonoBehaviour
{
    public TextAsset csvFile;

    public int sheetId;
    public int tableId;
    public string[,] datas;

    // Start is called before the first frame update
    void Start()
    {
        Output();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Output()
    {
        string[] dataUnfilter = csvFile.text.Split(',');
        for (int i = 0; i < dataUnfilter.Length; i++)
        {
            if (dataUnfilter[i].Contains("\""))
            {
                dataUnfilter[i] = dataUnfilter[i].Replace("\n", "").Replace("\r", "").Replace("\"", "");
            }
        }

        print(string.Join(",", dataUnfilter));

        //string[] filteredData = dataUnfilter;
        //datas = new string[filteredData.Length, 10];

        //// Process the data into a multidimension array
        //for (int x = 0; x < filteredData.Length; x++)
        //{
        //    string[] tempData = filteredData[x].Split(',');

        //    for (int y = 0; y < tempData.Length; y++)
        //    {
        //        datas[x, y] = tempData[y];
        //    }
        //}
    }
}
