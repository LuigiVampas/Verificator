using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Verificator.App
{
    public static class WpfSingleInstance
    {
        /// <summary>
        /// Запускает приложение в единственном экземпляре.
        /// </summary>
        internal static void Make()
        {
            Make(SingleInstanceModes.ForEveryUser);
        }

        /// <summary>
        /// Запускает приложение в единственном экземпляре.
        /// </summary>
        /// <param name="singleInstanceModes">Режим запуска.</param>
        internal static void Make(SingleInstanceModes singleInstanceModes)
        {
            var appName = Application.Current.GetType().Assembly.ManifestModule.ScopeName;

            var windowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();

            if (windowsIdentity == null) 
                throw new NullReferenceException("Не удалось получить данные о пользователе Windows.");

            var keyUserName = windowsIdentity.User != null ? windowsIdentity.User.ToString() : string.Empty;

            var eventWaitHandleName = string.Format(
                "{0}{1}",
                appName,
                singleInstanceModes == SingleInstanceModes.ForEveryUser ? keyUserName : string.Empty
                );

            if (!TryActivateApplication(eventWaitHandleName))
                OpenApplication(eventWaitHandleName);
        }

        /// <summary>
        /// Пытается активировать запущенное приложение.
        /// </summary>
        /// <param name="eventWaitHandleName">Имя события, активизирующего приложение.</param>
        /// <returns>true - если приложение активировалось, false - в противном случае.</returns>
        private static bool TryActivateApplication(string eventWaitHandleName)
        {
            try
            {
                using (var eventWaitHandle = EventWaitHandle.OpenExisting(eventWaitHandleName))
                {
                    // It informs first instance about other startup attempting.
                    eventWaitHandle.Set();
                }

                // Let's terminate this posterior startup.
                // For that exit no interceptions.
                Environment.Exit(0);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Запускает новое приложение.
        /// </summary>
        /// <param name="eventWaitHandleName">Имя события, активизирующего приложение.</param>
        private static void OpenApplication(string eventWaitHandleName)
        {
            using (var eventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset, eventWaitHandleName))
            {
                ThreadPool.RegisterWaitForSingleObject(eventWaitHandle, OtherInstanceAttemptedToStart, null, Timeout.Infinite, false);
            }

            RemoveApplicationsStartupDeadlockForStartupCrushedWindows();
        }

        /// <summary>
        /// Обработчик события запуска второй копии приложения.
        /// </summary>
        /// <param name="state">Текущее состояние.</param>
        /// <param name="timedOut">Просрочено ли ожидание.</param>
        private static void OtherInstanceAttemptedToStart(object state, bool timedOut)
        {
            RemoveApplicationsStartupDeadlockForStartupCrushedWindows();
            Application.Current.Dispatcher.BeginInvoke(new Action(() => { try { Application.Current.MainWindow.Activate(); } catch { } }));
        }

        internal static DispatcherTimer AutoExitAplicationIfStartupDeadlock;

        /// <summary>
        /// Бывают случаи, когда при старте произошла ошибка и ни одно окно не появилось.
        /// При этом второй инстанс приложения уже не запустить, а этот не закрыть, кроме как через панель задач. Deedlock своего рода получился.
        /// </summary>
        public static void RemoveApplicationsStartupDeadlockForStartupCrushedWindows()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    AutoExitAplicationIfStartupDeadlock =
                        new DispatcherTimer(
                            TimeSpan.FromSeconds(6),
                            DispatcherPriority.ApplicationIdle,
                            (o, args) =>
                            {
                                if (Application.Current.Windows.Cast<Window>().Count(window => !Double.IsNaN(window.Left)) == 0)
                                {
                                    // For that exit no interceptions.
                                    Environment.Exit(0);
                                }
                            },
                            Application.Current.Dispatcher
                        );
                }),
                DispatcherPriority.ApplicationIdle
                );
        }
    }

    /// <summary>
    /// Режимы запуска приложения.
    /// </summary>
    public enum SingleInstanceModes
    {
        /// <summary>
        /// Do nothing.
        /// </summary>
        NotInited = 0,

        /// <summary>
        /// Every user can have own single instance.
        /// </summary>
        ForEveryUser,
    }
}
