using Xamarin.Forms;

namespace Typescale
{
    public partial class CustomLabel : Label
    {
        public static readonly BindableProperty LineHeightProperty = BindableProperty.Create(
            nameof(LineHeight), 
            typeof(float), 
            typeof(CustomLabel), 
            default(float));

        public float LineHeight
        {
            get { return (float)GetValue(LineHeightProperty); }
            set { SetValue(LineHeightProperty, value); OnPropertyChanged(); }
        }

        public CustomLabel()
        {
            //needed to create custom Label to allow for a renderer
        }
    }
}