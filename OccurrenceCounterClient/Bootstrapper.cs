using Microsoft.Practices.Unity;
using OccurrenceCounter;
using OccurrenceCounter.DataProvider;
using OccurrenceCounter.SignsRemover;
using OccurrenceCounterClient.ViewModels;
using OccurrenceCounterClient.Views;
using Prism.Events;
using Prism.Unity;
using System.Windows;

namespace OccurrenceCounterClient
{
    /// <summary>
    /// Bootstrapper class.
    /// </summary>
    class Bootstrapper : UnityBootstrapper
    {
        /// <summary>
        /// Creates main view.
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject CreateShell()
        {
            return this.Container.Resolve<MainView>();
        }

        /// <summary>
        /// Application initialization.
        /// </summary>
        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        /// <summary>
        /// Unity container configuration which contains registration of types use during
        /// dependency injection.
        /// </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<ISignsRemover<string>, PunctuationRemover>();

            Container.RegisterType<IDataProvider<string>, TextDataProvider>
                (new InjectionConstructor(new ResolvedParameter<PunctuationRemover>()));

            Container.RegisterType<IDataProvider<string>, TextFromFileDataProvider>(
                new InjectionConstructor(new ResolvedParameter<PunctuationRemover>()));

            Container.RegisterType<IOccurrenceCounter<string>, WordsOccurrenceCounter>();

            Container.RegisterType<FreeTextViewModel>(
                new InjectionConstructor(typeof(IEventAggregator),
                                         new ResolvedParameter<WordsOccurrenceCounter>(),
                                         new ResolvedParameter<TextDataProvider>()));

            Container.RegisterType<TextFromFileViewModel>(
                new InjectionConstructor(typeof(IEventAggregator), 
                                         new ResolvedParameter<WordsOccurrenceCounter>(), 
                                         new ResolvedParameter<TextFromFileDataProvider>()));

            Container.RegisterTypeForNavigation<FreeTextView>();
            Container.RegisterTypeForNavigation<TextFromFileView>();
        }
    }
}
