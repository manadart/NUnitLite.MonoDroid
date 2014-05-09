using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using NUnitLite.Runner;

namespace NUnitLite.MonoDroid
{
    /// <summary>
    /// Derive from this activity to create a standard test runner activity in your app.
    /// </summary>
    public abstract class TestRunnerExpandableActivity : ExpandableListActivity
    {
        private TestResultsExpandableListAdapter _testResultsAdapter;

        /// <summary>
        /// Handles the creation of the activity
        /// </summary>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _testResultsAdapter = new TestResultsExpandableListAdapter(this);
            SetListAdapter(_testResultsAdapter);

            RunTests();
        }

        protected override void OnResume()
        {
            base.OnResume();

            _testResultsAdapter.NotifyDataSetInvalidated();
            _testResultsAdapter.NotifyDataSetChanged();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            menu.Add("Re-run");
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            RunTests();
            return true;
        }

        /// <summary>
        /// Handles list item click
        /// </summary>
        public override bool OnChildClick(ExpandableListView parent, View v, int groupPosition, int childPosition, long id)
        {
            var testRunItem = TestRunContext.Current.GetGroupChild(groupPosition, childPosition);

            if (testRunItem.Running)
            {
                Toast.MakeText(this, "This test is still running.", ToastLength.Short).Show();
                return true;
            }

            var intent = new Intent(this, GetDetailsActivityType);
            intent.PutExtra("TestCaseName", testRunItem.TestCaseName);

            StartActivity(intent);

            return true;
        }

        /// <summary>
        /// Retrieves a list of assemblies that contain test cases to execute using the test runner activity.
        /// </summary>
        /// <returns>Returns the list of assemblies to test</returns>
        protected abstract IEnumerable<Assembly> GetAssembliesForTest();

        /// <summary>
        /// Gets the type of activity to use for displaying test details
        /// </summary>
        protected abstract Type GetDetailsActivityType { get; }

        private void RunTests()
        {
            var testAssemblies = GetAssembliesForTest();
            var testAssemblyEnumerator = testAssemblies.GetEnumerator();
            var testRunner = new TestRunner();

            _testResultsAdapter.NotifyDataSetInvalidated();
            _testResultsAdapter.NotifyDataSetChanged();

            // Add a test listener for the test runner
            testRunner.AddListener(new UITestListener((TestResultsExpandableListAdapter)ExpandableListAdapter));

            // Start the test process in a background task
            Task.Factory.StartNew(() =>
            {
                while (testAssemblyEnumerator.MoveNext())
                {
                    try
                    {
                        var assembly = testAssemblyEnumerator.Current;
                        testRunner.Run(assembly);
                    }
                    catch (Exception ex)
                    {
                        ShowErrorDialog(ex);
                    }
                }
            });
        }

        /// <summary>
        /// Displays an error dialog in case a test run fails
        /// </summary>
        private void ShowErrorDialog(Exception exception)
        {
            RunOnUiThread(() => new AlertDialog.Builder(this)
                .SetTitle("Failed to execute unit-test suite")
                .SetMessage(exception.ToString())
                .Create()
                .Show());
        }
    }
}