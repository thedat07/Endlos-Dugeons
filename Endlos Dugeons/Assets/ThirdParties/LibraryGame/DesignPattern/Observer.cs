using System.Collections.Generic;
namespace DesignPatterns
{   
    public interface IObserver
    {
        void OnNotify();
    }

    public interface Subject
    {
        List<IObserver> Observers { get; }

        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers();
    }

    public class ConcreteSubject : Subject
    {
        private List<IObserver> observers = new List<IObserver>();

        public List<IObserver> Observers => observers;

        public void AddObserver(IObserver observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.OnNotify();
            }
        }
    }
}