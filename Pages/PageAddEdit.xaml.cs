using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DemoExamApp.Classes;

namespace DemoExamApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAddEdit.xaml
    /// </summary>
    public partial class PageAddEdit : Page
    {
        //новое поле, которое будет хранить в себе экземпляр добавляемого продукта 

        private Product _currentProduct = new Product();

        public PageAddEdit(Product product)
        {
            InitializeComponent();
            CmbProductCategory.ItemsSource = TradeEntities.GetContext().
                Product.Select(x => x.ProductCategory).Distinct().ToList();
            CmbProductManufacturer.ItemsSource = TradeEntities.GetContext().Product.
                Select(x => x.ProductManufacturer).Distinct().ToList();
            CmbProductEdin.Items.Add("шт");
            CmbProductEdin.Items.Add("кг");
            CmbProductEdin.Items.Add("куб.м");
            CmbProductEdin.Items.Add("кв.м");
            CmbProductEdin.Items.Add("мм");
            CmbProductEdin.Items.Add("м");
            CmbProductEdin.Items.Add("л");

            if (product != null)
                _currentProduct = product;
            //создаем контекст
            DataContext = _currentProduct;

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder(); //объект для сообщения об ошибке

            //проверка полей объекта
            //if (string.IsNullOrWhiteSpace(_currentProduct.FirstName))
            //    error.AppendLine("Укажите имя");
            //if (string.IsNullOrWhiteSpace(_currentUser.LastName))
            //    error.AppendLine("Укажите фамилию");
            //if (error.Length > 0)
            //{
            //    MessageBox.Show(error.ToString());
            //    return;
            //}
            //если продукт новый
            if (TradeEntities.GetContext().Product.Find(_currentProduct.ProductArticleNumber) == null)
                TradeEntities.GetContext().Product.Add(_currentProduct); //добавить в контекст
            try
            {
                TradeEntities.GetContext().SaveChanges(); // сохранить изменения
                                                          
                MessageBox.Show("Данные сохранены");
                ClassFrame.frame.Navigate(new PageListProduct());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
