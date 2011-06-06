using System.IO;
using System.Text;
using Android.Widget;

namespace NUnitLite.MonoDroid.Example
{
    /// <summary>Implementation of TextWriter that outputs to a TextView control.</summary>
    public class TextWriterTextView : TextWriter
    {
        readonly TextView _textView;

        public override Encoding Encoding { get { return System.Text.Encoding.UTF8; } }

        public TextWriterTextView(TextView textView) { _textView = textView; }

        public override void Write(string value) { _textView.Text = _textView.Text + value; }

        public override void WriteLine(string value)
        {
            Write(value);
            Write("\r\n");
        }
    }
}