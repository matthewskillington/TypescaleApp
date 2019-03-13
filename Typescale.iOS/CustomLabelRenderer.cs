using Foundation;
using Typescale;
using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Typescale.iOS.Renderers;

[assembly: ExportRenderer(typeof(CustomLabel), typeof(LabelLineHeight))]

namespace Typescale.iOS.Renderers
{

    public partial class LabelLineHeight : LabelRenderer
    {

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            //Sets LineHeight when Text LineHeight property changes
            if (e.PropertyName == CustomLabel.LineHeightProperty.PropertyName)
            {
                ApplyLineHeight();
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            //Sets LineHeight on Text initialisation
            if (e.OldElement == null)
            {
                ApplyLineHeight();
            }
        }

        private void ApplyLineHeight()
        {
            //get ios specific string with attributes
            var labelString = new NSMutableAttributedString(Control.AttributedText);

            var paragraphStyle = new NSMutableParagraphStyle
            {
                LineSpacing = (nfloat)((CustomLabel)Element).LineHeight - (nfloat)((CustomLabel)Element).FontSize,
        };

            var range = new NSRange(0, labelString.Length);

            if (paragraphStyle.LineSpacing > 0)
            {
                labelString.AddAttribute(UIStringAttributeKey.ParagraphStyle, paragraphStyle, range);

                Control.AttributedText = labelString;
            }
        }
    }
}