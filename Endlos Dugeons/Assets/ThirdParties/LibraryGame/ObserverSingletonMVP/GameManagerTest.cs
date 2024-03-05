using DesignPatterns;

namespace ObserverSingletonMVP
{
    public interface IModel
    {
        // Định nghĩa các phương thức cần thiết cho Model
        int GetScore();
        void AddPoints(int points);
    }

    public class Model : ConcreteSubject, IModel
    {
        // Triển khai các phương thức của IModel
        private int score;

        public int GetScore()
        {
            return score;
        }

        public void AddPoints(int points)
        {
            score += points;
        }
    }

    public class GameManagerTest : SingletonMaster<GameManagerTest>
    {
        private IModel scoreModel;

        public override void Awake()
        {
            base.Awake();
            // Tạo một instance mới của ScoreModel khi GameManager được tạo
            scoreModel = new Model();
        }

        public IModel GetScoreModel()
        {
            return scoreModel;
        }
    }
}