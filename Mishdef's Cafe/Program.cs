using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyFunctions.Tools;
using static MyFunctions.MessageBox;
using MyFunctions;

namespace Mishdef_s_Cafe
{
    internal class Program
    {
        static string[][] itemsAndCost = new string[2][]
            {
            new string[0] {},
            new string[0] {},
            };

        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.UTF8;
            do
            {
                try
                {
                    Console.Clear();

                    Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━┓");
                    Console.WriteLine("┃ ┌────────────────────┐ ┃");
                    Console.WriteLine("┃ │   Mishdef's Cafe   │ ┃");
                    Console.WriteLine("┃ │ ------------------ │ ┃");
                    Console.WriteLine("┃ │ 1. Add Item        │ ┃");
                    Console.WriteLine("┃ │ 2. Remove Item     │ ┃");
                    Console.WriteLine("┃ │ 3. Add Tip         │ ┃");
                    Console.WriteLine("┃ │ 4. Display Bill    │ ┃");
                    Console.WriteLine("┃ │ 5. Clear All       │ ┃");
                    Console.WriteLine("┃ │ 6. Save to file    │ ┃");
                    Console.WriteLine("┃ │ 7. Load from file  │ ┃");
                    Console.WriteLine("┃ │ 0. Exit            │ ┃");
                    Console.WriteLine("┃ └────────────────────┘ ┃");
                    Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━┛");

                    switch (InputInt("\nEnter your choice: "))
                    {
                        case 1:
                            {
                                MenuAddItem();
                                break;
                            }
                        case 2:
                            {
                                MenuDeleteItem();
                                break;
                            }
                        case 3:
                            {
                                break;
                            }
                        case 4:
                            {
                                break;
                            }
                        case 5:
                            {
                                break;
                            }
                        case 6:
                            {
                                break;
                            }
                        case 7:
                            {
                                break;
                            }
                        case 0:
                            {
                                Console.WriteLine("Exiting the program. Goodbye!");
                                return;
                            }
                        default:
                            {
                                Console.WriteLine("Invalid choice. Please try again.");
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            } while (true);
        }

        static void MenuAddItem()
        {
            if (itemsAndCost[0].Length >= 5)
            {
                MessageBox.Show("You cannot add more than 5 items.", "Error", Buttons.Ok);
                return;
            }

            Console.Clear();

            string itemName;
            do
            {
                Console.Write("Enter item name: ");
                itemName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(itemName) || itemName.Length < 3 || itemName.Length > 25)
                {
                    Console.WriteLine("Item name must be between 3 and 25 characters long. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(itemName) || itemName.Length < 3 || itemName.Length > 20);
            double itemCost = InputDouble("Enter item cost: ", InputType.Without, 0);

            MessageBox.Show(AddItem(itemName, itemCost),"Message", Buttons.Ok);
        }

        static string AddItem(string itemName, double itemCost)
        {
            try
            {
                int currentItemsCount = itemsAndCost[0].Length;
                Array.Resize(ref itemsAndCost[0], currentItemsCount + 1);
                Array.Resize(ref itemsAndCost[1], currentItemsCount + 1);

                itemsAndCost[0][currentItemsCount] = itemName;
                itemsAndCost[1][currentItemsCount] = itemCost.ToString("F2");

                return $"Item '{itemName}' with cost {itemCost:F2}$ added successfully.";
            }
            catch (Exception ex)
            {
                return "An error occurred while adding the item: " + ex.Message;
            }
        }

        static void MenuDeleteItem()
        {
            if (itemsAndCost[0].Length == 0)
            {
                MessageBox.Show("No items to remove.", "Error", Buttons.Ok);
                return;
            }

            Console.Clear();

            Console.WriteLine("Items available to remove:");
            for (int i = 0; i < itemsAndCost[0].Length; i++)
            {
                Console.WriteLine($"{i + 1}. {itemsAndCost[0][i]} - {itemsAndCost[1][i]}$");
            }
            int itemIndex = InputInt("Enter the number of the item to remove: ", InputType.With, 1, itemsAndCost[0].Length) - 1;
            
            MessageBox.Show(DeleteItem(itemIndex), "Message", Buttons.Ok);
        }

        static string DeleteItem(int itemIndex)
        {
            if (itemIndex < 0 || itemIndex >= itemsAndCost[0].Length)
            {
                return "Invalid item index.";
            }

            string removedItem = itemsAndCost[0][itemIndex];
            double removedCost = double.Parse(itemsAndCost[1][itemIndex]);

            for (int i = itemIndex; i < itemsAndCost[0].Length - 1; i++)
            {
                itemsAndCost[0][i] = itemsAndCost[0][i + 1];
                itemsAndCost[1][i] = itemsAndCost[1][i + 1];
            }

            Array.Resize(ref itemsAndCost[0], itemsAndCost[0].Length - 1);
            Array.Resize(ref itemsAndCost[1], itemsAndCost[1].Length - 1);

            return $"Item '{removedItem}' with cost {removedCost:F2}$ removed successfully.";
        }
    }
}