using System;
using Android.Content;
using Typescale;
using Typescale.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer (typeof(CustomLabel), typeof(LabelLineHeight))]
namespace Typescale.Droid.Renderers
{
    public partial class LabelLineHeight : LabelRenderer
    {
        public LabelLineHeight(Context context) : base(context)
        {
        }

        protected CustomLabel label { get; private set; }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if( Control != null)
            {
                this.label = (CustomLabel)this.Element;
            }

            var lineSpacing = this.label.LineHeight;
            if (Math.Abs(lineSpacing) > 0)
            {
                this.Control.SetLineSpacing(lineSpacing, 1f);
            }
            this.UpdateLayout();
        }
    }
}
