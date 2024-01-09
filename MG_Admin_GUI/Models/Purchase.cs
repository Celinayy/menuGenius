using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace MG_Admin_GUI.Models
{

    public class Purchase : INotifyPropertyChanged
    {
        private int _id;
        public int id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(id));
                }
            }
        }

        private DateTime _date_time;
        public DateTime date_time
        {
            get { return _date_time; }
            set
            {
                if (value != _date_time)
                {
                    _date_time = value;
                    OnPropertyChanged(nameof(date_time));
                }
            }
        }

        private int _total_pay;
        public int total_pay
        {
            get { return _total_pay; }
            set
            {
                if (value != _total_pay)
                {
                    _total_pay = value;
                    OnPropertyChanged(nameof(total_pay));
                }
            }
        }

        private string _status;
        public string status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(status));
                    //OnPropertyChanged(nameof(StatusAsString));
                }
            }
        }

        private bool _paid;
        public bool paid
        {
            get { return _paid; }
            set
            {
                if (_paid != value)
                {
                    _paid = value;
                    OnPropertyChanged(nameof(paid));
                }
            }
        }

        private User _purchaseUser;
        public User purchaseUser
        {
            get { return _purchaseUser; }
            set
            {
                if (_purchaseUser != value)
                {
                    _purchaseUser = value;
                    OnPropertyChanged(nameof(purchaseUser));
                }
            }
        }

        private Desk _purchaseDesk;
        public Desk purchaseDesk
        {
            get { return _purchaseDesk; }
            set
            {
                if (value != _purchaseDesk)
                {
                    _purchaseDesk = value;
                    OnPropertyChanged(nameof(purchaseDesk));
                }
            }
        }

        private List<KeyValuePair<Product, int>> _purchaseProducts;
        public List<KeyValuePair<Product, int>> purchaseProducts
        {
            get { return _purchaseProducts; }
            set
            {
                if (_purchaseProducts != value)
                {
                    _purchaseProducts = value;
                    OnPropertyChanged(nameof(purchaseProducts));
                }
            }
        }

        public Purchase(MySqlDataReader reader)
        {
            id = reader.GetInt32("id");
            date_time = reader.GetDateTime("date_time");
            total_pay = reader.GetInt32("total_pay");
            purchaseProducts = GetPurchasedProducts(reader.GetInt32("id"));
            //status = Enum.TryParse(reader.GetString("status"), out OrderStatus parsedStatus) ? parsedStatus : OrderStatus.ordered;
            status = reader.GetString("status");
            paid = reader.GetBoolean("paid");
            purchaseUser = GetUserForPurchase(reader.GetInt32("user_id"));
            purchaseDesk = GetDeskForPurchase(reader.GetInt32("desk_id"));
        }

        public static ObservableCollection<Purchase> GetPurchases()
        {
            ObservableCollection<Purchase> Purchases = new ObservableCollection<Purchase>();
            using (var connection = DatabaseHandler.OpenConnection())
            {
                var query = "SELECT * FROM purchases";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Purchases.Add(new Purchase(reader));
                        }
                    }
                }
            }
            return Purchases;
        }

        private List<KeyValuePair<Product, int>> GetPurchasedProducts(int purchaseId)
        {
            List<KeyValuePair<Product, int>> productList = new List<KeyValuePair<Product, int>>();
            using (var connection = DatabaseHandler.OpenConnection())
            {
                var query = "SELECT p.*, pp.quantity FROM products p INNER JOIN product_purchase pp ON p.id = pp.product_id WHERE pp.purchase_id = @purchaseId";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@purchaseId", purchaseId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product(reader);
                            int quantity = reader.GetInt32("quantity");

                            productList.Add(new KeyValuePair<Product, int>(product, quantity));
                        }
                    }
                }
            }
            return productList;
        }

        public User GetUserForPurchase(int userId)
        {
            User user = null;
            using (var connection = DatabaseHandler.OpenConnection())
            {
                var query = "SELECT * FROM users WHERE id = @userId";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = new User(reader);
                        }
                    }
                }
            }
            return user;
        }

        public static Desk GetDeskForPurchase(int deskId)
        {
            Desk desk = null;
            using (var connection = DatabaseHandler.OpenConnection())
            {
                var query = "SELECT * FROM desks WHERE id = @deskId";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@deskId", deskId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            desk = new Desk(reader);
                        }
                    }
                }
            }
            return desk;
        }


        //public string StatusAsString
        //{
        //    get { return OrderStatusLocalization.GetLocalizedStatus(status); }
        //}

        //public enum OrderStatus
        //{
        //    ordered,
        //    cooked,
        //    served
        //}

        //public class OrderStatusLocalization
        //{
        //    private static readonly Dictionary<OrderStatus, string> LocalizedStatuses = new Dictionary<OrderStatus, string>();

        //    static OrderStatusLocalization()
        //    {
        //        foreach (OrderStatus status in Enum.GetValues(typeof(OrderStatus)))
        //        {
        //            string key = status.ToString();
        //            string value = ConfigurationManager.AppSettings[key];
        //            LocalizedStatuses.Add(status, value);
        //        }
        //    }

        //    public static string GetLocalizedStatus(OrderStatus status)
        //    {
        //        return LocalizedStatuses.TryGetValue(status, out string localizedStatus) ? localizedStatus : status.ToString();
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
