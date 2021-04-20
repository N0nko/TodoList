using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BackendTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void BackendTestSimplePasses()
        {
            // Use the Assert class to test conditions
            
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator BackendTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            string loginData = "{username:\"test3\",password:\"test3\"}";
            yield return RequestController.PostRequest("authentication/login", System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(loginData)), "");
            LoginController.Code responseCode = JsonUtility.FromJson<LoginController.Code>(RequestController.GetResponseData());
            Assert.IsTrue(responseCode.code != "");

            yield return null;
            
        }
    }
    class LoginResponse { }
}
