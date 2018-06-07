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

        public void AddStartUpProducts()
        {
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
                Stock = 100,
                InventoryId = 1
            };
            var spinazie = new Product
            {
                Product_ID = 5,
                Price = 9000,
                Name = "Spinazie",
                Stock = 0,
                InventoryId = 1
            };
            db.Products.Add(appel);
            db.Products.Add(banaan);
            db.Products.Add(kiwi);
            db.Products.Add(rabarber);
            db.SaveChanges();
            MessageBox.Show("PRODUCTS ADDED ");
        }


        public MainWindow()
        {
            InitializeComponent();

            using (var db = new webshopHostEntities())
            {
                // dit snap ik niet helemaal
                // gaat niet helemaal lekker met foreign key als je hier new user en new invtory weghaald
                // is hier in principe niet nodig
                User admin = new User
                {
                    User_ID = 3,
                    Username = "admin",
                    Password = "admin"
                };
              
                var userInventory = new Inventory
                    {
                        Id = 1,
                        User = admin
                };
          
                db.Inventories.Add(userInventory);
                db.SaveChanges();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new webshopHostEntities())
                {
                // begin met lege tabel
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Products]");

                //producten alleen toevoegen als ze nog niet bestaan
                if (!db.Products.Any(p => p.Name == "Banaan" || p.Name == "Appel" || p.Name == "Kiwi" || p.Name == "Rabarber" || p.Name == "Spinazie"))
                {
                    AddStartUpProducts();
                    var query =
                         from product in db.Products
                         where product.Stock > 0
                         select new
                         {
                             product.Name,
                             product.Price,
                             product.Stock
                         };
                    ProductList.ItemsSource = query.ToList();
                }

                //huidige gebruiker opvragen
                IQueryable<User> currentUserInfo =
                    from u in db.Users
                    where u.Username == App.currentUser 
                    select u;
                User myUser = currentUserInfo.FirstOrDefault();

                // show users money in box 
                moneyBox.Text = myUser.Credit.ToString();
            }
        }

        private void Koop_button(object sender, RoutedEventArgs e)
        {
            //get selected item
            Product SelectedProduct = (Product)ProductList.SelectedItem;

            // add product to users inventory
            //Product.stock -1 doen 
            //User.saldo - product.price doen
            // update statements gebruiken hiervoor

            using (var db = new webshopHostEntities())
            {
            }
        }
    }
}
