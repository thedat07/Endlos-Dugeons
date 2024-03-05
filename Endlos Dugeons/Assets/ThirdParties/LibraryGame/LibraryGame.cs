using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Events;
using System.Linq;
using System.Collections.Generic;

namespace LibraryGame
{
    public class UserData
    {
        public UserData()
        {
        }
    }

    public class User
    {
        public static UserData userData;

        public static void Load()
        {
            var json = PlayerPrefs.GetString("USER_DATA_GAME", string.Empty);
            if (!string.IsNullOrEmpty(json))
            {
                userData = JsonConvert.DeserializeObject<UserData>(json);
            }
            else
            {
                userData = new UserData();
            }
        }

        public static void Save()
        {
            var json = JsonConvert.SerializeObject(userData);
            PlayerPrefs.SetString("USER_DATA_GAME", json);
            PlayerPrefs.Save();
        }
    }

    public class Control
    {
        // Hold
        private float m_HoldTime = 0.2f;
        private bool m_IsHolding;
        private float m_CurrentTimeHeld;
        private Vector3 m_PointMouse;

        public UnityAction<Vector3> HoldAction;

        // Swipe
        private Vector2 m_TouchStartPos;
        private Vector2 m_TouchEndPos;

        public UnityAction<float> SwipeRightAction;
        public UnityAction<float> SwipeLeftAction;
        public UnityAction<float> SwipeUpAction;
        public UnityAction<float> SwipeDownAction;

        public Control()
        {
            m_HoldTime = 0.2f;
            m_IsHolding = false;
            m_CurrentTimeHeld = 0f;
            m_PointMouse = Vector2.zero;
            m_TouchStartPos = Vector2.zero;
            m_TouchEndPos = Vector2.zero;
        }

        public void Hold(float deltaTime, bool reset = false)
        {
            if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                if (!m_IsHolding)
                {
                    // Bắt đầu thời gian giữ
                    m_CurrentTimeHeld = 0f;
                    m_IsHolding = true;
                    m_PointMouse = GetInputPosition();
                }

                // Tăng thời gian đã giữ
                m_CurrentTimeHeld += deltaTime;

                // Kiểm tra nếu thời gian giữ đủ lâu
                if (m_CurrentTimeHeld >= m_HoldTime)
                {
                    var m_PointMouseNow = GetInputPosition();

                    // Xử lý sự kiện "hold" ở đây
                    HoldAction?.Invoke(m_PointMouseNow - m_PointMouse);

                    if (reset == true)
                    {
                        // Sau khi xử lý, có thể reset thời gian giữ và cờ
                        m_CurrentTimeHeld = 0f;
                        m_IsHolding = false;
                    }
                }
            }
            else
            {
                // Nếu không giữ nữa, reset thời gian giữ và cờ
                m_CurrentTimeHeld = 0f;
                m_IsHolding = false;
            }
        }

        public void Swipe()
        {
            // Kiểm tra sự kiện bắt đầu chạm
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                m_TouchStartPos = GetInputPosition();
            }

            // Kiểm tra sự kiện kết thúc chạm
            if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
            {
                m_TouchEndPos = GetInputPosition();

                // Tính toán vector di chuyển
                Vector2 swipeVector = m_TouchEndPos - m_TouchStartPos;

                // Tính toán khoảng cách (độ dài) của vector
                float swipeDistance = swipeVector.magnitude;

                // Kiểm tra nếu chiều dài của vector di chuyển đủ lớn để coi là một vuốt
                if (swipeVector.magnitude > 50) // Có thể thay đổi ngưỡng theo mong muốn của bạn
                {
                    // Xác định hướng vuốt (trái, phải, lên, xuống)
                    float xSwipe = swipeVector.x;
                    float ySwipe = swipeVector.y;

                    if (Mathf.Abs(xSwipe) > Mathf.Abs(ySwipe))
                    {
                        // Vuốt ngang
                        if (xSwipe > 0)
                        {
                            SwipeRightAction?.Invoke(swipeDistance);
                        }
                        else
                        {
                            SwipeLeftAction?.Invoke(swipeDistance);
                        }
                    }
                    else
                    {
                        // Vuốt dọc
                        if (ySwipe > 0)
                        {
                            SwipeUpAction?.Invoke(swipeDistance);
                        }
                        else
                        {
                            SwipeDownAction?.Invoke(swipeDistance);
                        }
                    }
                }
            }
        }

        Vector3 GetInputPosition()
        {
            Vector3 inputPosition;

            if (Application.isEditor)
            {
                // Nếu đang chạy trên Editor, sử dụng Input.mousePosition
                inputPosition = Input.mousePosition;
            }
            else
            {
                // Nếu đang chạy trên điện thoại hoặc thiết bị cảm ứng, sử dụng Touch
                inputPosition = Input.GetTouch(0).position;
            }

            return inputPosition;
        }
    }

    public class Canvas
    {
        public static Vector2 WorldToScreenPoint(Camera camera, RectTransform area, Vector3 point)
        {
            Vector2 screenPosition = camera.WorldToScreenPoint(point);

            screenPosition.x *= area.rect.width / (float)camera.pixelWidth;
            screenPosition.y *= area.rect.height / (float)camera.pixelHeight;

            return screenPosition - area.sizeDelta / 2f; ;
        }
    }

    public static class AbbrevationUtility
    {
        private static readonly SortedDictionary<int, string> abbrevations = new SortedDictionary<int, string>
    {
        {1000,"K"},
        {1000000, "M" },
        {1000000000, "B" }
    };

        public static string AbbreviateNumber(float number)
        {
            for (int i = abbrevations.Count - 1; i >= 0; i--)
            {
                KeyValuePair<int, string> pair = abbrevations.ElementAt(i);
                if (Mathf.Abs(number) >= pair.Key)
                {
                    int roundedNumber = Mathf.FloorToInt(number / pair.Key);
                    return roundedNumber.ToString() + pair.Value;
                }
            }
            return number.ToString();
        }
    }
}