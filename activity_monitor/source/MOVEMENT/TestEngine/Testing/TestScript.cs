using System;
using System.Collections.Generic;
using System.Text;

namespace Movement.TestEngine.Testing
{
    /// <summary>
    /// A test script that the test-engine should run.
    /// </summary>
    public struct TestScript
    {
        /// <summary>
        /// The ID of the test script.
        /// </summary>
        public int TestScriptID;

        /// <summary>
        /// The body of the test script.
        /// </summary>
        public string ScriptBody;

        public TestScript(
            int testScriptID,
            string scriptBody)
        {
            TestScriptID = testScriptID;
            ScriptBody = scriptBody;
        }
    }
}
