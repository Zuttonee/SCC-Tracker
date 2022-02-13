using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Networking;
using System.Globalization;

public class OverviewSetter : MonoBehaviour
{
    public TMP_InputField date;
    public GetDataGS getOverviewData;
    public GetDataGS getMcData;
    public GetDataGS getLdData;

    [Header("Inputs")]
    [Space(20f)]
    public PieChart pieChart;
    public TextMeshProUGUI totalStrength;
    public TextMeshProUGUI currentStrength;
    public TextMeshProUGUI ActiveStrength;
    [Space(20f)]
    public TextMeshProUGUI mcCount;
    public TextMeshProUGUI ldCount;
    public Transform mcContainer;
    public Transform ldContainer;
    public GameObject itemPrefab;
    [Space(20f)]
    public GameObject totalCountOBJ;
    public GameObject mcCountOBJ;
    public GameObject ldCountOBJ;
    public GameObject seeAllOBJ;
    public Transform seeAllContainer;
    public TextMeshProUGUI seeAllHeader;

    private string[,] overviewData;
    private string[,] mcData;
    private string[,] ldData;


    public DateTime SelectedDate { get { return Convert.ToDateTime(date.text); } }


    private void Awake()
    {
        getOverviewData.OnOutputDone.AddListener(SetStrengthData);
        getMcData.OnOutputDone.AddListener(SetMCData);
        getLdData.OnOutputDone.AddListener(SetLDData);
    }

    private void OnEnable()
    {
        date.text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        CloseSeeAll();
        RefreshData();
    }

    public void RefreshData()
    {
        clearDatas();
        getOverviewData.GetData(0, 0);
        getMcData.GetData(0, 1);
        getLdData.GetData(0, 2);
    }

    private void OnDisable()
    {
        clearDatas();
    }

    public void SetStrengthData()
    {
        overviewData = getOverviewData.datas;

        // Set overview data
        if (overviewData.GetLength(0) > 0)
        {
            for (int i = 1; i < overviewData.GetLength(0); i++)
            {
                if (Convert.ToDateTime(overviewData[i, 0]) != SelectedDate) continue;

                totalStrength.text = getOverviewData.datas[i, 1];
                currentStrength.text = getOverviewData.datas[i, 2];
                ActiveStrength.text = getOverviewData.datas[i, 3];

                float[] values = { int.Parse(getOverviewData.datas[i, 1]), int.Parse(getOverviewData.datas[i, 2]), int.Parse(getOverviewData.datas[i, 3]) };
                pieChart.SetValues(values);
            }
        }
    }

    public void SetMCData()
    {
        mcData = getMcData.datas;
        int mcDataCount = 0;

        // Set MC People
        if (mcData.GetLength(0) > 1)
        {
            for (int i = 1; i < mcData.GetLength(0); i++)
            {
                if (Convert.ToDateTime(mcData[i, 1]) > SelectedDate || Convert.ToDateTime(mcData[i, 2]) < SelectedDate) continue;

                mcDataCount++;
                ItemSetter itemTemp = Instantiate(itemPrefab, mcContainer).GetComponent<ItemSetter>();
                itemTemp.name.text = mcData[i, 4];
                itemTemp.durationFrom.text = mcData[i, 1];
                itemTemp.durationTo.text = mcData[i, 2];
                itemTemp.reason.text = mcData[i, 5];
            }
        }

        mcCount.text = String.Format("[{0:00}]",mcDataCount);
    }

    public void SetLDData()
    {
        ldData = getLdData.datas;
        int LdDataCount = 0;

        // Set LD People
        if (ldData.GetLength(0) > 1)
        {
            for (int i = 1; i < ldData.GetLength(0); i++)
            {
                if (Convert.ToDateTime(ldData[i, 1]) > SelectedDate || Convert.ToDateTime(ldData[i, 2]) < SelectedDate) continue;

                LdDataCount++;
                ItemSetter itemTemp = Instantiate(itemPrefab, ldContainer).GetComponent<ItemSetter>();
                itemTemp.name.text = ldData[i, 4];
                itemTemp.durationFrom.text = ldData[i, 1];
                itemTemp.durationTo.text = ldData[i, 2];
                itemTemp.reason.text = ldData[i, 5];
            }
        }

        ldCount.text = String.Format("[{0:00}]", LdDataCount);
    }

    public void McSeeAll()
    {
        SetBasicStatus(false);
        OpenSeeAll(mcData, "Medical Leaves");
    }

    public void LdSeeAll()
    {
        SetBasicStatus(false);
        OpenSeeAll(ldData, "Light Duty");
    }

    public void CloseSeeAll()
    {
        SetBasicStatus(true);
        seeAllOBJ.SetActive(false);
    }

    public void OpenSeeAll(string[,] dataSet, string header)
    {
        seeAllOBJ.SetActive(true);
        int dataCount = 0;
        seeAllHeader.text = header; 

        // Set MC People
        if (dataSet.GetLength(0) > 1)
        {
            for (int i = 1; i < dataSet.GetLength(0); i++)
            {
                if (Convert.ToDateTime(dataSet[i, 1]) > SelectedDate || Convert.ToDateTime(dataSet[i, 2]) < SelectedDate) continue;

                dataCount++;
                ItemSetter itemTemp = Instantiate(itemPrefab, seeAllContainer).GetComponent<ItemSetter>();
                itemTemp.name.text = dataSet[i, 4];
                itemTemp.durationFrom.text = dataSet[i, 1];
                itemTemp.durationTo.text = dataSet[i, 2];
                itemTemp.reason.text = dataSet[i, 5];
            }
        }

        mcCount.text = String.Format("[{0:00}]", dataCount);
    }

    private void SetBasicStatus(bool status)
    {
        totalCountOBJ.SetActive(status);
        mcCountOBJ.SetActive(status);
        ldCountOBJ.SetActive(status);
    }

    private void clearDatas()
    {
        totalStrength.text = "";
        currentStrength.text = "";
        ActiveStrength.text = "";

        foreach (Transform item in mcContainer)
        {
            Destroy(item.gameObject);
        }

        foreach (Transform item in ldContainer)
        {
            Destroy(item.gameObject);
        }
    }
}
