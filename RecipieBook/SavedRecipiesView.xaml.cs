using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace RecipieBook
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SavedRecipiesView : Page
    {
        public SavedRecipiesView()
        {
            this.InitializeComponent();
            ReadSaved();
        }

        public void ReadSaved()
        {
            var savedRecipes = ButtonControl.recepies;
            string recipies = "";

            foreach (var recipe in savedRecipes)
            {
                recipies += recipe + "\n";
            }
            contentFrame.Content = recipies;           
        }
    }
}
