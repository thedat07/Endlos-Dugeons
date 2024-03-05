using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;

namespace ObserverSingletonMVP
{
    public interface IPresenter
    {
        // Định nghĩa các phương thức cần thiết cho Presenter
        void OnEnemyKilled();
    }

    public class ScorePresenter : MonoBehaviour, IPresenter
    {
        private IModel scoreModel;

        private void Start()
        {
            // Lấy tham chiếu đến ScoreModel từ GameManagerTest
            scoreModel = GameManagerTest.Instance.GetScoreModel();
        }

        [ContextMenu("Kill")]
        public void OnEnemyKilled()
        {
            // Xử lý sự kiện khi một kẻ thù bị tiêu diệt
            // (ví dụ: tăng điểm số)
            scoreModel.AddPoints(10);

            // Thông báo cho tất cả các Observer (trong trường hợp này là View)
            (scoreModel as Subject)?.NotifyObservers();
        }
    }
}