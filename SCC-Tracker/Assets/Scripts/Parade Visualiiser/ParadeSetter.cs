//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using System.Collections.Generic;
//using System.Collections;
//using System;
//using System.Linq;

//public class ParadeSetter : MonoBehaviour
//{

//    public GameObject prefab;
//    public Transform container;
//    public Vector2 offSet;
//    public TextMeshProUGUI textOverview;

//    public int ldStrength = 5;
//    public int additionalBackRows = 5;
//    public int activeStrength = 46;

//    public int numberOfRows = 3;
//    public int spacing = 100;
//    private int strengthUsed;

//    public GetDataGS getStrengthData;

//    private List<string> strengthData;

//    private void Awake()
//    {
//        getStrengthData.OnOutputDone.AddListener(UpdateVisual);
//    }

//    public void ShowVisuals()
//    {
//        StartCoroutine(getData());
//    }

//    IEnumerator getData()
//    {
//        getStrengthData.GetData(0,0);
//        yield return null;
//    }

//    public void UpdateVisual()
//    {
//        DeleteExistingParade();

//        strengthData = getStrengthData.datas;
//        // Set Unit Strength
//        if (strengthData.Count > 0)
//        {
//            for (int i = 1; i < strengthData.Count; i++)
//            {
//                List<string> data = strengthData[i].Split(',').ToList();
//                //if (Convert.ToDateTime(data[0]) != DateTime.Now.ToLocalTime()) continue;

//                activeStrength = int.Parse(data[3]);
//                ldStrength = int.Parse(data[2]) - int.Parse(data[3]);
//                break;
//            }
//        }

//        print(ldStrength);
//        print(activeStrength);

//        // Init Value
//        strengthUsed++; // OIC Stand in the middle;
//        strengthUsed = strengthUsed - additionalBackRows;
//        strengthUsed = strengthUsed - ldStrength;

//        int numberOfCol = (activeStrength - 1) / numberOfRows;
//        int numberOfBlank = (activeStrength - 1) % numberOfRows;
//        float totalWidth = numberOfRows * spacing + numberOfRows * prefab.GetComponent<RectTransform>().rect.width;
//        float totalheight = (numberOfCol) * spacing + (numberOfCol) * prefab.GetComponent<RectTransform>().rect.width;
//        int blankFiles = 0;

//        RectTransform tempGrid;
//        float xValue;
//        float yValue;

//        if (numberOfBlank != 0)
//        {
//            numberOfCol++;
//        }

//        // Create Grid
//        for (int y = 0; y < numberOfCol; y++)
//        {
//            for (int x = 0; x < numberOfRows; x++)
//            {
//                if (strengthUsed >= activeStrength) continue;

//                if (strengthUsed > activeStrength - 6)
//                {
//                    // * * *
//                    //     *
//                    // * * *
//                    if (numberOfBlank == 1)
//                    {
//                        blankFiles = 2;
//                        if (y == numberOfCol - 2 && (x == 2 || x == 1)) continue;

//                        strengthUsed++;
//                        tempGrid = Instantiate(prefab, container).GetComponent<RectTransform>();
//                        xValue = offSet.x - (x * spacing) - (x * tempGrid.rect.width) + totalWidth / 2;
//                        yValue = offSet.y + (y * spacing) + (y * tempGrid.rect.height) - totalheight / 2;
//                        tempGrid.localPosition = new Vector3(xValue, yValue);
//                        continue;
//                    }

//                    // * * *
//                    // *   *
//                    // * * *
//                    if (numberOfBlank == 2)
//                    {
//                        blankFiles = 1;
//                        if (y == numberOfCol - 2 && (x == 1)) continue;

//                        strengthUsed++;
//                        tempGrid = Instantiate(prefab, container).GetComponent<RectTransform>();
//                        xValue = offSet.x - (x * spacing) - (x * tempGrid.rect.width) + totalWidth / 2;
//                        yValue = offSet.y + (y * spacing) + (y * tempGrid.rect.height) - totalheight / 2;
//                        tempGrid.localPosition = new Vector3(xValue, yValue);
//                        continue;
//                    }
//                }
//                strengthUsed++;
//                tempGrid = Instantiate(prefab, container).GetComponent<RectTransform>();
//                xValue = offSet.x - (x * spacing) - (x * tempGrid.rect.width) + totalWidth / 2;
//                yValue = offSet.y + (y * spacing) + (y * tempGrid.rect.height) - totalheight / 2;
//                tempGrid.localPosition = new Vector3(xValue, yValue);
//            }
//        }

//        // Create OIC
//        tempGrid = Instantiate(prefab, container).GetComponent<RectTransform>();
//        tempGrid.GetComponent<Image>().color = Color.green;
//        xValue = offSet.x + totalWidth;
//        yValue = offSet.y;
//        tempGrid.localPosition = new Vector3(xValue, yValue);

//        for (int backRow = 0; backRow < ldStrength + additionalBackRows; backRow++)
//        {
//            tempGrid = Instantiate(prefab, container).GetComponent<RectTransform>();
//            tempGrid.GetComponent<Image>().color = Color.red;
//            xValue = offSet.x - totalWidth;
//            yValue = offSet.y + (backRow * spacing) + (backRow * tempGrid.rect.height) - ((ldStrength + additionalBackRows) * spacing + (ldStrength + additionalBackRows) * prefab.GetComponent<RectTransform>().rect.height) / 2;
//            tempGrid.localPosition = new Vector3(xValue, yValue);
//        }

//        textOverview.text = numberOfCol + " rows of " + numberOfRows + " with " + blankFiles + " blank files and " + (ldStrength + additionalBackRows) + " man left behind";
//    }

//    public void DeleteExistingParade()
//    {
//        strengthUsed = 0;
//        foreach (Transform unit in container)
//        {
//            Destroy(unit.gameObject);
//        }
//    }
//}
