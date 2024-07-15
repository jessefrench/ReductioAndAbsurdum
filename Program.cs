// list to store products
List<Product> products = new List<Product>()
{
    // product instances
    new Product()
    {
        Name = "Wand of Elemental Mastery",
        Price = 149.99M,
        ProductTypeId = 4,
        DateStocked = new DateTime(2024, 7, 10),
        Sold = false,
    },
    new Product()
    {
        Name = "Potion of Invisibility",
        Price = 49.99M,
        ProductTypeId = 2,
        DateStocked = new DateTime(2024, 6, 19),
        Sold = false,
    },
    new Product()
    {
        Name = "Crystal Ball of Clairvoyance",
        Price = 199.99M,
        ProductTypeId = 3,
        DateStocked = new DateTime(2024, 6, 27),
        Sold = false,
    },
    new Product()
    {
        Name = "Enchanted Quill",
        Price = 29.99M,
        ProductTypeId = 3,
        DateStocked = new DateTime(2024, 7, 12),
        Sold = false,
    },
    new Product()
    {
        Name = "Dragon Scale Armor",
        Price = 499.99M,
        ProductTypeId = 1,
        DateStocked = new DateTime(2024, 7, 8),
        Sold = false,
    },
    new Product()
    {
        Name = "Spellbook of Ancient Wisdom",
        Price = 249.99M,
        ProductTypeId = 3,
        DateStocked = new DateTime(2024, 6, 3),
        Sold = false,
    }
};

// list to store product types
List<ProductType> productTypes = new List<ProductType>()
{
    // product type instances
    new ProductType()
    {
        Name = "Apparel",
        Id = 1
    },
    new ProductType()
    {
        Name = "Potions",
        Id = 2
    },
    new ProductType()
    {
        Name = "Enchanted Objects",
        Id = 3
    },
    new ProductType()
    {
        Name = "Wands",
        Id = 4
    }
};

// greeting message
string greeting = @"Welcome to Reductio & Absurdum!
The best place for high-quality magical supplies for nearly 1000 years.";
Console.WriteLine(greeting);

// main menu loop
string choice = null;
while (choice != "0")
{
    Console.Clear();
    Console.WriteLine(@"Choose an option:
                        0. Exit
                        1. Display all products
                        2. Add a product
                        3. Sell a product
                        4. Delete a product
                        5. Search products by type
                        6. View available products");
    choice = Console.ReadLine();
    switch (choice)
    {
        case "0":
            Console.WriteLine("Goodbye!");
            break;
        case "1":
            DisplayAllProducts();
            break;
        case "2":
            AddProduct();
            break;
        case "3":
            SellProduct();
            break;
        case "4":
            DeleteProduct();
            break;
        case "5":
            SearchByProductType();
            break;
        case "6":
            ViewAvailableProducts();
            break;
        default:
            Console.WriteLine("Invalid choice. Please make a valid selection.");
            break;
    }
    if (choice != "0")
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}

void DisplayAllProducts() // method to display all products
{
    for (int i = 0; i < products.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {ProductDetails(products[i])}");
    }
}

void AddProduct() // method to add a new product
{
    Console.WriteLine("Enter product details:");

    // name
    Console.Write("Name: ");
    string name = Console.ReadLine();

    // price
    Console.Write("Price: ");
    decimal price;
    while (!decimal.TryParse(Console.ReadLine(), out price) || price <= 0)
    {
        Console.WriteLine("Invalid input. Price must be a positive number.");
        Console.Write("Price: ");
    }

    // display product types
    Console.WriteLine("Select product type: ");
    for (int i = 0; i < productTypes.Count; i++)
    {
        Console.WriteLine($"{productTypes[i].Id}. {productTypes[i].Name}");
    }

    // get product type id
    int productTypeId;
    while (!int.TryParse(Console.ReadLine(), out productTypeId) || !productTypes.Any(pt => pt.Id == productTypeId))
    {
        Console.WriteLine("Invalid input. Please make a valid selection.");
    }

    // create new product object
    Product newProduct = new Product()
    {
        Name = name,
        Price = price,
        ProductTypeId = productTypeId,
        Sold = false,
    };

    // add new product to the list
    products.Add(newProduct);
    Console.WriteLine("Product added successfully!");
}

void SellProduct() // method to sell a product
{
    Console.WriteLine("Available products: ");

    // display only available products
    List<Product> availableProducts = products.FindAll(p => !p.Sold);
    if (availableProducts.Count == 0)
    {
        Console.WriteLine("No products currently available.");
        return;
    }

    // display available products with index
    for (int i = 0; i < availableProducts.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {ProductDetails(availableProducts[i])}");
    }

    // prompt user to choose a product
    Console.Write("Enter the number of the product you want to buy: ");
    if (int.TryParse(Console.ReadLine(), out int productIndex) && productIndex > 0 && productIndex <= availableProducts.Count)
    {
        // update the selected product's sold status
        Product selectedProduct = availableProducts[productIndex - 1];
        selectedProduct.Sold = true;
        Console.WriteLine($"You have purchased the {selectedProduct.Name}!");
    }
    else
    {
        Console.WriteLine("Invalid input. Please make a valid selection.");
    }
}

void DeleteProduct() // method to delete a product
{
    Console.WriteLine("All products: ");

    // display all products with index
    for (int i = 0; i < products.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {ProductDetails(products[i])}");
    }

    // prompt user to choose a product to delete
    Console.Write("Enter the number of the product you want to delete: ");
    if (int.TryParse(Console.ReadLine(), out int productIndex) && productIndex > 0 && productIndex <= products.Count)
    {
        // remove the selected product from the list
        products.RemoveAt(productIndex - 1);
        Console.WriteLine("The selected product has been deleted.");
    }
    else
    {
        Console.WriteLine("Invalid input. Please make a valid selection.");
    }
}

void SearchByProductType() // method to search by product type
{
    // display product types
    Console.WriteLine("Select product type to look up: ");
    for (int i = 0; i < productTypes.Count; i++)
    {
        Console.WriteLine($"{productTypes[i].Id}. {productTypes[i].Name}");
    }

    // get product type id
    int productTypeId;
    while (!int.TryParse(Console.ReadLine(), out productTypeId) || !productTypes.Any(pt => pt.Id == productTypeId))
    {
        Console.WriteLine("Invalid input. Please make a valid selection.");
    }

    // retrieve products of the selected type
    var productsOfType = products.Where(p => p.ProductTypeId == productTypeId).ToList();

    // display the products
    if (productsOfType.Count > 0)
    {
        Console.WriteLine("Products of selected type:");
        int counter = 1;
        foreach (var product in productsOfType)
        {
            Console.WriteLine($"{counter++}. Name: {product.Name}, Price: {product.Price}, Sold: {product.Sold}");
        }
    }
    else
    {
        Console.WriteLine("No products found for the selected type.");
    }
}

void ViewAvailableProducts() // method to view all available products
{
    Console.WriteLine("Available products: ");

    // display only available products
    List<Product> unsoldProducts = products.Where(p => !p.Sold).ToList();
    if (unsoldProducts.Count == 0)
    {
        Console.WriteLine("No products currently available.");
        return;
    }

    // display available products with index
    for (int i = 0; i < unsoldProducts.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {ProductDetails(unsoldProducts[i])}");
    }
}

string ProductDetails(Product product) // method to format product object string
{
    string productString = $"{product.Name} {(product.Sold ? "was sold" : "is available")} for ${product.Price}. It has been on the shelf for {product.DaysOnShelf} days.";
    return productString;
}