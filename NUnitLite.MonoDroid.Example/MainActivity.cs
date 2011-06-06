using System;
using System.IO;
using Android.App;
using Android.Graphics;
using Android.Widget;
using Android.OS;
using NUnitLite.Runner;

namespace NUnitLite.MonoDroid.Example
{
    [Activity(Label = "NUnitLite.MonoDroid.Example", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MainActivity);
            var textView = FindViewById<TextView>(Resource.Id.lblTestProgress);

            try
            {
                TextWriter writer = new TextWriterTextView(textView);
                textView.SetTextColor(new TextUI(writer).Execute(new string[0]) ? Color.Green : Color.Red);
            }
            catch (Exception ex)
            {
                textView.SetTextColor(Color.Red);
                WriteProgressMessage(ex.ToString());
            }       
        }

        private void WriteProgressMessage(string message)
        {
            var testProgress = FindViewById<TextView>(Resource.Id.lblTestProgress);
            testProgress.Text = testProgress.Text + message + "\r\n";
        }
    }
}

