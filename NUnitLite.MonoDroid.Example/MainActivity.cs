using System;
using System.Collections.Generic;
using System.Reflection;
using Android.App;

namespace NUnitLite.MonoDroid.Example
{
    [Activity(Label = "NUnitLite.MonoDroid.Example", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : TestRunnerActivity
    {
        protected override IEnumerable<Assembly> GetAssembliesForTest()
        {
            yield return GetType().Assembly;
        }

        protected override Type GetDetailsActivityType
        {
            get { return typeof(DetailsActivity); }
        }
    }
}

