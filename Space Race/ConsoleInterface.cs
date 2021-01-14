using System;
//DO NOT DELETE the two following using statements *********************************
using Game_Logic_Class;
using Object_Classes;


namespace Space_Race
{
    class Console_Class
    {
        /// <summary>
        /// Algorithm below currently plays only one game
        /// 
        /// when have this working correctly, add the abilty for the user to 
        /// play more than 1 game if they choose to do so.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {      
             DisplayIntroductionMessage();
            // Set up the board in Board class (Board.SetUpBoard)
            Board.SetUpBoard();
            /*Determine number of players - initally play with 2 for testing purposes 
            Create the required players in Game Logic class
             and initialize players for start of a game             
            loop  until game is finished           
               call PlayGame in Game Logic class to play one round
               Output each player's details at end of round
            end loop
            Determine if anyone has won
            Output each player's details at end of the game
          */
            do
            {
                int numberOfPlayers = GetInput_numberOfPlayers();
                SpaceRaceGame.NumberOfPlayers = numberOfPlayers;
                SpaceRaceGame.SetUpPlayers();
                int roundNumber = 0;
                SpaceRaceGame.GameOver= false;
                while (!SpaceRaceGame.GameOver)
                {
                    RoundStarter(roundNumber);
                    SpaceRaceGame.PlayOneRound();
                    DisplayResults();
                    roundNumber++;
                }
                DisplayRanking();
            }
            while(PlayAgain());

            GoodbyeMessage();           
                
            PressEnter();

        }//end Main

   
        /// <summary>
        /// Display a welcome message to the console
        /// Pre:    none.
        /// Post:   A welcome message is displayed to the console.
        /// </summary>
        static void DisplayIntroductionMessage()
        {
            Console.WriteLine("Welcome to Space Race.\n");
        } //end DisplayIntroductionMessage

        /// <summary>
        /// Displays a prompt and waits for a keypress.
        /// Pre:  none
        /// Post: a key has been pressed.
        /// </summary>
        static void PressEnter()
        {
            Console.Write("\nPress Enter to terminate program ...");
            Console.ReadLine();
        } // end PressAny

        static int GetInput_numberOfPlayers()
        {
            string usrInput = "";
            int numberOfPlayers = 0;

            Console.WriteLine("\tThis game is for 2 to 6 players.");
            Console.Write("\tNumber of players (2-6): ");
            usrInput = Console.ReadLine();

            if ((int.TryParse(usrInput, out numberOfPlayers)) && (numberOfPlayers >= SpaceRaceGame.MIN_PLAYERS) && (numberOfPlayers <= SpaceRaceGame.MAX_PLAYERS))
            {
                return numberOfPlayers;
            }
            else
            {
                Console.WriteLine("Error: Invalid number of players entered.");
                return GetInput_numberOfPlayers();
            }
        }
        static void DisplayResults()
        {
            foreach (Player player in SpaceRaceGame.Players)
            {
                Console.WriteLine(String.Format("\t{0} on square {1} with {2} yottawatt of power remaining", player.Name, player.Position, player.RocketFuel));
            }
        }
        static void RoundStarter(int round_number)
        {
            Console.WriteLine("\n\nPress Enter to play a round...");
            Console.ReadLine();
            if (round_number == 0)
            {
                Console.WriteLine("\n\tFirst Round\n");
            }
            else
            {
                Console.WriteLine("\n\tNext Round\n");
            }
        }
        static bool PlayAgain()
        {
            string userInput = "";

            Console.Write("\tPlay Again? (Y or N): ");
            userInput = Console.ReadLine();

            //return true or false based on user input - call method
            if (userInput == "Y" || userInput == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static void DisplayRanking()
        {
            //from : game logic - SpaceRaceGame
            //display for each player and wait for user to hit ENTER
            Console.WriteLine(SpaceRaceGame.DisplayGameResults());

            Console.WriteLine("\tIndividual players finished at the locations specified.");
            foreach (Player player in SpaceRaceGame.Players)
            {
                Console.WriteLine(String.Format("\n\t\t{0} with {1} yottawatt of power at square {2}", player.Name, player.RocketFuel, player.Position));
            }
            Console.WriteLine("\n\n\tPress Enter key to continue...");
            Console.ReadLine();
        }

        // end game gracefully with message
        static void GoodbyeMessage()
        {
            Console.WriteLine("\n\n\tThanks for playing Space Race.");
        }
    }//end Console class
}