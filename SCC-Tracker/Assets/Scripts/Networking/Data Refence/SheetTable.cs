using System;
using System.Collections.Generic;

namespace Networking.GSRef
{
    [Serializable]
    // Define table for individual sheet table
    public struct SheetTable
    {
        public string name;
        /// <summary>
        /// Id of table
        /// </summary>
        public string tableId;
        /// <summary>
        /// Id to add data into table
        /// </summary>
        public string formUrl;
        /// <summary>
        /// Entry id for each field of the form
        /// </summary>
        public List<SendDataEntryId> entryIds;

        public SheetTable(string name, string tableId, List<SendDataEntryId> entryIds)
        {
            this.name = name;
            this.tableId = tableId;
            this.entryIds = entryIds;
            this.formUrl = null;
        }
    }
}

