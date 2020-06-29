using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
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
        private ObservableCollection<PesticideMaster> pesticideMasters;
        public ObservableCollection<PesticideMaster> PesticideMasters
        {
            get { return pesticideMasters; }
            set { SetProperty(ref pesticideMasters, value); }
        }

        private PesticideMaster pesticideMaster;
        public PesticideMaster PesticideMaster
        {
            get { return pesticideMaster; }
            set { SetProperty(ref pesticideMaster, value); }
        }

        private PesticideMaster registerPesticide;
        public PesticideMaster RegisterPesticide
        {
            get { return registerPesticide; }
            set { SetProperty(ref registerPesticide, value); }
        }

        private ObservableCollection<WorkerMaster> workerMasters;
        public ObservableCollection<WorkerMaster> WorkerMasters
        {
            get { return workerMasters; }
            set { SetProperty(ref workerMasters, value); }
        }

        private WorkerMaster workerMaster;
        public WorkerMaster WorkerMaster
        {
            get { return workerMaster; }
            set { SetProperty(ref workerMaster, value); }
        }

        private WorkerMaster registerWorker;
        public WorkerMaster RegisterWorker
        {
            get { return registerWorker; }
            set { SetProperty(ref registerWorker, value); }
        }

        /// <summary>
        /// 日付、天気インスタンス
        /// </summary>
        public DateWeather DateWeather { get; set; }

        /// <summary>
        /// 作業インスタンス
        /// </summary>
        public WorkContent WorkContent { get; set; }

        public PesticideContent PesticideContent { get; set; }

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
            DataBaseManager.ConnectDB("test.db");
            // テスト
            PesticideMasters = new ObservableCollection<PesticideMaster>
            {
                new PesticideMaster(){Name = "A", Unit = "ℓ", ID = 1},
                new PesticideMaster(){Name = "B", Unit = "g", ID = 2},
                new PesticideMaster(){Name = "C", Unit = "kg", ID = 3},
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
            PesticideContent.PestcideMaster = new PesticideMaster(PesticideMaster);
            WorkContent.PesticideContents.Add(new PesticideContent(PesticideContent));
        }

        /// <summary>
        /// 作業登録コールバック
        /// </summary>
        public void AddWorkContent()
        {
            recordByDate.WorkContents.Add(new WorkContent(WorkContent));
            List<DateWeather> tmp = DataBaseManager.DBManager.GetDateWeather();
        }
    }
}
