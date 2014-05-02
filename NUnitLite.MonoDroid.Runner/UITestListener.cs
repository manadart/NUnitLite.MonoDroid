using System;
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
        private readonly IBaseAdapter _adapter;

        /// <summary>
        /// Initializes a new instance <see cref="UITestListener"/>
        /// </summary>
        public UITestListener(IBaseAdapter adapter)
        {
            // Create a new thread handler for the main looper.
            // This handler is used to post code from the background thread
            // back to the UI thread.
            _threadHandler = new Handler(Application.Context.MainLooper);

            _adapter = adapter;
        }

        /// <summary>
        /// Handles when a test is started
        /// </summary>
        public void TestStarted(ITest test)
        {
            _threadHandler.Post(() =>
            {
                var testRunInfo = TestRunContext.Current.FindOrAddTestRunInfo(test);
                testRunInfo.Running = true;
                testRunInfo.Passed = false;

                _adapter.NotifyDataSetInvalidated();
                _adapter.NotifyDataSetChanged();
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
                var testRunInfo = TestRunContext.Current.FindOrAddTestRunInfo(result.Test);
                testRunInfo.Passed = result.IsSuccess;
                testRunInfo.Running = false;
                testRunInfo.TestResult = result;

                _adapter.NotifyDataSetInvalidated();
                _adapter.NotifyDataSetChanged();
            });
        }

    }
}