using Figgle;
using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace Word_Data_Manager
{
    internal class Program
    {
        // ========================
        // Data Storage and Tracking
        // ========================

        // Declaration of a static instance of MyDictionary to store words and their corresponding Node instances

        private static MyDictionary<string, Node> dictionary = new MyDictionary<string, Node>();

        // Declaration of a static array to store Node instances

        private static Node[] nodeArray = new Node[0];

        // Declaration of a static string to keep track of the current sorting method

        private static string currentSortingMethod = "None";

        // ========================
        // Main Program Functionalities
        // ========================

        // Main entry point of the program
        static void Main(string[] args)
        {
            // Display the main title of the application
            MainTitle();

            // Main menu loop that continues until the program is exited
            while (true)
            {
                MainMenu();
            }
        }

        // Displays the main title of the application along with relevant information and options
        static void MainTitle()
        {
            // The main title text for the application
            string titleText = "Word Data Manager";

            // Generate ASCII art for the main title
            string asciiArt = FiggleFonts.Standard.Render(titleText);

            // Display application information, version, and author
            Console.Write(asciiArt + "COMP605 - Data Structures and Algorithms\n" + "v1.0\n" + "By Hamish Getty\n");

            // Print a line break to enhance readability
            PrintLineBreak();

            // Display the list of available operations in the application
            Console.Write("This application can:\n" +
                "- Load File: Insert words from a selected file into a dictionary or node array.\n" +
                "- Insert Word: Add new words to the dictionary, avoiding duplicates.\n" +
                "- Find Word: Search for a specific word within the dictionary and retrieve its details.\n" +
                "- Delete: Choose to remove a particular word or clear the entire dictionary (caution: irreversible).\n" +
                "- Sort using Bubble Sort / O(n^2): Arrange words in the node array using the Bubble Sort algorithm.\n" +
                "- Sort using Quick Sort / O(nlogn): Arrange words in the node array using the Quick Sort algorithm.\n" +
                "- Print Operations: Display information about the dictionary or node array and the current sorting method.\n" +
                "- Exit: Close the application.\n\n");

            // Prompt the user to press any key to get started
            Console.Write("Press any key to get started: ");

            // Wait for user input
            Console.ReadKey();
        }

        // Displays the main menu and handles user input for navigation
        static void MainMenu()
        {
            // Infinite loop for continuous menu display
            while (true)
            {
                // Clear the console for a clean menu appearance
                Console.Clear();

                // Display the main menu options
                DisplayMenu(new string[] { "Dictionary Manager", "Array Manager", "Exit" });

                // Get user choice from input
                string choice = Console.ReadLine();

                // Switch statement to handle user choices
                switch (choice)
                {
                    case "1":
                        // Navigate to the Dictionary Manager submenu
                        DictionaryMainMenu();
                        break;

                    case "2":
                        // Navigate to the Node Array Manager submenu
                        NodeArrayMainMenu();
                        break;

                    case "3":
                        // Exit the application
                        Environment.Exit(0);
                        break;

                    default:
                        // Display an error message for invalid choices
                        DisplayErrorMessage();
                        break;
                }
            }
        }

        // Displays an error message for invalid user choices and prompts for continuation
        private static void DisplayErrorMessage()
        {
            Console.WriteLine("Invalid choice. Please try again.");
            Console.Write("Press any key to continue: ");
            Console.ReadKey();
        }

        // Displays a menu with the provided options
        private static void DisplayMenu(string[] options)
        {
            PrintLineBreak();

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            Console.Write("Select an option: ");
        }

        // Prints a line break using dashes to match the console window width
        static void PrintLineBreak()
        {
            // Get the current width of the console window
            int consoleWidth = Console.WindowWidth;

            // Create a string of dashes with the same width as the console window
            string dashes = new string('-', consoleWidth);

            // Output the line break
            Console.Write(dashes);
        }

        // Reads valid lines from a file, excluding comments and empty lines
        static string[] ReadValidLinesFromFile(string filePath)
        {
            try
            {
                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);

                // Filter valid lines (excluding comments and empty lines)
                var validLines = lines
                    .Where(line => !line.StartsWith("#") && !string.IsNullOrWhiteSpace(line))
                    .ToArray();

                return validLines;
            }
            catch (Exception ex)
            {
                // Print an error message if an exception occurs during file reading
                Console.WriteLine($"An error occurred: {ex.Message}");

                // Return an empty array in case of an error
                return Array.Empty<string>();
            }
        }

        // ========================
        // Dictionary Management
        // ========================

        // Manages the main menu for dictionary-related operations
        static void DictionaryMainMenu()
        {
            while (true)
            {
                // Clear the console screen for a clean display
                Console.Clear();

                // Display the menu options for dictionary operations
                DisplayMenu(new string[] { "Load File", "Insert Word", "Find Word", "Delete", "Print Dictionary", "Back" });

                // Get user input for menu selection
                string choice = Console.ReadLine();

                // Perform actions based on user choice
                switch (choice)
                {
                    case "1":
                        DictionaryFileMenu(dictionary);
                        break;

                    case "2":
                        DictionaryInsertMenu(dictionary);
                        break;

                    case "3":
                        DictionaryFindMenu(dictionary);
                        break;

                    case "4":
                        DictionaryDeleteMenu(dictionary);
                        break;

                    case "5":
                        DictionaryPrint(dictionary);
                        break;

                    case "6":
                        return;

                    default:
                        // Display an error message for invalid choices
                        DisplayErrorMessage();
                        break;
                }
            }
        }

        // Handles the file menu for inserting words into the dictionary
        static void DictionaryFileMenu(MyDictionary<string, Node> dictionary)
        {
            // Print a line break for better readability
            PrintLineBreak();

            Console.WriteLine("Choose a folder to access:");
            Console.WriteLine("1. Ordered");
            Console.WriteLine("2. Random");

            Console.Write("Select an option: ");
            string folderChoice = Console.ReadLine();

            string folderPath = "";

            // Determine the folder path based on user choice
            switch (folderChoice)
            {
                case "1":
                    folderPath = "ordered";
                    break;
                case "2":
                    folderPath = "random";
                    break;
                default:
                    // Display an error message for an invalid choice
                    DisplayErrorMessage();
                    return;
            }

            // Print a line break for better readability
            PrintLineBreak();

            Console.WriteLine($"You selected the {folderPath} folder.");

            Console.WriteLine("Choose a file to insert into the dictionary:");

            // Get all files in the selected folder
            string[] files = Directory.GetFiles(folderPath);

            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(files[i])}");
            }

            Console.Write("Select an option: ");
            string fileChoice = Console.ReadLine();

            int selectedFileIndex;
            if (int.TryParse(fileChoice, out selectedFileIndex) && selectedFileIndex >= 1 && selectedFileIndex <= files.Length)
            {
                string filePath = files[selectedFileIndex - 1];
                PrintLineBreak();

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                Console.WriteLine($"Clearing the current dictionary...");

                // Clear the current dictionary
                dictionary.Clear();

                Console.WriteLine($"Inserting words from {filePath} into the dictionary...");

                // Read the file and insert words into the dictionary
                DictionaryReadFile(filePath, dictionary);

                stopwatch.Stop();
                TimeSpan elapsedTime = stopwatch.Elapsed;

                Console.WriteLine($"Time taken: {elapsedTime.TotalMilliseconds} milliseconds");

                Console.Write("Press any key to continue: ");
                Console.ReadKey();
                return;
            }
            else
            {
                // Print a line break for better readability
                PrintLineBreak();
                DisplayErrorMessage();
                return;
            }
        }

        // Reads valid lines from a file and inserts unique words into the dictionary
        static void DictionaryReadFile(string filePath, MyDictionary<string, Node> dictionary)
        {
            // Initialize counters for inserted and duplicate words
            int wordsInserted = 0;
            int duplicateWords = 0;

            // Read valid lines from the file using the ReadValidLinesFromFile method
            string[] validLines = ReadValidLinesFromFile(filePath);

            // Iterate through valid lines and insert unique words into the dictionary
            foreach (string line in validLines)
            {
                if (!dictionary.ContainsKey(line))
                {
                    // Create a new Node for the word and add it to the dictionary
                    Node node = new Node(line);
                    dictionary.Add(line, node);
                    wordsInserted++;
                }
                else
                {
                    // Increment the duplicate words counter if the word already exists in the dictionary
                    duplicateWords++;
                }
            }

            // Display information about the operation
            PrintLineBreak();
            Console.WriteLine($"Duplicate keys found: {duplicateWords}. Skipped insertions.");
            Console.WriteLine($"{wordsInserted} words inserted into the dictionary successfully.");
        }

        // Handles the insertion of a word into the dictionary
        static void DictionaryInsertMenu(MyDictionary<string, Node> dictionary)
        {
            // Display a line break for better visual separation
            PrintLineBreak();

            // Prompt the user to enter a word for insertion
            Console.Write("Enter a word to insert: ");
            string wordToInsert = Console.ReadLine();

            // Initialize a stopwatch to measure the insertion time
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Validate the input and proceed with insertion if the input is valid
            if (!string.IsNullOrWhiteSpace(wordToInsert) && wordToInsert.All(char.IsLetter))
            {
                // Check if the dictionary already contains the word
                if (!dictionary.ContainsKey(wordToInsert))
                {
                    // Create a new Node object for the word and add it to the dictionary
                    Node node = new Node(wordToInsert);
                    dictionary.Add(wordToInsert, node);

                    // Stop the stopwatch and calculate the elapsed time
                    stopwatch.Stop();
                    TimeSpan elapsedTime = stopwatch.Elapsed;

                    // Display information about the insertion
                    Console.WriteLine($"Time taken for insertion: {elapsedTime.TotalMilliseconds} milliseconds");
                    Console.WriteLine($"Word '{wordToInsert}' inserted into the dictionary.");
                }
                else
                {
                    // Display a message if the word already exists in the dictionary
                    Console.WriteLine($"Word '{wordToInsert}' already exists in the dictionary. Skipping insertion.");
                }
            }
            else
            {
                // Display an error message for invalid input
                Console.WriteLine("Invalid input. Please enter a non-empty word containing only alphabetic characters.");
            }

            // Prompt the user to press any key to continue
            Console.Write("Press any key to continue: ");
            Console.ReadKey();
            return;
        }

        // Handles the search for a word in the dictionary
        static void DictionaryFindMenu(MyDictionary<string, Node> dictionary)
        {
            // Display a line break for better visual separation
            PrintLineBreak();

            // Prompt the user to enter the word to find
            Console.Write("Enter the word to find: ");
            string wordToFind = Console.ReadLine();

            // Initialize a stopwatch to measure the search time
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Perform the search operation in the dictionary
            var foundNode = dictionary.Find(wordToFind);

            // Stop the stopwatch and calculate the elapsed time
            stopwatch.Stop();
            TimeSpan elapsedTime = stopwatch.Elapsed;

            // Display the result of the search operation
            if (foundNode != null)
            {
                Console.WriteLine($"Word '{wordToFind}' found. Details: {foundNode.Word}, {foundNode.Length}");
            }
            else
            {
                Console.WriteLine($"Word '{wordToFind}' not found in the dictionary.");
            }

            // Display the time taken for the find operation
            Console.WriteLine($"Time taken for find operation: {elapsedTime.TotalMilliseconds} milliseconds");

            // Prompt the user to press any key to continue
            Console.Write("Press any key to continue: ");
            Console.ReadKey();
            return;
        }

        // Manages the deletion of words in the dictionary
        static void DictionaryDeleteMenu(MyDictionary<string, Node> dictionary)
        {
            // Display a line break for better visual separation
            PrintLineBreak();

            // Prompt the user for the deletion choice
            Console.WriteLine("What would you like to delete?");
            Console.WriteLine("1. A particular word");
            Console.WriteLine("2. Entire dictionary");

            Console.Write("Select an option: ");
            string deleteChoice = Console.ReadLine();

            switch (deleteChoice)
            {
                case "1":
                    // Prompt the user to enter the word to delete
                    PrintLineBreak();
                    Console.Write("Enter the word to delete: ");
                    string wordToDelete = Console.ReadLine();

                    // Delete the specified word from the dictionary
                    dictionary.Delete(wordToDelete);
                    break;

                case "2":
                    // Prompt the user for confirmation to delete the entire dictionary
                    PrintLineBreak();
                    Console.WriteLine("Are you sure you would like to delete the entire dictionary? This action cannot be undone.");
                    Console.Write("Y/N: ");

                    string confirmDelete = Console.ReadLine().ToLower();

                    if (confirmDelete == "y" || confirmDelete == "yes")
                    {
                        // Clear the entire dictionary
                        dictionary.Clear();
                        Console.WriteLine("Entire dictionary deleted.");
                    }
                    else if (confirmDelete == "n" || confirmDelete == "no")
                    {
                        Console.WriteLine("Returning to the main menu.");
                        Console.Write("Press any key to continue: ");
                        return;
                    }
                    else
                    {
                        // Display an error message for invalid input
                        DisplayErrorMessage();
                        return;
                    }
                    break;

                default:
                    // Display an error message for invalid input
                    DisplayErrorMessage();
                    return;
            }

            // Prompt the user to press any key to continue
            Console.Write("Press any key to continue: ");
            Console.ReadKey();
            return;
        }

        // Prints the contents of the dictionary
        public static void DictionaryPrint(MyDictionary<string, Node> dictionary)
        {
            // Display a line break for better visual separation
            PrintLineBreak();

            // Start a stopwatch to measure the time taken for the print operation
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Printing Dictionary Contents:");

            // Get the entries of the dictionary
            List<KeyValuePair<string, Node>> entries = dictionary.GetEntries();

            if (entries.Count > 0)
            {
                // Print the contents of the dictionary
                dictionary.Print();
            }
            else
            {
                Console.WriteLine("Dictionary is empty.");
            }

            // Stop the stopwatch and calculate the elapsed time
            stopwatch.Stop();
            TimeSpan elapsedTime = stopwatch.Elapsed;

            // Display a line break for better visual separation
            PrintLineBreak();

            Console.WriteLine($"Time taken: {elapsedTime.TotalMilliseconds} milliseconds");

            // Prompt the user to press any key to continue
            Console.Write("Press any key to continue: ");
            Console.ReadKey();
            return;
        }

        // ========================
        // Node Array Management
        // ========================

        // Manages the main menu for node array-related operations
        static void NodeArrayMainMenu()
        {
            while (true)
            {
                // Clear the console screen for a clean display
                Console.Clear();

                // Display the menu options for node array operations
                DisplayMenu(new string[] { "Load File", "Sort using Bubble Sort / O(n^2)", "Sort using Quick Sort / O(nlogn)", "Print Array", "Back" });

                // Get user input for menu selection
                string choice = Console.ReadLine();

                // Stopwatch to measure sorting time
                Stopwatch stopwatch = new Stopwatch();

                // Perform actions based on user choice
                switch (choice)
                {
                    case "1":
                        NodeArrayFileMenu();
                        break;

                    case "2":
                        // Sorting using BubbleSort
                        PrintLineBreak();
                        Console.WriteLine("Sorting using BubbleSort...");
                        stopwatch.Start();

                        BubbleSort(nodeArray);

                        stopwatch.Stop();

                        currentSortingMethod = "Bubble Sort";

                        Console.WriteLine($"Array sorted using Bubble Sort.");
                        Console.WriteLine($"Time taken: {stopwatch.Elapsed.TotalMilliseconds} milliseconds");
                        Console.Write("Press any key to continue: ");
                        Console.ReadKey();
                        break;

                    case "3":
                        // Sorting using QuickSort
                        PrintLineBreak();
                        Console.WriteLine("Sorting using QuickSort...");
                        stopwatch.Start();

                        QuickSort(nodeArray, 0, nodeArray.Length - 1);

                        stopwatch.Stop();

                        currentSortingMethod = "Quick Sort";

                        Console.WriteLine($"Array sorted using Quick Sort.");
                        Console.WriteLine($"Time taken: {stopwatch.Elapsed.TotalMilliseconds} milliseconds");
                        Console.Write("Press any key to continue: ");
                        Console.ReadKey();
                        break;

                    case "4":
                        // Print the node array
                        NodeArrayPrint(nodeArray, currentSortingMethod);
                        break;

                    case "5":
                        return;

                    default:
                        // Display an error message for invalid choices
                        DisplayErrorMessage();
                        break;
                }
            }
        }

        // Handles the file menu for inserting words into the node array
        static void NodeArrayFileMenu()
        {
            while (true)
            {
                // Print a line break for better readability
                PrintLineBreak();

                Console.WriteLine("Choose a file to insert into the node array:");

                // Get all files in the "random" folder
                string[] files = Directory.GetFiles("random");

                for (int i = 0; i < files.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {Path.GetFileName(files[i])}");
                }

                Console.Write("Select an option: ");
                string fileChoice = Console.ReadLine();

                int selectedFileIndex;
                if (int.TryParse(fileChoice, out selectedFileIndex) && selectedFileIndex >= 1 && selectedFileIndex <= files.Length)
                {
                    string filePath = files[selectedFileIndex - 1];
                    PrintLineBreak();

                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    Console.WriteLine($"Inserting words from {filePath} into the node array...");

                    // Read the file and insert words into the node array
                    NodeArrayReadFile(filePath);

                    stopwatch.Stop();
                    TimeSpan elapsedTime = stopwatch.Elapsed;

                    Console.WriteLine($"Time taken: {elapsedTime.TotalMilliseconds} milliseconds");

                    break;
                }
                else
                {
                    // Print a line break for better readability
                    PrintLineBreak();
                    DisplayErrorMessage();
                    return;
                }
            }
        }

        // Reads valid lines from a file and populates the nodeArray with Node objects
        static void NodeArrayReadFile(string filePath)
        {
            try
            {
                // Initialize a stopwatch to measure the execution time
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                // Read valid lines from the file using the ReadValidLinesFromFile method
                string[] validLines = ReadValidLinesFromFile(filePath);

                // Resize the nodeArray to accommodate the valid lines
                Array.Resize(ref nodeArray, validLines.Length);

                // Populate the nodeArray with Node objects created from valid lines
                for (int i = 0; i < validLines.Length; i++)
                {
                    Node node = new Node(validLines[i]);
                    nodeArray[i] = node;
                }

                // Reset the currentSortingMethod to "None" after reading the file
                currentSortingMethod = "None";

                // Stop the stopwatch and calculate the elapsed time
                stopwatch.Stop();
                TimeSpan elapsedTime = stopwatch.Elapsed;

                // Display information about the operation
                PrintLineBreak();
                Console.WriteLine($"{validLines.Length} words inserted into the node array successfully.");
                Console.WriteLine($"Time taken: {elapsedTime.TotalMilliseconds} milliseconds");
                Console.Write("Press any key to continue: ");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                // Display an error message if an exception occurs
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // Prints the contents of the nodeArray, including location, word, and length of each node
        static void NodeArrayPrint(Node[] nodeArray, string sortingMethod = "")
        {
            // Initialize a stopwatch to measure the printing time
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Iterate through the nodeArray and print information for each non-null element
            for (int i = 0; i < nodeArray.Length; i++)
            {
                if (nodeArray[i] != null)
                {
                    Console.WriteLine($"Array Location: {i}");
                    Console.WriteLine($"Word: {nodeArray[i].Word}");
                    Console.WriteLine($"Length: {nodeArray[i].Length}");
                    Console.WriteLine();
                }
            }

            // Stop the stopwatch to calculate the elapsed time
            stopwatch.Stop();
            TimeSpan elapsedTime = stopwatch.Elapsed;

            // Display information about the printing process
            Console.WriteLine($"Printed all elements in the node array (Sorted using {sortingMethod}).");
            Console.WriteLine($"Time taken: {elapsedTime.TotalMilliseconds} milliseconds");

            Console.Write("Press any key to continue: ");
            Console.ReadKey();
            return;
        }

        // ========================
        // Sorting Algorithms
        // ========================

        // Performs Bubble Sort on the given array of Nodes
        static void BubbleSort(Node[] nodeArray)
        {
            int n = nodeArray.Length;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    // Check if both nodes are not null before comparing
                    if (nodeArray[j] != null && nodeArray[j + 1] != null)
                    {
                        // Compare the words of two adjacent nodes
                        if (nodeArray[j].Word.CompareTo(nodeArray[j + 1].Word) > 0)
                        {
                            // Swap the nodes if they are in the wrong order
                            Node temp = nodeArray[j];
                            nodeArray[j] = nodeArray[j + 1];
                            nodeArray[j + 1] = temp;
                        }
                    }
                }
            }
        }

        // Performs Quick Sort on the given array of Nodes within the specified range
        static void QuickSort(Node[] nodeArray, int low, int high)
        {
            // Check if the low index is less than the high index to continue sorting
            if (low < high)
            {
                // Find the partition index where elements are correctly placed
                int partitionIndex = Partition(nodeArray, low, high);

                // Recursively sort the subarrays on both sides of the partition
                QuickSort(nodeArray, low, partitionIndex - 1);
                QuickSort(nodeArray, partitionIndex + 1, high);
            }
        }

        // Partitions the array of Nodes based on a pivot element, placing smaller elements to the left and larger elements to the right
        static int Partition(Node[] nodeArray, int low, int high)
        {
            // Select the pivot element (in this case, the last element in the array)
            Node pivot = nodeArray[high];
            int i = low - 1;

            // Iterate through the array elements
            for (int j = low; j < high; j++)
            {
                // Check for null elements to avoid potential issues
                if (nodeArray[j] != null && pivot != null)
                {
                    // Compare the current element with the pivot and rearrange if necessary
                    if (nodeArray[j].Word.CompareTo(pivot.Word) <= 0)
                    {
                        i++;
                        // Swap elements to maintain the partitioning
                        Node temp = nodeArray[i];
                        nodeArray[i] = nodeArray[j];
                        nodeArray[j] = temp;
                    }
                }
            }

            // Swap the pivot element to its correct position in the array
            if (nodeArray[i + 1] != null && pivot != null)
            {
                Node tempPivot = nodeArray[i + 1];
                nodeArray[i + 1] = nodeArray[high];
                nodeArray[high] = tempPivot;
            }

            // Return the index of the pivot element after partitioning
            return i + 1;
        }
    }
}