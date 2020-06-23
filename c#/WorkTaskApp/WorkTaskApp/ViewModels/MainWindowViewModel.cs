using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading;
using System.Windows.Navigation;
using WorkTaskApp.Models;

namespace WorkTaskApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        /// <summary>
        /// 登録用のインスタンス
        /// </summary>
        private RecordByDate recordByDate = new RecordByDate();

        /// <summary>
        /// 農薬マスタ読み込みコレクション
        /// </summary>
        private ObservableCollection<PesticideContent> pesticideContents;
        public ObservableCollection<PesticideContent> PesticideContents
        {
            get { return pesticideContents; }
            set { SetProperty(ref pesticideContents, value); }
        }

        /// <summary>
        /// 日付、天気インスタンス
        /// </summary>
        public DateWeather DateWeather { get; set; }

        /// <summary>
        /// 作業インスタンス
        /// </summary>
        public WorkContent WorkContent { get; set; }

        /// <summary>
        /// 農薬インスタンス
        /// </summary>
        private PesticideContent pesticideContent;
        public PesticideContent PesticideContent
        {
            get { return pesticideContent; }
            set { SetProperty(ref pesticideContent, value); }
        }

        /// <summary>
        /// 農薬登録クリックイベント
        /// </summary>
        public DelegateCommand PesticideClicked { get; private set; }

        /// <summary>
        /// 作業内容クリックイベント
        /// </summary>
        public DelegateCommand WorkContentClicked { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
        {
            // テスト
            PesticideContents = new ObservableCollection<PesticideContent>
            {
                new PesticideContent(){PestcideName = "A", Used = 100, Unit = "ℓ"},
                new PesticideContent(){PestcideName = "B", Used = 150, Unit = "g"},
                new PesticideContent(){PestcideName = "C", Used = 10, Unit = "kg"},
            };

            DateWeather = new DateWeather();
            WorkContent = new WorkContent();
            PesticideContent = new PesticideContent();

            // 農薬登録コマンド登録
            PesticideClicked = new DelegateCommand(
                () => AddPesticide());

            // 作業登録コマンド登録
            WorkContentClicked = new DelegateCommand(
                () => AddWorkContent());
        }

        /// <summary>
        /// 農薬登録コールバック
        /// </summary>
        public void AddPesticide()
        {
            WorkContent.PesticideContents.Add(new PesticideContent(PesticideContent));
        }

        /// <summary>
        /// 作業登録コールバック
        /// </summary>
        public void AddWorkContent()
        {
            recordByDate.WorkContents.Add(new WorkContent(WorkContent));
        }
    }
}
