using System.Drawing;
using System.ComponentModel;
using Object_Classes;


namespace Game_Logic_Class
{
    public static class SpaceRaceGame
    {
        // Minimum and maximum number of players.
        public const int MIN_PLAYERS = 2;
        public const int MAX_PLAYERS = 6;

        private static int numberOfPlayers = 2;  //default value for test purposes only 
        public static int NumberOfPlayers
        {
            get
            {
                return numberOfPlayers;
            }
            set
            {
                numberOfPlayers = value;
            }
        }

        public static string[] names = { "One", "Two", "Three", "Four", "Five", "Six" };  // default values

        // Only used in Part B - GUI Implementation, the colours of each player's token
        private static Brush[] playerTokenColours = new Brush[MAX_PLAYERS] { Brushes.Yellow, Brushes.Red,
                                                                       Brushes.Orange, Brushes.White,
                                                                      Brushes.Green, Brushes.DarkViolet};
        /// <summary>
        /// A BindingList is like an array which grows as elements are added to it.
        /// </summary>
        private static BindingList<Player> players = new BindingList<Player>();
        public static BindingList<Player> Players
        {
            get
            {
                return players;
            }
        }

        // The pair of die
        private static Die die1 = new Die(), die2 = new Die();


        /// <summary>
        /// Set up the conditions for this game as well as
        ///   creating the required number of players, adding each player 
        ///   to the Binding List and initialize the player's instance variables
        ///   except for playerTokenColour and playerTokenImage in Console implementation.
        ///   
        ///     
        /// Pre:  none
        /// Post:  required number of players have been initialsed for start of a game.
        /// </summary>
        public static void SetUpPlayers()
        {
            /*
            for number of players
                create a new player object
                initialize player's instance variables for start of a game
                add player to the binding list
            */
            /*Clear players, add new players to new game for number selected
             * from array, place on square
             */

            Players.Clear();
            for (int livePlayer = 0; livePlayer < NumberOfPlayers; livePlayer++)
            {
                Player Current_player = new Player(names[livePlayer]);
                Current_player.Position = Board.START_SQUARE_NUMBER; 
                Current_player.Location = Board.Squares[Board.START_SQUARE_NUMBER];
                //power & fuel              
                Current_player.HasPower = true;
                Current_player.RocketFuel = Player.INITIAL_FUEL_AMOUNT;

                Current_player.PlayerTokenColour = playerTokenColours[livePlayer];

                players.Add(Current_player);
            }
        }


        /// <summary>
        ///  Plays one round of a game
        /// </summary>
        public static void PlayOneRound()
        {
            if (SingleStep)
            {
                Players[Player_number].Play(die1, die2);
                if (Player_number == (NumberOfPlayers - 1))
                {
                    GameOverCheck();
                    Player_number = 0;

                }
                else
                {
                    Player_number++;
                }
            }
            else
            {
                foreach (Player player in Players)
                {
                    player.Play(die1, die2);
                }
                GameOverCheck();
            }
        }

        private static bool gameOver;
        public static bool GameOver
        {
            get
            {
                return gameOver;
            }

            set
            {
                gameOver = value;
            }

        }
        private static bool singleStep;
        public static bool SingleStep
        {
            get
            {
                return singleStep;
            }
            
            set
            {
                singleStep = value;
            }
        }
        private static int playerNum = 0;
        public static int Player_number
        {
            get
            {
                return playerNum;
            }

            set
            {
                playerNum = value;
            }
        }

        public static string resultText { get; private set; }

        public static string DisplayGameResults()
        {
            bool playerReachedEnd = false;
            string resultText = " ";
            string winners = "";

            foreach (Player player in Players)
            {
                if (player.AtFinish)
                {
                    winners += string.Format("\n\t\t{0}", player.Name);
                    playerReachedEnd = true;
                }
            }

            if (playerReachedEnd) 
            {
                resultText += ("\n\n\tThe following player(s) finished the game.\n");
                resultText += (winners + "\n\n");
            }
            else 
            //exception statement
            {
                resultText += ("\n\n\tNo players finished the game\n");
            }

            return resultText;
        }


        /// checks if game is over based on player position and fuel amount
        private static void GameOverCheck()
        {
            bool outOfPower_forAll = true;

            foreach (Player player in Players)
            {
                //single player check
                if (player.HasPower)
                {
                    outOfPower_forAll = false;
                }
                if (player.AtFinish || outOfPower_forAll)
                {
                    GameOver = true;
                }
            }
        }




    }//end SnakesAndLadders
}
