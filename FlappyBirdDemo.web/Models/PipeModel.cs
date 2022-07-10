namespace FlappyBirdDemo.web.Models
{
    public class PipeModel
    {  
        public int DistanceFromLeft { get; private set; } = 500;
        public int DistanceFromBottom { get; private set; } = new Random().Next(60);
        public int Speed { get; private set; } = 2;
        public int Gap { get; private set; } = 130;

        public int GapBottom => DistanceFromBottom + 300;
        public int GapTop => GapBottom + Gap;


        public void Move()
        {
            Console.WriteLine(DistanceFromBottom);
            DistanceFromLeft -= Speed;
        }

        public bool IsOffScreen()
        {
            return DistanceFromLeft <= -60;
        }

        public bool IsCentered()
        {
            bool hasEnteredCenter = DistanceFromLeft <= (500 / 2) + (60 / 2);
            bool hasExitCenter = DistanceFromLeft <= (500 / 2) - (60 / 2) - 60;


            return hasEnteredCenter && !hasExitCenter;
        }
    }

}


