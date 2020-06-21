using Prism.Mvvm;
using System;
using System.Net;
using WorkTaskApp.Models;

namespace WorkTaskApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private DateWeather dateWeather;
        public DateWeather DateWeather
        {
            get { return dateWeather; }
            set { SetProperty(ref dateWeather, value); }
        }

        private WorkContent workContent;
        public WorkContent WorkContent
        {
            get { return workContent; }
            set { SetProperty(ref workContent, value); }
        }

        private PesticideContent pesticideContent;
        public PesticideContent PesticideContent
        {
            get { return pesticideContent; }
            set { SetProperty(ref pesticideContent, value); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
        {
            DateWeather = new DateWeather();
            WorkContent = new WorkContent();
            PesticideContent = new PesticideContent();
        }
    }
}
