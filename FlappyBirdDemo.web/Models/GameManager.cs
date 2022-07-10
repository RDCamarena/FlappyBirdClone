

namespace FlappyBirdDemo.web.Models
{
    public class GameManager 
    {
        private readonly int _gravity = 2;

        public event EventHandler MainLoopCompleated;
        

        public BirdModel Bird { get; private set; }
        public List<PipeModel> Pipes { get; private set; } 
        public bool IsRunning { get; private set; } = false;


        public GameManager()
        {
            Bird = new BirdModel();
            Pipes = new List<PipeModel>();
        }

        public async void MainLoop()
        {   
            IsRunning = true;
            while (IsRunning)
            {

                MoveObjs();

                CheckForCollisons();

                ManagePipes();


                MainLoopCompleated?.Invoke(this, EventArgs.Empty);

                await Task.Delay(20);

            }
        }


        public void StartGame()
        {
            if (!IsRunning)
            {
                Bird = new BirdModel();
                Pipes = new List<PipeModel>();
                MainLoop();
            }
            
        }


        public void Jump()
        {
            if (IsRunning)
            {
                Bird.Jump();
            }
        }

        void ManagePipes()
        {
            if (!Pipes.Any() || Pipes.Last().DistanceFromLeft <= 250)
            {
                Pipes.Add(new PipeModel());
            }
            if (Pipes.First().IsOffScreen())
            {
                Pipes.Remove(Pipes.First());
            }
        }

        void CheckForCollisons()
        {
            if (Bird.IsOnGround())
            {
                GameOver();

            }

            var centeredPipe = Pipes.FirstOrDefault(p => p.IsCentered());

            if (centeredPipe != null)
            {
                bool hasCollidedWithBottom = Bird.DistanceFromGround < centeredPipe.GapBottom - 150;
                bool hasCollidedWithTop = Bird.DistanceFromGround + 45 > centeredPipe.GapTop - 150;
                if(hasCollidedWithBottom || hasCollidedWithTop)
                {
                    GameOver();
                }
            }
        }
        void MoveObjs()
        {
            Bird.Fall(_gravity);


            foreach (var pipe in Pipes)
            {
                pipe.Move();
            }
        }
        public void GameOver()
        {
            IsRunning = false;
        }
    }
}
