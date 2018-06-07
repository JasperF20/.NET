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
            db.Products.Add(spinazie);
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
                    ProductList.ItemsSource = query.ToList();                                                                                                                           //dit moet je doen
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
            //Huidige gebruiker opvragen, die heb je hier ook nodig, misschien dus gewoon hoger zetten (als je 'm in de using zet, kan je 'm daarbuiten niet zien)
            IQueryable<User> currentUserInfo =
                from u in db.Users
                where u.Username == App.currentUser
                select u;
            User myUser = currentUserInfo.FirstOrDefault();



            //get selected item, als je dit doet met Product, kan je inderdaad .Price enzo erop aanroepen, ik krijg dan wel een error met uitvoeren
            //Invalid Cast Exception bij het selecteren van een product, en het klikken op koop artikel button
            //Product SelectedProduct = (Product)ProductList.SelectedValue;
            //Met var heb ik geen error, maar kan ik bijvoorbeeld .Price er niet op aanroepen, die ik wel nodig heb
            var SelectedProduct = ProductList.SelectedItem;
            Console.WriteLine(SelectedProduct);


            using (var db = new webshopHostEntities())
            {
                //Als het SelectedProduct niet null is, ofwel als er een geselecteerd is
                if (SelectedProduct == null)
                {
                    //Geen item geselecteerd, dit bericht
                    MessageBox.Show("Er is geen item geselecteerd");
                }
                else
                {
                    int productPrice =
                        (from product in db.Products
                        where SelectedProduct.ToString() == product.Name
                        select product.Price).SingleOrDefault();

                      //Check of de prijs van het geselecteerde product hoger is dan de credit van de huidige user
                      if (productPrice > myUser.Credit)
                      {
                          //Te laag saldo, dit bericht
                          MessageBox.Show("You dont have enough credit to purchase this item");
                      }



                     /* else
                      {
                          //Kijk of het item al in de lijst staat
                          if (!db.Inventories.Any(i => i.Name == "Banaan" || i.Name == "Appel" || i.Name == "Kiwi" || i.Name == "Rabarber" || i.Name == "Spinazie"))
                          {
                              //Voeg item toe aan de lijst, en zet het aantal op 1
                              var query =
                                   from product in db.Products
                                   where product.Stock > 0
                                   select new
                                   {
                                       Inventory.Name,
                                       Inventory.Price,
                                       Inventory.Quantity += 1
                                   };

                              //Prijs moet ook nog van de user af
                              //myuser.Credit - selectedProduct.Price


                              InventoryList.ItemsSource = query.ToList();                                                                                                                           //dit moet je doen
                          }
                          else
                          {
                              //quantity +1
                          }
                      }*/
                }

            }
        }
    }
}