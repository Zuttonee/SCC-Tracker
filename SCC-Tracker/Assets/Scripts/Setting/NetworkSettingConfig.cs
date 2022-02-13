using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Networking.GSRef;

public class NetworkSettingConfig : MonoBehaviour
{
    public GoogleSheetsReference gsReference;

    [Header("Input Fields")]
    public TMP_InputField sheetNameIF;
    public TMP_InputField sheetIDIF;
    public Transform sheetTableContainer;
    public GameObject sheetTablePrefab;

    private List<SheetTableConfig> sheetTableList = new List<SheetTableConfig>();

    private void OnEnable()
    {
        // Setup Data Field;
        sheetNameIF.text = gsReference.sheetTypes[0].name;
        sheetIDIF.text = gsReference.sheetTypes[0].sheetId;
        for (int i = 0; i < gsReference.sheetTypes[0].sheetTable.Count; i++)
        {
            SheetTableConfig tempConfig = Instantiate(sheetTablePrefab, sheetTableContainer).GetComponent<SheetTableConfig>();
            tempConfig.header.text = gsReference.sheetTypes[0].sheetTable[i].name;
            tempConfig.tableId.text = gsReference.sheetTypes[0].sheetTable[i].tableId;
            sheetTableList.Add(tempConfig);
        }
        Canvas.ForceUpdateCanvases();
    }

    private void OnDisable()
    {
        // Save All table in a sheet
        List<SheetTable> tempSheetTables = new List<SheetTable>();
        for (int i = 0; i < sheetTableList.Count; i++)
        {
            SheetTable tempSheetTable = new SheetTable(sheetTableList[i].header.text, sheetTableList[i].tableId.text, new List<SendDataEntryId>());
            tempSheetTables.Add(tempSheetTable);
        }

        // Save the Sheet data
        List<Sheet> tempSheets = new List<Sheet>();
        Sheet tempSheet = new Sheet(sheetNameIF.text, sheetIDIF.text, tempSheetTables);
        tempSheets.Add(tempSheet);

        gsReference.sheetTypes[0] = tempSheet;

        for (int i = 1; i < sheetTableContainer.childCount; i++)
        {
            Destroy(sheetTableContainer.GetChild(i).gameObject);
        }

        sheetTableList.Clear();
    }
}
