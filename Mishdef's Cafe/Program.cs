using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyFunctions.Tools;

namespace Mishdef_s_Cafe
{
    internal class Program
    {
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
                                break;
                            }
                        case 2:
                            {
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
    }
}
