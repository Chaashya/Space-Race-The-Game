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
                int player_num = AskForPlayers();
                SpaceRaceGame.NumberOfPlayers = player_num;
                SpaceRaceGame.SetUpPlayers();
                int round_num = 0;
                SpaceRaceGame.GameFinished = false;
                while (!SpaceRaceGame.GameFinished)
                {
                    RoundStart(round_num);
                    SpaceRaceGame.PlayOneRound();
                    ShowRoundResults();
                    round_num++;
                }
                ShowGameResults();
            }
            while (PlayAgain());

            ThankyouMsg();           
                
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

        static int AskForPlayers()
        {
            string raw_input = "";
            int player_num = 0;

            Console.WriteLine("\tThis game is for 2 to 6 players.");
            Console.Write("\tNumber of players (2-6): ");
            raw_input = Console.ReadLine();

            if ((int.TryParse(raw_input, out player_num)) && (player_num >= SpaceRaceGame.MIN_PLAYERS) && (player_num <= SpaceRaceGame.MAX_PLAYERS))
            {
                return player_num;
            }
            else
            {
                Console.WriteLine("Error: Invalid number of players entered.");
                return AskForPlayers();
            }
        }
        static void ShowRoundResults()
        {
            foreach (Player player in SpaceRaceGame.Players)
            {
                Console.WriteLine(String.Format("\t{0} on square {1} with {2} yottawatt of power remaining", player.Name, player.Position, player.RocketFuel));
            }
        }
        static void RoundStart(int round_number)
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


            //Calls Method again, to play game again
            if (userInput == "Y" || userInput == "y")
            {
                return true;
            }


            //Terminate Game
            else
            {
                return false;
            }
        }
        static void ShowGameResults()
        {
            //Display the results of the game (see SpaceRaceGame.cs)
            Console.WriteLine(SpaceRaceGame.DisplayGameResults());


            //Individual Results
            Console.WriteLine("\tIndividual players finished at the locations specified.");
            foreach (Player player in SpaceRaceGame.Players)
            {
                Console.WriteLine(String.Format("\n\t\t{0} with {1} yottawatt of power at square {2}", player.Name, player.RocketFuel, player.Position));
            }


            //Wait for the user to press enter
            Console.WriteLine("\n\n\tPress Enter key to continue...");
            Console.ReadLine();
        }


        /// <summary>
        /// Display the exit message.
        /// </summary>
        static void ThankyouMsg()
        {


            Console.WriteLine("\n\n\tThanks for playing Space Race");


        }
    }//end Console class
}