using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayModeDataToObject
    {
        // A Test behaves as an ordinary method
        [Test]
        public void PlayModeDataToObjectSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator PlayModeDataToObjectWithEnumeratorPasses()
        {
            UISystem.instance.sessionController.GetAllTasks();
            yield return UISystem.instance.sessionController.GetAllTasks();
            Assert.IsNotNull(UISystem.instance.sessionController.taskLists);
        }
    }
}
