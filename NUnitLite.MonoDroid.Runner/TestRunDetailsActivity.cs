using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Views;

namespace NUnitLite.MonoDroid
{
    /// <summary>
    /// Displays the details of a run
    /// </summary>
    public abstract class TestRunDetailsActivity: Activity, ITestListener
    {
        private TestRunInfo _testRunInfo;
        private TestRunDetailsView _detailsView;

        /// <summary>
        /// Handles the creation of the activity
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var testCaseName = Intent.GetStringExtra("TestCaseName");
            _testRunInfo = TestRunContext.Current.TestResults.First(item => item.TestCaseName == testCaseName);

            _detailsView = new TestRunDetailsView(this, _testRunInfo) { Description = DefaultDescription };
            SetContentView(_detailsView);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            menu.Add("Re-run");
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var testCase = (TestCase) _testRunInfo.TestResult.Test;

            // Start the test process in a background task
            Task.Factory.StartNew(() =>
            {
                testCase.Run(this);
            });

            return true;
        }

        public void TestStarted(ITest test)
        {
            RunOnUiThread(() => _detailsView.Description = "Running...");
        }

        public void TestFinished(TestResult result)
        {
            _testRunInfo.TestResult = result;
            RunOnUiThread(() => _detailsView.Description = DefaultDescription);
        }

        private string DefaultDescription
        {
            get
            {
                return _testRunInfo.TestResult.Message + "\r\n\r\n" + _testRunInfo.TestResult.StackTrace;
            }
        }
    }
}