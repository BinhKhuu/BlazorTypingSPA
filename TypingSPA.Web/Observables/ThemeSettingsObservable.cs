using TypingSPA.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TypingSPA.Web.Observables
{
    public class ThemeSettingsObservable : IObservable<ThemeSettings>
    {
        public ThemeSettings Settings { get; set; }
        private List<IObserver<ThemeSettings>> Observers { get; set; }
        public ThemeSettingsObservable() {
            Settings = new ThemeSettings();
            Observers = new List<IObserver<ThemeSettings>>();
        }

        public IDisposable Subscribe(IObserver<ThemeSettings> observer)
        {
            if (!Observers.Contains(observer)) Observers.Add(observer);

            return new Unsubscriber(Observers, observer);
        }

        public void UpdateThemeSettings(ThemeSettings themeSettings)
        {
            Settings = themeSettings;
            foreach (var observer in Observers.ToArray())
            {
                observer.OnNext(themeSettings);
            }
        }
        private class Unsubscriber : IDisposable
        {
            private List<IObserver<ThemeSettings>> Observers;
            private IObserver<ThemeSettings> Observer;

            public Unsubscriber(List<IObserver<ThemeSettings>> observers, IObserver<ThemeSettings> observer) {
                Observers = observers;
                Observer = observer;
            }
            public void Dispose()
            {
                if (!(Observer == null)) Observers.Remove(Observer);
            }
        }

        public class ThemeSettingSubject : IObserver<ThemeSettings>
        {
            private Action<ThemeSettings> Action;

            public ThemeSettingSubject(Action<ThemeSettings> action)
            {
                Action = action;
            }
            public void OnCompleted()
            {
                throw new NotImplementedException();
            }

            public void OnError(Exception error)
            {
                throw new NotImplementedException();
            }

            public void OnNext(ThemeSettings value)
            {
                Action(value);
            }
        }
    }
}
