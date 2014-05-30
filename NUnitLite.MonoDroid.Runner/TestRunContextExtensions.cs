using System;
using System.Linq;
using Android.Runtime;

namespace NUnitLite.MonoDroid
{
    /// <summary>
    /// Extensions to <see cref="TestRunContext"/>.
    /// These handle the fact that the context is flat but we want some structure
    /// for the expandable list.
    /// </summary>
    public static class TestRunContextExtensions
    {
        /// <summary>
        /// Get the test that represents the group level
        /// </summary>
        public static TestRunInfo GetGroupParent(this TestRunContext context, int groupPosition)
        {
            return context.TestResults
                .Where(t => t.IsClass)
                .Skip(groupPosition)
                .First();
        }

        /// <summary>
        /// Get the number of groups
        /// </summary>
        public static int GetGroupCount(this TestRunContext context)
        {
            return context.TestResults.Count(t => t.IsClass);
        }

        /// <summary>
        /// Get the children inside the group
        /// </summary>
        public static JavaList<TestRunInfo> GetGroupChildren(this TestRunContext context, int groupPosition)
        {
            // skip the required number of group levels
            var groupCount = 0;
            var group = context.TestResults.SkipWhile(t =>
            {
                if (t.IsClass && ++groupCount > groupPosition)
                {
                    return false;
                }
                return true;
            });

            // skip the group level that we stopped at and then keep taking while we're
            // still getting children
            var children = group.Skip(1).TakeWhile(t => !t.IsTestSuite);
            return new JavaList<TestRunInfo>(children);
        }

        /// <summary>
        /// Get the child for the specified <paramref name="groupPosition"/> and <paramref name="childPosition"/>
        /// </summary>
        public static TestRunInfo GetGroupChild(this TestRunContext context, int groupPosition, int childPosition)
        {
            return context.GetGroupChildren(groupPosition)[childPosition];
        }

        /// <summary>
        /// Find a matching test in the context. If not found then add it.
        /// </summary>
        public static TestRunInfo FindOrAddTestRunInfo(this TestRunContext context, ITest test)
        {
            var testRunItem =
                test.FullName != null
                    ? context.TestResults.FirstOrDefault(item => item.TestCaseName == test.FullName)
                    : context.TestResults.FirstOrDefault(item => item.Description == test.Name);
            if (testRunItem == null)
            {
                testRunItem = new TestRunInfo
                {
                    Description = test.Name,
                    TestCaseName = test.FullName,
                    IsTestSuite = test is TestSuite,
                    IsIgnored = test.IgnoreReason != null
                };
                context.TestResults.Add(testRunItem);
            }
            return testRunItem;
        }

    }
}