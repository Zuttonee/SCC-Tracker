using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

namespace Networking
{
    public abstract class WebRequests : MonoBehaviour
    {
        #region Public Variables
        [HideInInspector] public string url;

        public UnityEvent OnOutputDone;
        public UnityWebRequest webRequest;
        public WWWForm form;
        #endregion

        #region Public Functions
        // Used to send data to web
        public void SendWebRequest()
        {
            StartCoroutine(PostWebRequest());
        }

        /// <summary>
        /// Send WebRequest
        /// </summary>
        /// <returns></returns>
        public IEnumerator PostWebRequest()
        {
            UpdateForm();
            using (webRequest = UnityWebRequest.Post(url, form))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError)
                {
                    Error();
                }
                else if (webRequest.isHttpError)
                {
                    Error();

                }
                else
                {
                    Output();
                }
            }
        }
        #endregion

        #region Virtual/Abstract Functions
        // Use custom forms
        public virtual void UpdateForm()
        {
            form = new WWWForm();
        }

        // The out put of the data
        public virtual void Output()
        {
            OnOutputDone.Invoke();
        }

        public virtual void Error()
        {
            Debug.Log(webRequest.error);
            ErrorDisplay.OpenErrorMessage();
            ErrorDisplay.UpdateErrorMessage(webRequest.error);

            switch (webRequest.responseCode)
            {
                /// cannot reach host
                case 0:
                    ErrorDisplay.UpdateErrorFix("Oh no the database is offline or cannot be reach!!");
                    break;

                /// not found
                case 404:
                    ErrorDisplay.UpdateErrorFix("PHP request cannot be found!");
                    break;

                /// cannot access
                case 403:
                    ErrorDisplay.UpdateErrorFix(webRequest.error);
                    break;

                /// bad request
                case 400:
                    ErrorDisplay.UpdateErrorFix(webRequest.error);
                    break;

                /// internal error
                case 500:
                    ErrorDisplay.UpdateErrorFix(webRequest.error);
                    break;

                /// gateway timeout
                case 503:
                    ErrorDisplay.UpdateErrorFix(webRequest.error);
                    break;

                /// generic error
                default:
                    ErrorDisplay.UpdateErrorFix("Please Restart Your Browser Or Open A New Browser");
                    break;

            }
        }
        #endregion
    }
}