using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BackTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void BackTestsSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator BackTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            string loginData = "{\"username\":\"test3\",\"password\":\"test3\"}";
            yield return RequestController.PostRequest("authentication/login", System.Text.Encoding.UTF8.GetBytes(loginData), "");
            LoginController.Code responseCode = JsonUtility.FromJson<LoginController.Code>(RequestController.GetResponseData());
            Assert.IsTrue(responseCode.code != "");


            string loginData2 = "{\"username\":\"\",\"password\":\"\"}";
            yield return RequestController.PostRequest("authentication/login", System.Text.Encoding.UTF8.GetBytes(loginData2), "");
            Assert.IsTrue(RequestController.GetResponseData().Contains("400 Bad Request"));

            string loginData3 = "{\"username\":\"xxxxxxxxxxxx\",\"password\":xxxxxxxxxxxxxx\"\"}";
            yield return RequestController.PostRequest("authentication/login", System.Text.Encoding.UTF8.GetBytes(loginData3), "");
            Assert.IsTrue(RequestController.GetResponseData().Contains("400 Bad Request"));
        }
    }
}
