using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using TMPro;

namespace ObserverSingletonMVP
{
    public class ViewTest2 : MonoBehaviour, IView
    {
        private IModel scoreModel;

        private void Start()
        {
            // Lấy tham chiếu đến ScoreModel từ GameManagerTest
            scoreModel = GameManagerTest.Instance.GetScoreModel();

            // Đăng ký làm Observer khi khởi tạo
            (scoreModel as Subject)?.AddObserver(this);
            // Cập nhật hiển thị điểm số khi bắt đầu
            UpdateView();
        }


        public void OnNotify()
        {
            UpdateView();
        }

        public void UpdateView()
        {
            // Cập nhật giao diện người dùng dựa trên trạng thái của Model
            int currentScore = GameManagerTest.Instance.GetScoreModel().GetScore();
            // Cập nhật giao diện người dùng với điểm số hiện tại
            // (ví dụ: cập nhật text trên UI)
            Debug.Log("Updated UI with Score2: " + currentScore);
        }

        private void OnDestroy()
        {
            // Hủy đăng ký khi Observer bị hủy
            (scoreModel as Subject)?.RemoveObserver(this);
        }
    }
}