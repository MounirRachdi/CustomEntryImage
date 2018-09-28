using CustomEntryImage.CustomControl;
using CustomEntryImage.UWP;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace CustomEntryImage.UWP
{
    public class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
               // Control.Background = new SolidColorBrush(Colors.Beige);
               // Control.BorderThickness =  new Windows.UI.Xaml.Thickness(0);
            }
        }
        
    }
}
