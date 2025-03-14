/* Possible CA2 Solution
 * Author : Therese Hume
 */

using System.Text;

namespace CA2_solution_2025
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] usersArray = new string[100]; 
            int[] stepsArray = new int[100];       

            string[] rangeDescriptions = { "Sedentary", "Lightly Active", "Moderately Active", "Active", "Very Active", "Highly Active" };
            int[] rangeBoundaries = { 0, 4000, 8000, 12000, 15000, 20000 };

            int count = ReadUsers(usersArray, stepsArray);           
            GenerateReport(usersArray,stepsArray,count,rangeDescriptions,rangeBoundaries);
        }

/// <summary>
/// This method reads and validates the input of users and steps from the console, returning the number of user details read in.
/// </summary>
/// <param name="usersArray"> array of users </param>
/// <param name="stepsArray">corresponding array of steps </param>
/// <returns></returns>
        static int ReadUsers(string[] usersArray, int[] stepsArray)
        {
            string input = "";
            int steps;
            int count = 0;       

            Console.WriteLine("Please enter User ID");
            input = Console.ReadLine();

            while (input.ToUpper() != "U000")
            {
                while (!IsValidUser(input))
                {
                    Console.WriteLine("Invalid input: Should be of length 4 and begin with a U.");
                    Console.WriteLine("Please enter User ID");
                    input = Console.ReadLine();
                }
                if (input == "U000") break;// exit if terminating

                Console.WriteLine("Please enter Number of Steps");

                while ((!int.TryParse(Console.ReadLine(), out steps)) || (steps < 0))
                {
                    Console.WriteLine("Invalid input");
                    Console.WriteLine("Please enter Number of Steps");
                }
                usersArray[count] = input;
                stepsArray[count] = steps;
                count++;

                Console.WriteLine("Please enter User ID");
                input = Console.ReadLine();
            }
            return count;
        }

        /// <summary>
        /// This method checks that a user ID is a valid one (length 4, starts with U)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        static bool IsValidUser(string userId)
        {
            if ((Char.ToUpper(userId[0]) == 'U') && (userId.Length == 4))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// This method generates a report on user and steps data, based on categories provided
        /// </summary>
        /// <param name="users">user IDs</param>
        /// <param name="steps">corresponding steps for each user </param>
        /// <param name="count">number of users/steps</param>
        /// <param name="rangeDescriptions">description of each step category </param>
        /// <param name="rangeBoundaries">lower boundary of each step category</param>
        static void GenerateReport(string[] users, int[] steps, int count, string[] rangeDescriptions, int[] rangeBoundaries)
        {
            
            int[] rangeCounters= new int[rangeBoundaries.Length]; // counters for each range
            int[] rangeTotals = new int[rangeDescriptions.Length];// overall total steps for each range

            int biggestIndex = 0; // stores the index of the largest number of steps
            int totalSteps = 0;

            if (count > 0) 
            {

                for (int i = 0; i < count; i++)
                {

                    //Check for the user with the most steps.

                    if (steps[i] > steps[biggestIndex])
                    {
                        biggestIndex = i;
                    }

                    // Count the number of users and steps for each band

                    int index = GetRangeIndex(steps[i], rangeBoundaries);
                   
                    rangeCounters[index]++;
                    rangeTotals[index] += steps[i];
                    totalSteps += steps[i];
                }

                //Print the report

                Console.WriteLine("Statistics");
                Console.Write($"\n {"Range",-25}{"From-To",-15}{"Users",-7}{"Average",-7}");

                for (int i = 0; i < rangeCounters.Length; i++)
                {
                    if (i != rangeCounters.Length - 1)
                    {
                        Console.Write($"\n {rangeDescriptions[i],-25}{rangeBoundaries[i],-7}-{rangeBoundaries[i + 1],-7}{rangeCounters[i],-7}");
                    }
                    else
                    {
                        Console.Write($"\n {rangeDescriptions[i],-25}{rangeBoundaries[i],-7}{"++ ",-7}{rangeCounters[i],-7}");
                    }

                    // Print the average, if applicable.

                    if (rangeCounters[i] > 0)
                    {
                        Console.Write($"{rangeTotals[i] / rangeCounters[i],-7}");
                    }

                }

            }
            else
            {
                Console.WriteLine("No Data");
            }
        }
    
/// <summary>
/// This method
/// </summary>
/// <param name="steps"> number of steps</param>
/// <param name="ranges">this array contains the lower boundaries of the ranges of each category</param>
/// <returns>the index of the category that the number of steps falls in, -1 if none of them </returns>
    static int GetRangeIndex(int steps, int[] ranges)
    {
        for (int i = 0; i < ranges.Length - 1; i++)
        {
            if ((steps >= ranges[i]) && (steps < ranges[i + 1]))
            {
                return i;
            }
          
        }
        if (steps >= ranges[ranges.Length - 1])
                return ranges.Length - 1;

        return -1;
    }
}
}
