namespace TRSimulation
{
    public class Obstacle
    {
        public int x;
        public int y;
        public bool placed;

        public  Obstacle()
         {
            x = -1;
            y = -1;
            placed = false;
         }
        public void Place()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 5);
            int randomNUmber2 = random.Next(0, 5);
            x = randomNumber;
            y = randomNUmber2;
            placed = true;
        }
    }
}
