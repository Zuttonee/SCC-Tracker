using Networking.GSRef;

namespace Networking
{
    public class GetDataGS : WebRequests
    {
        #region Public Variables
        public GoogleSheetsReference networkReference;

        public int sheetId;
        public int tableId;
        public string[,] datas;
        #endregion

        #region Public Function
        public void SetUrl()
        {
            url = networkReference.host + networkReference.sheetTypes[sheetId].sheetId + "/export?";
        }

        public void GetData(int sheetId, int tableId)
        {
            this.sheetId = sheetId;
            this.tableId = tableId;

            SetUrl();
            SendWebRequest();
        }
        #endregion

        #region Override Functions
        public override void UpdateForm()
        {
            base.UpdateForm();
            form.AddField("format", "csv");
            form.AddField("id", networkReference.sheetId);
            form.AddField("gid", networkReference.sheetTypes[sheetId].sheetTable[tableId].tableId);
        }

        public override void Output()
        {
            string[] data = webRequest.downloadHandler.text.Split('\n');
            datas = new string[data.Length, 10];

            // Process the data into a multidimension array
            for (int x = 0; x < data.Length; x++)
            {
                string[] tempData = data[x].Split(',');

                for (int y = 0; y < tempData.Length; y++)
                {
                    datas[x, y] = tempData[y];
                }
            }

            OnOutputDone.Invoke();
        }
        #endregion
    }
}