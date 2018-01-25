using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MuscleTrainingRecords00
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GraphPage : ContentPage
    {


        public GraphPage()
        {
            InitializeComponent();

            today.Text = DateTime.Today.ToString("yyyy/MM/dd");

        }
        //DateTime yyyymmdd;//追加







        /********************ここから追加******************************************/

        async Task Handle_ClickedAsync(object sender, System.EventArgs e)
        {
            try
            {


                if (bWeight.Text == null || bFat.Text == null)
                {
                    DisplayAlert("", "数値を入力してください", "OK");

                }

                var db = TodoItemDatabase.getDatabase();
                //String sName = eName.Text;
                //String sNotes = eNotes.Text;
                //Boolean bDone = eDone.IsToggled;
                int B_Weight = int.Parse(bWeight.Text);
                int B_Fat = int.Parse(bFat.Text);
                DateTime dCreated = DateTime.Today.AddDays(+3);


                TodoItem sameDateItem = await db.GetItemByCreatedAsync(dCreated);
                if (sameDateItem == null)
                {
                    TodoItem item = new TodoItem() { Created = dCreated, Bweight = B_Weight, Bfat = B_Fat };
                    await db.SaveItemAsync(item);
                    await DisplayAlert("", "記録されました:" + item.Created, "OK");
                }
                else
                {
                    await db.DeleteItemAsync(sameDateItem);
                    TodoItem item = new TodoItem() { Created = dCreated, Bweight = B_Weight, Bfat = B_Fat };
                    await db.SaveItemAsync(item);
                    await DisplayAlert("", "更新されました:{" + sameDateItem.Created + "}→{" + item.Created + "}", "OK");

                }

                Application.Current.MainPage = new MainPage();
            }
            catch (Exception)
            {
                DisplayAlert("", "数値を入力してください", "OK");
            }

            /*private void eCreated_DateSelected(object sender, DateChangedEventArgs e)//追加
            {
                yyyymmdd = new DateTime(eCreated.Date.Year, eCreated.Date.Month, eCreated.Date.Day);
            }*/
        }
    }
}