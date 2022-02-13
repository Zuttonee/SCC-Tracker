using System;
using System.Collections.Generic;

namespace Networking.GSRef
{
    [Serializable]
    // Define sheet for the whole table
    public struct Sheet
    {
        public string name;
        /// <summary>
        /// Id of all the sheet
        /// </summary>
        public string sheetId;
        /// <summary>
        /// Data of all the table
        /// </summary>
        public List<SheetTable> sheetTable;

        public Sheet(string name, string sheetId, List<SheetTable> sheetTable)
        {
            this.name = name;
            this.sheetId = sheetId;
            this.sheetTable = sheetTable;
        }
    }
}