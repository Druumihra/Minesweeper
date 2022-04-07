using System;
using System.Threading;

namespace Minesweeper
{

    public class Player
    {
        public string name;
        public int score;
    }
    class Program
    {
        static void Main(string[] args)
        {
            int xlength = 0;
            int ylength = 0;
            int i = 0;
            int j = 0;
            int surroundingbombs = 0;
            string nameinput = "";
            bool playing = true;
            int playerscore = 0;

            Player player = new Player();

            Console.WriteLine("Please input player name");
            nameinput = Console.ReadLine();
            player.name = nameinput;
            Console.Clear();
            while (playing)
            {
                Console.WriteLine($"Welcome to Minesweeper, {player.name}!");
                Console.WriteLine("1: Play game \n2: Scores\n3: Exit");
                char choice = Console.ReadKey().KeyChar;
                Console.Clear();

                
              
                
                

                if (choice == '1')
                {
                    bool successgridx = false;
                    bool successgridy = false;
                    int xgridsize;
                    int ygridsize;
                    
                    while (!successgridx)
                    {
                        Console.WriteLine("Please input X length. Minumum size: 5. Max size: 40");
                        successgridx = int.TryParse(Console.ReadLine(), out xgridsize);
                        xlength = xgridsize;
                        
                    }

                    if (xlength > 40)
                    {
                        xlength = 40;
                    }

                    
                    while (!successgridy)
                    {
                        Console.WriteLine("Please input Y length. Minumum size: 5. Max size: 40");
                        successgridy = int.TryParse(Console.ReadLine(), out ygridsize);
                        ylength = ygridsize;
                    }
                    
                    if (ylength > 40)
                    {
                        ylength = 40;
                    }

                    int[,] hiddenmap = Program.Mapgen(xlength, ylength);

                    string[,] emptymap = new string[xlength, ylength];


                    while (i < xlength)
                    {
                        while (j < ylength)
                        {
                            emptymap[i, j] = "[ ]";
                            j++;
                        }
                        j = 0;
                        i++;
                    }
                    i = 0;
                    j = 0;

                    //loop that prints out map on screen every time it has been updated
                    bool life = true;
                    while (life)
                    {
                        Console.Clear();
                        bool xgrid = true;
                        bool ygrid = true;
                        while (j < ylength)
                        {

                            while (i < xlength)
                            {
                                
                                if (xgrid)
                                {
                                    int k = 0;
                                    Console.Write("  ");
                                    while (k < xlength)
                                    {                                                                       
                                        if (k+1 < 10)
                                        {
                                            Console.Write($"   0{k+1}");
                                        }
                                        else { 
                                        Console.Write($"   {k+1}");
                                        }
                                        k++;
                                    }
                                    xgrid = false;
                                    Console.Write("\n");
                                }
                                while (ygrid)
                                {
                                    if (j+1 < 10)
                                    {
                                        Console.Write($"0{j + 1}");
                                    }
                                    else 
                                    { 
                                    Console.Write($"{j + 1}");
                                    }
                                    ygrid = false;
                                }
                                                           

                                Console.Write($"  {emptymap[i, j]}");
                                i++;
                            }
                            Console.Write("\n");
                            j++;
                            i = 0;
                            ygrid = true;
                        }
                        
                        bool userturn = true;
                        while (userturn)
                        {

                            int userinputx = 0;
                            int userinputy = 0;
                            bool successx = false;
                            bool successy = false;
                            j = 0;

                            Console.WriteLine("Please input X and Y coordinate to check square");
                           while (!successx)
                            {
                                Console.WriteLine("X:");
                                successx = int.TryParse(Console.ReadLine(), out userinputx);
                            }
                            userinputx -= 1;

                            while (!successy) 
                            { 
                                Console.WriteLine("Y:");
                                successy = int.TryParse(Console.ReadLine(), out userinputy);
                            }
                            userinputy -= 1;
                            Console.Clear();
                            //if statement that checks user input against the hiddenmap as to confirm whether a bomb is there or not

                            if (hiddenmap[userinputx, userinputy] == 1)
                            {
                                if (player.score < playerscore)
                                {
                                    player.score = playerscore;
                                }
                                Console.Clear();
                                Console.WriteLine("BOOM! Twas as bomb.");
                                Console.WriteLine("You lost");
                                Thread.Sleep(2000);
                                Console.Clear();


                                userturn = false;
                                life = false;

                            }
                            else
                            {
                                //checks the 8 squares around players chosen tile to see amount of bombs
                                //encase them in if statements that check if they would create an out of bounds error TO DO
                                //The IF statements should check each corner around the inputted array point to see if its out of bounds.
                                // [x-1, y] [x+1, y] [x, y-1] [x, y+1] [x-1, y-1] [x-1, y+1] [x+1, y-1] [x+1, y+1]


                                //checks if both side x and y are less than zero then checks squares around point (top left)
                                if (userinputx - 1 < 0 && userinputy - 1 < 0)
                                {
                                    if (hiddenmap[userinputx + 1, userinputy] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx, userinputy + 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx + 1, userinputy + 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                }
                                //checks if X coord leaves array and if y coord is too small (top right)
                                else if (userinputx + 2 > xlength && userinputy - 1 < 0)
                                {
                                    if (hiddenmap[userinputx - 1, userinputy] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx - 1, userinputy + 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx, userinputy + 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                }
                                // checks if x coord is too small and if y coord leaves array (bottom left)
                                else if (userinputx - 1 < 0 && userinputy + 2 > ylength)
                                {
                                    if (hiddenmap[userinputx + 1, userinputy - 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx, userinputy - 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx + 1, userinputy] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                }
                                // checks if x coord leaves array and if y coord leaves (bottom right)
                                else if (userinputx + 2 > xlength && userinputy + 2 > ylength)
                                {
                                    if (hiddenmap[userinputx - 1, userinputy] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx - 1, userinputy - 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx, userinputy - 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                }
                                // checks if coords leave array on the left side
                                else if (userinputx - 1 < 0)
                                {
                                    if (hiddenmap[userinputx + 1, userinputy] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx, userinputy + 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx, userinputy - 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx + 1, userinputy + 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx + 1, userinputy - 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                }
                                // checks if coords leave array on the right side 
                                else if (userinputx + 2 > xlength)
                                {
                                    if (hiddenmap[userinputx - 1, userinputy] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx, userinputy + 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx, userinputy - 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx - 1, userinputy + 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx - 1, userinputy - 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                }
                                // checks if coords leave array at the top
                                else if (userinputy - 1 < 0)
                                {
                                    if (hiddenmap[userinputx - 1, userinputy] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx + 1, userinputy] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx - 1, userinputy + 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx, userinputy + 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx + 1, userinputy + 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                }

                                // checks if coords leave array at the bottom
                                else if (userinputy + 2 > ylength)
                                {
                                    if (hiddenmap[userinputx - 1, userinputy] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx + 1, userinputy] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx - 1, userinputy - 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx, userinputy - 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx + 1, userinputy - 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                }
                                else
                                {
                                    if (hiddenmap[userinputx - 1, userinputy] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx + 1, userinputy] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx, userinputy - 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx, userinputy + 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx - 1, userinputy - 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx - 1, userinputy + 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx + 1, userinputy - 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                    if (hiddenmap[userinputx + 1, userinputy + 1] == 1)
                                    {
                                        surroundingbombs++;
                                    }
                                }
                            
                                playerscore += 10;
                                emptymap[userinputx, userinputy] = $"[{surroundingbombs}]";
                                surroundingbombs = 0;
                                userturn = false;
                                
                            } 

                        }
                    }
                } 




               else if (choice == '2')
               {
                    
                  Console.WriteLine($"{player.name} got a score of {player.score}");
                  Thread.Sleep(2000);
                  Console.Clear();
                
               }
                               
                else if (choice == '3')
                {
                    playing = false;
                }

                else
                {
                    Console.Clear();
                    Console.WriteLine("Error: Please pick 1 or 2 or 3");
                    Console.WriteLine("1: Play game \n2: Exit");
                                       
                }
            }
        }



      public static int[,] Mapgen(int x, int y)
      {
            //forcefully sets a minimum map size if its too small 
            if (x < 5)
            {
                x = 5;
            }
            if (y < 5)
            {
                y = 5;
            }
            int[,] map = new int[x, y];
            int bombs = map.Length / 6;
            Random rnd = new Random();
            int b = 0;
            int i = 0;
            int j = 0;



            //sets all map coordinate values to 0
            while (i < x)
            {

                while (j < y)
                {
                    map[i, j] = 0;
                    j++;
                }
                j = 0;
                i++;
            }

            //places bombs on the map with the value of 1
            while (b < bombs) 
            {

                int xcoord = rnd.Next(0, x-1);
                int ycoord = rnd.Next(0, y-1);
                
                if (map[xcoord,ycoord] != 1)
                {
                    map[xcoord, ycoord] = 1;
                    b++;
                }

            }
                return map;
      }
    }
}
