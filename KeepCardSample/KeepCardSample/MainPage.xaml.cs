using Microsoft.Maui.Controls;
using Syncfusion.Maui.Kanban;
using System.Collections.ObjectModel;
using SampleBrowser.Maui.Base.Converters;
using System.Reflection;

namespace KeepCardSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnKanbanCardDragEnd(object sender, KanbanDragEndEventArgs e)
        {
            if (e.Data is not MenuItem menuItem)
                return;

            var viewModel = this.BindingContext as CustomizationViewModel;

            if (viewModel == null)
                return;

            if (e.TargetColumn != null && e.SourceColumn?.Title.ToString() == "Menu" && (e.TargetColumn.Title.ToString() == "Order" || e.TargetColumn.Title.ToString() == "Ready to Serve" || e.TargetColumn.Title.ToString() == "Ready to Deliver"))
            {
                e.Cancel = true;
                viewModel.LastOrderID += 1;
                viewModel.MenuItems.Add(GetClonedItemModel(menuItem, e.TargetCategory));
            }
        }

        /// <summary>
        /// Creates a new <see cref="MenuItem"/> instance by cloning the properties of the provided data model.
        /// </summary>
        /// <param name="datamodel">The Dragged MenuItem.</param>
        /// <param name="category">The Category in which the card is dropped.</param>
        /// <returns></returns>
        private MenuItem GetClonedItemModel(MenuItem datamodel, object? category)
        {
            MenuItem newModel = new MenuItem();
            var viewModel = this.BindingContext as CustomizationViewModel;

            newModel.Image = datamodel.Image;
            newModel.Category = category ?? datamodel.Category;
            newModel.Description = datamodel.Description;
            newModel.OrderID = datamodel.OrderID;
            newModel.Ingredients = datamodel.Ingredients;
            newModel.ItemName = datamodel.ItemName;
            newModel.OrderID = "Order ID - #" + viewModel?.LastOrderID.ToString();
            return newModel;
        }

        private void OnKanbanCardDragStart(object sender, KanbanDragStartEventArgs e)
        {
            if (e.Data == null)
                return;

            if ((e.Data as MenuItem)?.Category.ToString() == "Menu")
            {
                e.KeepCard = true;
            }
            else
            {
                return;
            }
        }
    }

    public class CustomizationViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the last order ID used in the menu, which is incremented each time a new order is created.
        /// </summary>
        public int LastOrderID { get; set; } = 16365;

        /// <summary>
        /// Gets or sets the collection of <see cref="MenuItem"/> representing the different pizza items available in the menu.
        /// </summary>
        public ObservableCollection<MenuItem> MenuItems { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomizationViewModel"/> class.
        /// </summary>
        public CustomizationViewModel()
        {
            this.MenuItems = new ObservableCollection<MenuItem>();
            Assembly assembly = typeof(SfImageSourceConverter).GetTypeInfo().Assembly;
            var assemblyName = assembly.GetName().Name + ".Resources.Images";

            MenuItems.Add(
                new MenuItem()
                {
                    ItemName = "Margherita",
                    Image = ImageSource.FromResource($"{assemblyName}.margherita.png", assembly),
                    Category = "Menu",
                    Rating = 4,
                    Description = "The classic. Fresh tomatoes, garlic, olive oil, and basil. For pizza purists and minimalists.",
                    Ingredients = new List<string> { "Cheese" }
                }
            );

            MenuItems.Add(
                new MenuItem()
                {
                    ItemName = "Double Cheese",
                    Image = ImageSource.FromResource($"{assemblyName}.margherita.png", assembly),
                    Category = "Menu",
                    Rating = 5,
                    Description = "The minimalist classic with a double helping of cheese",
                    Ingredients = new List<string> { "Cheese" }
                }
            );

            MenuItems.Add(
                new MenuItem()
                {
                    ItemName = "Bucolic Pie",
                    Image = ImageSource.FromResource($"{assemblyName}.bucolicpie.png", assembly),
                    Category = "Menu",
                    Rating = 4.5f,
                    Description = "The pizza you daydream about to escape city life. Onions, peppers, and tomatoes.",
                    Ingredients = new List<string> { "Onions", "Capsicum" }
                }
            );

            MenuItems.Add(
                new MenuItem()
                {
                    ItemName = "Bumper Crop",
                    Image = ImageSource.FromResource($"{assemblyName}.bumpercrop.png", assembly),
                    Rating = 3.25f,
                    Category = "Menu",
                    Description = "Can’t get enough veggies? Eat this. Carrots, mushrooms, potatoes, and way more",
                    Ingredients = new List<string> { "Tomato", "Mushroom" }
                }
            );

            MenuItems.Add(
                new MenuItem()
                {
                    ItemName = "Spice of Life",
                    Image = ImageSource.FromResource($"{assemblyName}.spiceoflife.png", assembly),
                    Category = "Menu",
                    Rating = 4.75f,
                    Description = "Thrill-seeking, heat-seeking pizza people only.  It’s hot. Trust us.",
                    Ingredients = new List<string> { "Corn", "Gherkins" }
                }
            );

            MenuItems.Add(
                new MenuItem()
                {
                    ItemName = "Very Nicoise",
                    Image = ImageSource.FromResource($"{assemblyName}.verynicoise.png", assembly),
                    Category = "Menu",
                    Rating = 3.75f,
                    Description = "Anchovies, Dijon vinaigrette, shallots, red peppers, and potatoes.",
                    Ingredients = new List<string> { "Red pepper", "Capsicum" }
                }
            );

            MenuItems.Add(
                new MenuItem()
                {
                    ItemName = "Salad Daze",
                    Image = ImageSource.FromResource($"{assemblyName}.saladdaze.png", assembly),
                    Category = "Menu",
                    Rating = 5,
                    Description = "Pretty much salad on a pizza. Broccoli, olives, cherry tomatoes, red onion.",
                    Ingredients = new List<string> { "Onions", "Jalapeno" }
                }
            );

            MenuItems.Add(
                new MenuItem()
                {
                    ItemName = "Bumper Crop",
                    Image = ImageSource.FromResource($"{assemblyName}.bumpercrop.png", assembly),
                    Category = "Ready to Serve",
                    OrderID = "Order ID - #16362",
                    Description = "Can’t get enough veggies? Eat this. Carrots, mushrooms, potatoes, and way more",
                    Ingredients = new List<string> { "Tomato", "Mushroom" }
                }
            );

            MenuItems.Add(
                new MenuItem()
                {
                    ItemName = "Spice of Life",
                    Image = ImageSource.FromResource($"{assemblyName}.spiceoflife.png", assembly),
                    Category = "Ready to Serve",
                    OrderID = "Order ID - #16363",
                    Description = "Thrill-seeking, heat-seeking pizza people only.  It’s hot. Trust us.",
                    Ingredients = new List<string> { "Corn", "Gherkins" }
                }
            );

            MenuItems.Add(
                new MenuItem()
                {
                    ItemName = "Bucolic Pie",
                    Image = ImageSource.FromResource($"{assemblyName}.bucolicpie.png", assembly),
                    Category = "Door Delivery",
                    OrderID = "Order ID - #16361",
                    Description = "The pizza you daydream about to escape city life. Onions, peppers, and tomatoes.",
                    Ingredients = new List<string> { "Onions", "Capsicum" }
                }
            );

            MenuItems.Add(
                new MenuItem()
                {
                    ItemName = "Very Nicoise",
                    Image = ImageSource.FromResource($"{assemblyName}.verynicoise.png", assembly),
                    Category = "Order for Dining",
                    OrderID = "Order ID - #16364",
                    Description = "Anchovies, Dijon vinaigrette, shallots, red peppers, and potatoes.",
                    Ingredients = new List<string> { "Red pepper", "Capsicum" }
                }
            );

            MenuItems.Add(
                new MenuItem()
                {
                    ItemName = "Double Cheese",
                    Image = ImageSource.FromResource($"{assemblyName}.margherita.png", assembly),
                    Category = "Order for Delivery",
                    OrderID = "Order ID - #16365",
                    Description = "The minimalist classic with a double helping of cheese",
                    Ingredients = new List<string> { "Cheese" }
                }
            );
        }

        #endregion
    }

    public class MenuItem
    {
        #region Fields

        /// <summary>
        /// The name of the menu item.
        /// </summary>
        private string itemName = string.Empty;

        /// <summary>
        /// The description of the menu item.
        /// </summary>
        private string description = string.Empty;

        /// <summary>
        /// The category to which the menu item belongs.
        /// </summary>
        private object category = string.Empty;

        /// <summary>
        /// The image path or URL representing the menu item.
        /// </summary>
        private ImageSource image = string.Empty;

        /// <summary>
        /// The list of ingredients used in the menu item.
        /// </summary>
        private List<string> ingredients = new List<string>();

        /// <summary>
        /// The current order state of the menu item.
        /// </summary>
        private object orderState = string.Empty;

        /// <summary>
        /// The order ID of the menu item.
        /// </summary>
        private string orderID = string.Empty;

        /// <summary>
        /// The rating of the menu item.
        /// </summary>
        private float rating = 0f;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the menu item.
        /// </summary>
        public string ItemName
        {
            get { return this.itemName; }
            set { this.itemName = value; }
        }

        /// <summary>
        /// Gets or sets the description of the menu item.
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        /// <summary>
        /// Gets or sets the category of the menu item.
        /// </summary>
        public object Category
        {
            get { return this.category; }
            set { this.category = value; }
        }

        /// <summary>
        /// Gets or sets the image source representing the menu item, which can be a file path, URI, or embedded resource.
        /// </summary>
        public ImageSource Image
        {
            get { return this.image; }
            set { this.image = value; }
        }

        /// <summary>
        /// Gets or sets the list of ingredients used in the menu item.
        /// </summary>
        public List<string> Ingredients
        {
            get { return this.ingredients; }
            set { this.ingredients = value; }
        }

        /// <summary>
        /// Gets or sets the current order state of the menu item (e.g., Dining, Delivery).
        /// </summary>
        public object OrderState
        {
            get { return this.orderState; }
            set { this.orderState = value; }
        }

        /// <summary>
        /// Gets or sets the rating of the menu item.
        /// </summary>
        public float Rating
        {
            get { return this.rating; }
            set { this.rating = value; }
        }

        /// <summary>
        /// Gets or sets the order ID of the menu item.
        /// </summary>
        public string OrderID
        {
            get { return this.orderID; }
            set { this.orderID = value; }
        }

        #endregion
    }
}
