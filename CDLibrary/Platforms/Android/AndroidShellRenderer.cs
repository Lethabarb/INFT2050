using Android.Content;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDLibrary.Platforms.Android
{
    internal class AndroidShellRenderer : ShellRenderer
    {
        public AndroidShellRenderer(Context context) : base(context)
        {
        }
        protected override IShellItemRenderer CreateShellItemRenderer(ShellItem shellItem)
        {
            return new AndroidShellItemRenderer(this);
        }
    }
    public class AndroidShellItemRenderer : ShellItemRenderer
    {
        public AndroidShellItemRenderer(IShellContext shellContext) : base(shellContext)
        {
        }

        /// <summary>  
        /// Pops to root when the selected tab is pressed.  
        /// </summary>  
        /// <param name="shellSection"></param>  
        protected override void OnTabReselected(ShellSection shellSection)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await shellSection?.Navigation.PopToRootAsync();
            });
        }
    }
}
