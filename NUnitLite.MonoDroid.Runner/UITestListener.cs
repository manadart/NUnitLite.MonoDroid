using System;
using System.Linq;
using Android.App;
using Android.OS;

namespace NUnitLite.MonoDroid
{
    /// <summary>
    /// Test listener that handles tests results on the user interface.
    /// </summary>
    public class UITestListener : ITestListener
    {
        private readonly Handler _threadHandler;
        private readonly TestResultsListAdapter _listAdapter;

        /// <summary>
        /// Initializes a new instance <see cref="UITestListener"/>
        /// </summary>
        /// <param name="listAdapter"></param>
        public UITestListener(TestResultsListAdapter listAdapter)
        {
            // Create a new thread handler for the main looper.
            // This handler is used to post code from the background thread
            // back to the UI thread.
            _threadHandler = new Handler(Application.Context.MainLooper);

            _listAdapter = listAdapter;
        }

        /// <summary>
        /// Handles when a test is started
        /// </summary>
        /// <param name="test"></param>
        public void TestStarted(ITest test)
        {
            _threadHandler.Post(() =>
            {
                var testRunInfo = FindOrAddTestRunInfo(test);
                testRunInfo.Running = true;
                testRunInfo.Passed = false;

                _listAdapter.NotifyDataSetInvalidated();
                _listAdapter.NotifyDataSetChanged();
            });
        }

        /// <summary>
        /// Handles the outcome of a test case
        /// </summary>
        /// <param name="result">The result.</param>
        public void TestFinished(TestResult result)
        {
            _threadHandler.Post(() =>
            {
                var testRunInfo = FindOrAddTestRunInfo(result.Test);
                testRunInfo.Passed = result.IsSuccess;
                testRunInfo.Running = false;
                testRunInfo.TestResult = result;

                _listAdapter.NotifyDataSetInvalidated();
                _listAdapter.NotifyDataSetChanged();
            });
        }

        private static TestRunInfo FindOrAddTestRunInfo(ITest test)
        {
            var testRunItem =
                test.FullName != null
                    ? TestRunContext.Current.TestResults
                        .FirstOrDefault(item => item.TestCaseName == test.FullName)
                    : TestRunContext.Current.TestResults
                        .FirstOrDefault(item => item.Description == test.Name);
            if (testRunItem == null)
            {
                testRunItem = new TestRunInfo
                {
                    Description = test.Name,
                    TestCaseName = test.FullName,
                    IsTestSuite = test is TestSuite
                };
                TestRunContext.Current.TestResults.Add(testRunItem);
            }
            return testRunItem;
        }
    }
}