using CDLibrary.Entities;
using CDLibrary.Pages;
using CDLibrary.Repositories;
using Index = CDLibrary.Pages.Index;

namespace CDLibrary
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Index", typeof(Index));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        //protected override void OnNavigating(ShellNavigatingEventArgs args)
        //{
        //    base.OnNavigating(args);

        //    try
        //    {
        //        var x = Shell.Current.CurrentItem;
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    if (args.Source == ShellNavigationSource.ShellSectionChanged)
        //    {
        //        var navigation = Shell.Current.Navigation;
        //        var pages = navigation.NavigationStack;
        //        for (var i = pages.Count - 1; i >= 1; i--)
        //        {
        //            navigation.RemovePage(pages[i]);
        //        }
        //    }
        //}



    }
}
