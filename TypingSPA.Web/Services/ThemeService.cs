using MudBlazor;
using System;

namespace TypingSPA.Web.Services
{
    public class ThemeService
    {

        public DarkModeObservable IsDarkModeObservable { get; set; }

        public MudTheme Theme { get; set; }
        public ThemeService() { 
            Theme = new MudTheme();
            IsDarkModeObservable = new DarkModeObservable();
        }

        /// <summary>
        /// control darkmode subscription to scoped service.
        /// </summary>
        /// <param name="OnDarkModeChange"></param>
        /// <returns></returns>
        public IDisposable AddDarkModeSubscription(Action<bool> OnDarkModeChange) 
        {
            var darkModeSubject = new DarkModeSubject(OnDarkModeChange);
            return IsDarkModeObservable.Subscribe(darkModeSubject);
        }

        public class DarkModeObservable : IObservable<bool>
        {
            public bool IsDarkMode { get; private set; }
            private List<IObserver<bool>> Observers;
            public DarkModeObservable()
            {
                Observers = new List<IObserver<bool>>();
            }

            /// <summary>
            /// Subscribe to scoped singleton DarkMode observable
            /// </summary>
            /// <param name="observer"></param>
            /// <returns> Unsubscribe to darkmode object</returns>
            public IDisposable Subscribe(IObserver<bool> observer)
            {
                if (!Observers.Contains(observer))
                    Observers.Add(observer);

                return new Unsubscriber(Observers, observer);
            }

            public void UpdateDarkMode(bool isDarkMode)
            {
                IsDarkMode = isDarkMode;
                foreach (var observer in Observers.ToArray())
                {
                    observer.OnNext(isDarkMode);
                }
            }

            /// <summary>
            /// Unsubscribe object for DarkMode
            /// </summary>
            private class Unsubscriber : IDisposable
            {
                private List<IObserver<bool>> Observers;
                private IObserver<bool> Observer;

                public Unsubscriber(List<IObserver<bool>> observers, IObserver<bool> observer)
                {
                    Observers = observers;
                    Observer = observer;
                }

                public void Dispose()
                {
                    if (!(Observer == null)) Observers.Remove(Observer);
                }
            }
        }

        /// <summary>
        /// Subscription to Darkmode
        /// </summary>
        private class DarkModeSubject : IObserver<bool>
        {
            private Action<bool> _action;

            public DarkModeSubject(Action<bool> action)
            {
                _action = action;
            }
            public void OnCompleted()
            {
                throw new NotImplementedException();
            }

            public void OnError(Exception error)
            {
                throw new NotImplementedException();
            }

            public void OnNext(bool value)
            {
                _action(value);
            }
        }


    }
}
