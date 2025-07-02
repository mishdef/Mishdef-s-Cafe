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
        static double tip = 0.0;

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
                                MenuAddTip();
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

            MessageBox.Show(AddItem(itemName, itemCost), "Message", Buttons.Ok);
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
            DrawLine(40);
            for (int i = 0; i < itemsAndCost[0].Length; i++)
            {
                Console.WriteLine($"{i + 1}. {itemsAndCost[0][i]} - {itemsAndCost[1][i]}$");
            }
            Console.WriteLine("\n0. Cancel");
            DrawLine(40);

            int itemIndex = InputInt("Enter the number of the item to remove: ", InputType.With, 0, itemsAndCost[0].Length) - 1;

            if (itemIndex == -1)
            {
                return;
            }

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

        static void MenuAddTip()
        {
            Console.Clear();

            if (itemsAndCost[1].Length == 0)
            {
                MessageBox.Show("No items available to calculate the tip.", "Message", Buttons.Ok);
                return;
            }

            BoxItem($"Curent tip: {tip}$");
            
            Console.WriteLine(" 1. Input amount");
            Console.WriteLine(" 2. Input percentage");
            Console.WriteLine(" 3. Without tip");

            switch (InputInt("\nEnter your choice: ", InputType.With, 0, 3))
            {
                case 1:
                    double amount = InputDouble("Enter tip amount: ", InputType.With, 0);
                    MessageBox.Show(AddTipAmount(amount), "Message", Buttons.Ok);
                    break;
                case 2:
                    double percentage = InputDouble("Enter tip percentage (0-100): ", InputType.With, 0);
                    MessageBox.Show(AddTipPercentage(percentage), "Message", Buttons.Ok);
                    break;
                case 3:
                    tip = 0.0;
                    MessageBox.Show("Tip has been set to 0.0$", "Message", Buttons.Ok);
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        static string AddTipAmount(double amount)
        {
            tip = amount;
            return $"Tip {tip}$ are set";
        }

        static string AddTipPercentage(double percentage)
        {
            double totalCost = 0.0;

            foreach (var cost in itemsAndCost[1])
            {
                totalCost += double.Parse(cost);
            }

            tip = totalCost * (percentage / 100);

            return $"Tip {tip:F2}$ are set based on {percentage}% of the total cost.";
        }
    }
}