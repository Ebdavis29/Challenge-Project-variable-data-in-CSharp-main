using System;
using System.Linq;
using System.Threading;

// Initialize the ourAnimals array
string animalSpecies = "";
string animalID = "";
string animalAge = "";
string animalPhysicalDescription = "";
string animalPersonalityDescription = "";
string animalNickname = "";
string suggestedDonation = "";

int maxPets = 8;
string? readResult;
string menuSelection = "";
decimal decimalDonation = 0.00m;
string[,] ourAnimals = new string[maxPets, 7];

// Sample data for ourAnimals array
for (int i = 0; i < maxPets; i++)
{
    switch (i)
    {
        case 0:
            animalSpecies = "dog";
            animalID = "d1";
            animalAge = "2";
            animalPhysicalDescription = "medium sized cream colored female golden retriever weighing about 45 pounds. housebroken.";
            animalPersonalityDescription = "loves to have her belly rubbed and likes to chase her tail. gives lots of kisses.";
            animalNickname = "lola";
            suggestedDonation = "85.00";
            break;

        case 1:
            animalSpecies = "dog";
            animalID = "d2";
            animalAge = "9";
            animalPhysicalDescription = "large reddish-brown male golden retriever weighing about 85 pounds. housebroken.";
            animalPersonalityDescription = "loves to have his ears rubbed when he greets you at the door, or at any time! loves to lean-in and give doggy hugs.";
            animalNickname = "gus";
            suggestedDonation = "49.99";
            break;

        case 2:
            animalSpecies = "cat";
            animalID = "c3";
            animalAge = "1";
            animalPhysicalDescription = "small white female weighing about 8 pounds. litter box trained.";
            animalPersonalityDescription = "friendly";
            animalNickname = "snow";
            suggestedDonation = "40.00";
            break;

        case 3:
            animalSpecies = "cat";
            animalID = "c4";
            animalAge = "";
            animalPhysicalDescription = "";
            animalPersonalityDescription = "";
            animalNickname = "lion";
            suggestedDonation = "";
            break;

        default:
            animalSpecies = "";
            animalID = "";
            animalAge = "";
            animalPhysicalDescription = "";
            animalPersonalityDescription = "";
            animalNickname = "";
            suggestedDonation = "";
            break;
    }

    ourAnimals[i, 0] = "ID #: " + animalID;
    ourAnimals[i, 1] = "Species: " + animalSpecies;
    ourAnimals[i, 2] = "Age: " + animalAge;
    ourAnimals[i, 3] = "Nickname: " + animalNickname;
    ourAnimals[i, 4] = "Physical description: " + animalPhysicalDescription;
    ourAnimals[i, 5] = "Personality: " + animalPersonalityDescription;

    if (!decimal.TryParse(suggestedDonation, out decimalDonation))
    {
        decimalDonation = 45.00m; // if suggestedDonation NOT a number, default to 45.00
    }
    ourAnimals[i, 6] = $"Suggested Donation: {decimalDonation:C2}";
}

// Main menu loop
do
{
    // Console.Clear() might throw an exception in some debug environments
    Console.Clear();

    Console.WriteLine("Welcome to the Contoso PetFriends app. Your main menu options are:");
    Console.WriteLine(" 1. List all of our current pet information");
    Console.WriteLine(" 2. Display all dogs with a specified characteristic");
    Console.WriteLine();
    Console.WriteLine("Enter your selection number (or type Exit to exit the program)");

    readResult = Console.ReadLine();
    menuSelection = readResult?.ToLower() ?? "";

    // Handle menu selections
    switch (menuSelection)
    {
        case "1":
            // List all pet info
            for (int i = 0; i < maxPets; i++)
            {
                if (ourAnimals[i, 0] != "ID #: ")
                {
                    Console.WriteLine();
                    for (int j = 0; j < 7; j++)
                    {
                        Console.WriteLine(ourAnimals[i, j].ToString());
                    }
                }
            }

            Console.WriteLine("\r\nPress the Enter key to continue");
            Console.ReadLine();
            break;

        case "2":
            // #1 Gather multiple search terms
            string? dogCharacteristics = "";
            while (string.IsNullOrWhiteSpace(dogCharacteristics))
            {
                Console.WriteLine("\r\nEnter dog characteristics to search for, separated by commas:");
                dogCharacteristics = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(dogCharacteristics))
                {
                    Console.WriteLine("Search terms cannot be empty. Please enter valid search terms.");
                }
            }

            // #2 Separate, trim, and sort search terms
            string[] searchTerms = dogCharacteristics
                .Split(',')
                .Select(term => term.Trim().ToLower())
                .Where(term => !string.IsNullOrWhiteSpace(term))
                .Distinct()
                .OrderBy(term => term)
                .ToArray();

            bool foundAnyMatch = false;

            // #4 "Rotating" animation with countdown
            string[] searchingIcons = { "|", "/", "-", "\\" };

            // Loop through animals array to search for matching animals
            for (int i = 0; i < maxPets; i++)
            {
                if (ourAnimals[i, 1].Contains("dog"))
                {
                    string dogDescription = ourAnimals[i, 4] + "\r\n" + ourAnimals[i, 5];
                    bool dogMatchFound = false;

                   for (int countdown = 2; countdown >= 0; countdown--)
{
    foreach (string icon in searchingIcons)
    {
        //animation with countdown updated code
        Console.Write($"\rSearching our dog {ourAnimals[i, 3]} for terms {string.Join(", ", searchTerms)} {icon} {countdown}  ");
        Thread.Sleep(250);
    }
}


                    // #3a Iterate over search terms and check for matches
                    foreach (string term in searchTerms)
                    {
                        if (dogDescription.Contains(term))
                        {
                            // #3b Update message to reflect term
                            Console.WriteLine($"\nOur dog {ourAnimals[i, 3]} is a match for your search for '{term}'!");
                            dogMatchFound = true;
                            foundAnyMatch = true;
                        }
                    }

                    // #3d If matches are found, write match message + dog description
                    if (dogMatchFound)
                    {
                        Console.WriteLine($"\n{ourAnimals[i, 3]}'s description:\n{dogDescription}\n");
                    }
                }
            }

            if (!foundAnyMatch)
            {
                Console.WriteLine("No matches found for any available dogs.");
            }

            Console.WriteLine("\n\rPress the Enter key to continue");
            Console.ReadLine();
            break;

        default:
            break;
    }

} while (menuSelection != "exit");
