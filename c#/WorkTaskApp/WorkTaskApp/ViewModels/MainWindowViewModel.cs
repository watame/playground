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

        private ObservableCollection<WorkContent> workContents;
        public ObservableCollection<WorkContent> WorkContents
        {
            get { return workContents; }
            set { SetProperty(ref workContents, value); }
        }

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
        /// 農薬マスタ登録クリックイベント
        /// </summary>
        public DelegateCommand RegisterPesticideClicked { get; private set; }

        /// <summary>
        /// 作業者マスタ登録クリックイベント
        /// </summary>
        public DelegateCommand RegisterWorkerClicked { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
        {
            DataBaseManager.ConnectDB("test.db");
            WorkContents = new ObservableCollection<WorkContent>();
            PesticideMasters = new ObservableCollection<PesticideMaster>(DataBaseManager.DBManager.GetPesticideMaster());
            WorkerMasters = new ObservableCollection<WorkerMaster>(DataBaseManager.DBManager.GetWorkerMaster());

            DateWeather = new DateWeather();
            WorkContent = new WorkContent();
            PesticideContent = new PesticideContent();
            WorkerMaster = new WorkerMaster();
            RegisterPesticide = new PesticideMaster();
            RegisterWorker = new WorkerMaster();

            // 農薬登録コマンド登録
            PesticideClicked = new DelegateCommand(
                () => AddPesticide());

            // 作業登録コマンド登録
            WorkContentClicked = new DelegateCommand(
                () => AddWorkContent());

            // 農薬マスタ登録コマンド登録
            RegisterPesticideClicked = new DelegateCommand(
                () => RegisterPesticideMaster());

            // 作業者マスタ登録コマンド登録
            RegisterWorkerClicked = new DelegateCommand(
                () => RegisterWorkerMaster());
        }

        /// <summary>
        /// 農薬登録コールバック
        /// </summary>
        private void AddPesticide()
        {
            PesticideContent.PestcideMaster = new PesticideMaster(PesticideMaster);
            WorkContent.PesticideContents.Add(new PesticideContent(PesticideContent));
        }

        /// <summary>
        /// 作業登録コールバック
        /// </summary>
        private void AddWorkContent()
        {
            WorkContent.UserId = WorkerMaster.ID;
            WorkContents.Add(new WorkContent(WorkContent));
        }

        /// <summary>
        /// 農薬マスタ登録コールバック
        /// </summary>
        private void RegisterPesticideMaster()
        {
            DataBaseManager.DBManager.RegisterPesticideMaster(RegisterPesticide);
            PesticideMasters = new ObservableCollection<PesticideMaster>(DataBaseManager.DBManager.GetPesticideMaster());
        }

        /// <summary>
        /// 作業者マスタ登録コールバック
        /// </summary>
        private void RegisterWorkerMaster()
        {
            DataBaseManager.DBManager.RegisterWorkerMaster(RegisterWorker);
            WorkerMasters = new ObservableCollection<WorkerMaster>(DataBaseManager.DBManager.GetWorkerMaster());
        }
    }
}
