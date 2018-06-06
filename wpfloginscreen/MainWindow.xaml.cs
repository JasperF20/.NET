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
using System.Data.Entity.Validation;
using System.Data.Entity.Core.Objects;

namespace wpfloginscreen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        webshopHostEntities db = new webshopHostEntities();

        public MainWindow()
        {
            InitializeComponent();

            using (var db = new webshopHostEntities())
            {
                //maak standaard een paar producten aan
                var appel = new Product
                {
                    Product_ID = 1,
                    Price = 2,
                    Name = "Appel",
                    Stock = 10,
                    InventoryId = 1
                };
                var banaan = new Product
                {
                    Product_ID = 2,
                    Price = 1,
                    Name = "Banaan",
                    Stock = 6,
                    InventoryId = 1
                };
                var kiwi = new Product
                {
                    Product_ID = 3,
                    Price = 105,
                    Name = "Kiwi",
                    Stock = 1,
                    InventoryId = 1
                };
                var rabarber = new Product
                {
                    Product_ID = 4,
                    Price = 9000,
                    Name = "Rabarber",
                    Stock = 0,
                    InventoryId = 1
                };



                User admin = new User
                {
                    User_ID = 3,
                    Username = "admin",
                    Password = "admin"
                };

                // dit snap ik niet helemaal
                // gaat niet helemaal lekker met foreign key als je hier new user en new invtory weghaald
                // is hier in principe niet nodig
                var userInventory = new Inventory
                    {
                        Id = 1,
                        User = admin
                    };
                  

                db.Products.Add(appel);
                db.Products.Add(banaan);
                db.Products.Add(kiwi);
                db.Products.Add(rabarber);
                db.Inventories.Add(userInventory);
                db.SaveChanges();
            }
        }

        // TODO: query aanpassen zodat de stock property ook werkt (toevoegen bij product)
        // en deze duplicates bij elkaar opteld en niet dubbel laten zien in de lijst
        // 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // laat producten zien in de lijst on window load
            using (var db = new webshopHostEntities())
                {
                var query =
                from product in db.Products
                where product.Stock > 0
                select new { product.Name, product.Price, product.Stock };

                ProductList.ItemsSource = query.ToList();
                //get logged in user

                // show money in box moneyBox.Text = currentUser.Credit;
                moneyBox.Text = "10";
                

            }
        }

        private void Koop_button(object sender, RoutedEventArgs e)
        {
            //get selected item
            Product SelectedProduct = (Product)ProductList.SelectedItem;

            // add product to users inventory
            //Product.stock -1 doen 
            //User.saldo - product.price doen

            using (var db = new webshopHostEntities())
            {

            }

           


        }
    }
}



/*
 *                 var banaan = new Product
                {
                    UserUser_ID = 1,
                    Product_ID = 2,
                    Price = 1,
                    Name = "Banaan",
                    InventoryId = 1
                };
 * */
