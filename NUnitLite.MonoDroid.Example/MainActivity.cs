using System;
using Android.App;
using Android.Widget;
using Android.OS;

namespace NUnitLite.MonoDroid.Example
{
    [Activity(Label = "NUnitLite.MonoDroid.Example", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MainActivity);
            var testOutput = FindViewById<TextView>(Resource.Id.lblTestProgress);

            try
            {
                Runner.Run(testOutput, new[]{"NUnitLite.MonoDroid.Example"}); 
            }
            catch (Exception ex)
            {
                testOutput.Text = ex.ToString();
            }
            
        }
    }
}

