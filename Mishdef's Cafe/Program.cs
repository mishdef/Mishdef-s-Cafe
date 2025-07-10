using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static MyFunctions.Tools;
using static MyFunctions.MessageBox;
using MyFunctions;

namespace Mishdef_s_Cafe
{
    internal class Program
    {
        static string[] items = new string[0];
        static double[] costs = new double[0];

        static double tipAmount = 0.0;
        static bool isDataSaved = true;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "Mishdef's Cafe   ||   Stadnikov Michailo 611п";

            Console.WriteLine("┏━┯━━━━━━━━━━━━━━━━━━━━┯━┓");
            Console.WriteLine("┠─┼────────────────────┼─┨");
            Console.WriteLine("┃ │                    │ ┃");
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
            Console.WriteLine("┃ │                    │ ┃");
            Console.WriteLine("┠─┼────────────────────┼─┨");
            Console.WriteLine("┗━┷━━━━━━━━━━━━━━━━━━━━┷━┛");

            do
            {
                try
                {
                    switch (InputInt("Enter your choice: ", InputType.With, 0, 7))
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
                                DisplayBill();
                                break;
                            }
                        case 5:
                            {
                                MenuClearAllItems();
                                break;
                            }
                        case 6:
                            {
                                MenuWriteToFile();
                                break;
                            }
                        case 7:
                            {
                                MenuReadFromFile();
                                break;
                            }
                        case 0:
                            {
                                if (isDataSaved)
                                {
                                    Console.WriteLine("Exiting the program. Goodbye!");
                                    return;
                                }
                                else
                                {
                                    if(MessageBox.Show("You have unsaved data or changes! Do you want to exit without saving?", "Message", Buttons.YesNo) == Button.Yes)
                                    {
                                        Console.WriteLine("Exiting the program. Goodbye!");
                                        return;
                                    }
                                    Console.WriteLine("Comming back to main menu....");
                                    break;
                                }
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
            if (items.Length >= 5)
            {
                MessageBox.Show("You cannot add more than 5 items.", "Error", Buttons.Ok);
                return;
            }

            if (tipAmount != 0)
            {
                MessageBox.Show("Tip are gone be reset to 0.", "Warning", Buttons.None);
            }

            string itemName;
            do
            {
                Console.Write("Enter item name: ");
                itemName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(itemName) || itemName.Length < 3 || itemName.Length > 20)
                {
                    Console.WriteLine("Item name must be between 3 and 20 characters long. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(itemName) || itemName.Length < 3 || itemName.Length > 20);
            double itemCost = InputDouble("Enter item cost: ", InputType.Without, 0);

            tipAmount = 0.0;

            MessageBox.Show(AddItem(itemName, itemCost), "Message", Buttons.Ok);
        }

        static string AddItem(string itemName, double itemCost)
        {
            try
            {
                int currentItemsCount = items.Length;
                Array.Resize(ref items, currentItemsCount + 1);
                Array.Resize(ref costs, currentItemsCount + 1);

                items[currentItemsCount] = itemName;
                costs[currentItemsCount] = itemCost;

                isDataSaved = false;

                return $"Item '{itemName}' with cost {itemCost:F2}$ added successfully.";
            }
            catch (Exception ex)
            {
                return "An error occurred while adding the item: " + ex.Message;
            }
        }

        static void MenuDeleteItem()
        {
            if (items.Length == 0)
            {
                MessageBox.Show("No items to remove.", "Error", Buttons.Ok);
                return;
            }

            if (tipAmount != 0)
            {
                MessageBox.Show("Tip are gone be reset to 0.", "Warning", Buttons.None);
            }

            Console.WriteLine("Items available to remove:");
            DrawLine(40);
            for (int i = 0; i < items.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i]} - {costs[i].ToString("F2")}$");
            }
            Console.WriteLine("\n0. Cancel");
            DrawLine(40);

            int itemIndex = InputInt("Enter the number of the item to remove: ", InputType.With, 0, items.Length) - 1;

            if (itemIndex == -1)
            {
                return;
            }

            MessageBox.Show(DeleteItem(itemIndex), "Message", Buttons.Ok);

            if (items.Length != 0)
            {
                if (MessageBox.Show("Would you like to set a tip?", "Question", Buttons.YesNo) == Button.Yes)
                {
                    MenuAddTip();
                }
                else
                {
                    MessageBox.Show("Tip has been set to 0.0$", "Message", Buttons.Ok);
                }
            }
        }

        static string DeleteItem(int itemIndex)
        {
            if (itemIndex < 0 || itemIndex >= items.Length)
            {
                return "Invalid item index.";
            }

            string removedItem = items[itemIndex];
            double removedCost = costs[itemIndex];

            for (int i = itemIndex; i < items.Length - 1; i++)
            {
                items[i] = items[i + 1];
                costs[i] = costs[i + 1];
            }

            Array.Resize(ref items, items.Length - 1);
            Array.Resize(ref costs, costs.Length - 1);

            isDataSaved = false;
            tipAmount = 0;

            return $"Item '{removedItem}' with cost {removedCost:F2}$ removed successfully.";
        }

        static void MenuAddTip()
        {
            if (items.Length == 0)
            {
                MessageBox.Show("No items available to calculate the tip.", "Message", Buttons.Ok);
                return;
            }

            double totalCost = 0.0;

            foreach (var value in costs)
            {
                totalCost += value;
            }

            BoxItem($"Curent tip: {tipAmount}$  ┃  Net Total: {totalCost}$");

            Console.WriteLine(" 1. Input percentage");
            Console.WriteLine(" 2. Input amount");
            Console.WriteLine(" 3. Without tip");

            switch (InputInt("\nEnter your choice: ", InputType.With, 1, 3))
            {
                case 1:
                    double percentage = InputDouble("Enter tip percentage: ", InputType.With, 0);
                    MessageBox.Show(AddTipPercentage(percentage), "Message", Buttons.Ok);
                    break;
                case 2:
                    double amount = InputDouble("Enter tip amount: ", InputType.With, 0);
                    MessageBox.Show(AddTipAmount(amount), "Message", Buttons.Ok);
                    break;
                case 3:
                    tipAmount = 0.0;
                    MessageBox.Show("Tip has been set to 0.0$", "Message", Buttons.Ok);
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        static string AddTipAmount(double amount)
        {
            tipAmount = amount;
            return $"Tip {tipAmount}$ are set";
        }

        static string AddTipPercentage(double percentage)
        {
            double totalCost = 0.0;

            foreach (var amount in costs)
            {
                totalCost += amount;
            }

            tipAmount = totalCost * (percentage / 100);

            return $"Tip {tipAmount:F2}$ are set based on {percentage}% of the total cost.";
        }

        static void DisplayBill()
        {
            if (items.Length == 0)
            {
                MessageBox.Show("No items in the bill.", "Message", Buttons.Ok);
                return;
            }

            double totalCost = 0.0;

            Console.WriteLine("\nDescription               Price");
            Console.WriteLine("-------------------- ----------");
            for (int i = 0; i < items.Length; i++)
            {
                Console.WriteLine($"{items[i].PadRight(20)} {("$" + costs[i].ToString()).PadLeft(10)}");
                totalCost += costs[i];
            }

            double gstAmount = totalCost * 0.05;

            Console.WriteLine("-------------------- ----------");
            Console.WriteLine($"{"Net Total".PadLeft(20)} {('$' + totalCost.ToString("F2")).PadLeft(10)}");
            Console.WriteLine($"{"Tip Amount".PadLeft(20)} {('$' + tipAmount.ToString("F2")).PadLeft(10)}");
            Console.WriteLine($"{"GST Amount".PadLeft(20)} {('$' + gstAmount.ToString("F2")).PadLeft(10)}");
            Console.WriteLine($"{"Total Amount".PadLeft(20)} {('$' + (totalCost + tipAmount + gstAmount).ToString("F2")).PadLeft(10)}");

            Console.WriteLine("\nPress any key to continue...\n");
            Console.CursorVisible = false;
            Console.ReadKey();
            Console.CursorVisible = true;
        }

        static void MenuClearAllItems()
        {
            if (items.Length == 0)
            {
                MessageBox.Show("No items to clear.", "Message", Buttons.Ok);
                return;
            }
            if (MessageBox.Show("Are you sure you want to clear items?", "Question", Buttons.YesNo) == Button.Yes)
            {
                MessageBox.Show(ClearAllItems(), "Message");
            }
            else
            {
                MessageBox.Show("Clearing items has been canceled.", "Message");
                return;
            }
        }

        static string ClearAllItems()
        {
            items = new string[0];
            costs = new double[0];
            tipAmount = 0.0;

            isDataSaved = true;

            return "All items and tips have been cleared.";
        }

        static void MenuWriteToFile()
        {
            if (items.Length == 0)
            {
                MessageBox.Show("No items to save.", "Message", Buttons.Ok);
                return;
            }

            string filePath = InputFileName("Enter file name to save data: ", ".txt");

            if (File.Exists(filePath))
            {
                if (MessageBox.Show($"File {filePath} already exists. Do you want to overwrite it?", "Question", Buttons.YesNo) == Button.No)
                {
                    MessageBox.Show("Saving data has been canceled.");
                    return;
                }
            }

            MessageBox.Show(WriteToFile(filePath), "Message", Buttons.Ok);
        }

        static string WriteToFile(string filePath)
        {
            try
            {
                using (FileStream file = new FileStream(filePath, FileMode.Create))
                using (StreamWriter writer = new StreamWriter(file))
                {
                    for (int i = 0; i < items.Length; i++)
                    {
                        writer.WriteLine($"{items[i]}\t{costs[i]}");
                    }
                }

                isDataSaved = true;

                return $"Data saved successfully to {filePath}.";
            }
            catch (Exception ex)
            {
                return $"Error saving data: {ex.Message}";
            }
        }

        static void MenuReadFromFile()
        {
            if (items.Length != 0)
            {
                if (MessageBox.Show("All your previous data will be cleard. Continue?", "Question", Buttons.YesNo) == Button.No)
                {
                    return;
                }
            }
            string filePath = InputFileName("Enter file name to load data: ", ".txt");
            MessageBox.Show(ReadFromFile(filePath), "Message", Buttons.Ok);
        }

        static string ReadFromFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return $"File {filePath} does not exist.";
                }

                using (FileStream file = new FileStream(filePath, FileMode.Open))
                using (StreamReader reader = new StreamReader(file))
                {
                    if (file.Length == 0)
                    {
                        return $"File {filePath} is empty.";
                    }

                    ClearAllItems();

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split('\t');
                        if (parts.Length == 2)
                        {
                            Array.Resize(ref items, items.Length + 1);
                            Array.Resize(ref costs, costs.Length + 1);
                            items[items.Length - 1] = parts[0];
                            costs[costs.Length - 1] = double.Parse(parts[1]);
                        }
                    }

                    isDataSaved = true;

                    return $"Data loaded successfully from {filePath}.";
                }
            }
            catch (Exception ex)
            {
                return $"Error loading data: {ex.Message}";
            }
        }
    }
}