using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDLibrary.Platforms.iOS
{
    public class IosShellRenderer : ShellRenderer
    {
        protected override IShellSectionRenderer CreateShellSectionRenderer(ShellSection shellSection)
        {
            var renderer = base.CreateShellSectionRenderer(shellSection);
            if (renderer != null)
            {
                var tabbarController = (renderer as ShellSectionRenderer).TabBarController;
                if (null != tabbarController)
                {
                    tabbarController.ViewControllerSelected += async (o, e) => {
                        await shellSection?.Navigation.PopToRootAsync();

                    };
                }
            }
            return renderer;
        }
    }
}
