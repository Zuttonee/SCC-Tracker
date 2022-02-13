using System.Collections.Generic;
using UnityEngine;

namespace Networking.GSRef
{
    [CreateAssetMenu(menuName = "Networking/Google Sheet Reference")]
    public class GoogleSheetsReference : ScriptableObject
    {
        #region Public Variables
        public string host;
        public string date;
        [HideInInspector]public string sheetId;
        public List<Sheet> sheetTypes = new List<Sheet>();
        #endregion

        #region Public Property
        public string URL { get { return host + sheetId + "/export?"; } }
        #endregion
    }
}