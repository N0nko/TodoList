using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayModeHistogram
    {
        // A Test behaves as an ordinary method
        [Test]
        public void PlayModeHistogramSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator PlayModeHistogramWithEnumeratorPasses()
        {
            List<ListModel> taskLists = new List<ListModel>();


            string code;
            string accessToken;
            var login = new LoginController.LoginForm("test3", "test3");
            var f = JsonUtility.ToJson(login);
            var bytes = System.Text.Encoding.UTF8.GetBytes(f);
            Debug.Log(f);
       
            yield return RequestController.PostRequest("authentication/login", bytes, "");

            string jsonCode = RequestController.GetResponseData();
            Assert.IsNotNull(jsonCode);
            try
            {
                code = JsonUtility.FromJson<LoginController.Code>(jsonCode).code;
            }
            catch
            {
                Debug.LogWarning("Failed to login");
                yield break;
            }

        
            LoginController.TokenForm t = new LoginController.TokenForm("authorization_code", code);
            var tok = JsonUtility.ToJson(t);
            var tokenBytes = System.Text.Encoding.UTF8.GetBytes(tok);
            yield return RequestController.PostRequest("token", tokenBytes, "");

            string jsonToken = RequestController.GetResponseData();
            accessToken = JsonUtility.FromJson<LoginController.Token>(jsonToken).accessToken;


            Assert.IsTrue(taskLists.Count == 0);

            yield return RequestController.GetRequest("list", accessToken);
            var data = RequestController.GetResponseData();
            Debug.Log(data);
            Assert.IsNotNull(data);
            ListsModel lists = JsonUtility.FromJson<ListsModel>(data);
            Debug.Log(lists.count);

            List<ListModel> answer = new List<ListModel>();
            for (int i = 0; i < lists.count; i++)
            {
                answer.Add(lists.rows[i]);
                Debug.Log(lists.rows[i]);
            }
            taskLists = answer;

            Assert.IsFalse(taskLists.Count == 0);

       
            
        }
      
    }
}
