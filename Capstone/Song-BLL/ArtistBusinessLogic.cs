namespace Capstone_BLL
{
    using System.Collections.Generic;

    public class ArtistBusinessLogic
    {
        //Method to calculate the average from a list of shorts
        public int CalculateAverageFromListOfShorts(List<short> iListOfNumbers)
        {
            //Declare new int
            int oAverage = 0;

            //If Statement to check if list was populated in DAL
            if (iListOfNumbers.Count >= 1)
            {
                //Foreach loop to add all values to oAverage
                foreach (short lNumber in iListOfNumbers)
                {
                    oAverage = oAverage + lNumber;
                }
                //Division to find average
                oAverage = oAverage / iListOfNumbers.Count;
            }
            else
            {
                //If it was not populated, leave oAverage set to a value of 0
            }

            //Return actual average
            return oAverage;
        }
    }
}