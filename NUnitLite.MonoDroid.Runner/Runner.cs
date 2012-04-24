using System;
using System.IO;
using Android.Graphics;
using Android.Widget;
using NUnitLite.Runner;

namespace NUnitLite.MonoDroid
{
    public static class Runner
    {
        /// <summary>Run all tests contained in the input assemblies and display the results in the input TextView.</summary>
        /// <param name="outputTarget"></param>
        /// <param name="assemblyNames"></param>
        public static void Run(TextView outputTarget, string[] assemblyNames)
        {
            try
            {
                TextWriter writer = new TextWriterTextView(outputTarget);
                outputTarget.SetTextColor(new TextUI(writer).Execute(assemblyNames) ? Color.Green : Color.Red);
            }
            catch (Exception ex)
            {
                outputTarget.SetTextColor(Color.Red);
                outputTarget.Text = outputTarget.Text + ex + "\r\n";
            }
        }
    }
}