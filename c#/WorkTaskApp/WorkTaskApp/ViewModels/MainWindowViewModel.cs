using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using WorkTaskApp.Models;

namespace WorkTaskApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        /// <summary>
        /// クリック時に選択された農薬内容のインデックス（永続用）
        /// </summary>
        private int selectedPesticideContentIndex = 0; 

        /// <summary>
        /// クリック時に選択された農薬内容のインデックス
        /// </summary>
        public int PesticideContentIndex { get; set; } = 0;

        /// <summary>
        /// 選択された農薬マスタのインデックス
        /// </summary>
        private int pesticideMasterIndex = 0;
        public int PesticideMasterIndex
        {
            get { return pesticideMasterIndex; }
            set { SetProperty(ref pesticideMasterIndex, value); }
        }

        /// <summary>
        /// 選択された農薬マスタのインデックス
        /// </summary>
        private int workerMasterIndex = 0;
        public int WorkerMasterIndex
        {
            get { return workerMasterIndex; }
            set { SetProperty(ref workerMasterIndex, value); }
        }

        /// <summary>
        /// 作業内容リスト
        /// </summary>
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
        /// 作業内容インスタンス
        /// </summary>
        private WorkContent workContent;
        public WorkContent WorkContent
        {
            get { return workContent; }
            set { SetProperty(ref workContent, value); }
        }

        /// <summary>
        /// 農薬内容インスタンス
        /// </summary>
        private PesticideContent pesticideContent;
        public PesticideContent PesticideContent
        {
            get { return pesticideContent; }
            set { SetProperty(ref pesticideContent, value); }
        }

        /// <summary>
        /// 農薬内容登録クリックイベント
        /// </summary>
        public DelegateCommand RegisterPesticideContentClicked { get; private set; }
        /// <summary>
        /// 農薬内容更新クリックイベント
        /// </summary>
        public DelegateCommand UpdatePesticideContentClicked { get; private set; }
        /// <summary>
        /// 農薬内容削除クリックイベント
        /// </summary>
        public DelegateCommand DeletePesticideContentClicked { get; private set; }
        /// <summary>
        /// 農薬内容リストダブルクリックイベント
        /// </summary>
        public DelegateCommand<Object> PesticideContentsClicked { get; private set; }

        /// <summary>
        /// 作業内容登録クリックイベント
        /// </summary>
        public DelegateCommand RegisterWorkContentClicked { get; private set; }
        /// <summary>
        /// 作業内容更新クリックイベント
        /// </summary>
        public DelegateCommand UpdateWorkContentClicked { get; private set; }
        /// <summary>
        /// 作業内容削除クリックイベント
        /// </summary>
        public DelegateCommand DeleteWorkContentClicked { get; private set; }
        /// <summary>
        /// 作業内容リストダブルクリックイベント
        /// </summary>
        public DelegateCommand<Object> WorkContentsClicked { get; private set; }

        /// <summary>
        /// 農薬マスタ登録クリックイベント
        /// </summary>
        public DelegateCommand RegisterPesticideClicked { get; private set; }
        /// <summary>
        /// 農薬マスタ更新クリックイベント
        /// </summary>
        public DelegateCommand UpdatePesticideClicked { get; private set; }
        /// <summary>
        /// 農薬マスタ削除クリックイベント
        /// </summary>
        public DelegateCommand DeletePesticideClicked { get; private set; }
        /// <summary>
        /// 農薬マスタリストダブルクリックイベント
        /// </summary>
        public DelegateCommand<Object> PesticideMasterClicked { get; private set; }

        /// <summary>
        /// 作業者マスタ登録クリックイベント
        /// </summary>
        public DelegateCommand RegisterWorkerClicked { get; private set; }
        /// <summary>
        /// 作業者マスタ更新クリックイベント
        /// </summary>
        public DelegateCommand UpdateWorkerClicked { get; private set; }
        /// <summary>
        /// 作業者マスタ削除クリックイベント
        /// </summary>
        public DelegateCommand DeleteWorkerClicked { get; private set; }
        /// <summary>
        /// 作業者マスタリストダブルクリックイベント
        /// </summary>
        public DelegateCommand<Object> WorkerMasterClicked { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
        {
            DataBaseManager.ConnectDB("test.db");
            WorkContents = new ObservableCollection<WorkContent>(DataBaseManager.DBManager.GetWorkContents());
            PesticideMasters = new ObservableCollection<PesticideMaster>(DataBaseManager.DBManager.GetPesticideMasters());
            WorkerMasters = new ObservableCollection<WorkerMaster>(DataBaseManager.DBManager.GetWorkerMasters());

            WorkContent = new WorkContent();
            PesticideContent = new PesticideContent();
            WorkerMaster = new WorkerMaster();
            RegisterPesticide = new PesticideMaster();
            RegisterWorker = new WorkerMaster();

            // 農薬内容登録コマンド登録
            RegisterPesticideContentClicked = new DelegateCommand(
                () => RegisterPesticideContent());
            // 農薬内容更新コマンド登録
            UpdatePesticideContentClicked = new DelegateCommand(
                () => UpdatePesticideContent());
            // 農薬内容削除コマンド登録
            DeletePesticideContentClicked = new DelegateCommand(
                () => DeletePesticideContent());
            // 農薬内容読み込みコマンド登録(クリックで取得したインスタンスで登録用インスタンスを上書き)
            PesticideContentsClicked = new DelegateCommand<Object>(
                (readPesticideContent) =>
                {
                    if (readPesticideContent is PesticideContent)
                    {
                        // インデックスを取得
                        selectedPesticideContentIndex = PesticideContentIndex;
                        // リスト内の要素が選択されている場合のみ、インスタンスを上書きする
                        PesticideContent = new PesticideContent((PesticideContent)readPesticideContent);
                        PesticideMasterIndex = PesticideMasters.ToList().FindIndex(master => PesticideContent.PestcideMaster.Id == master.Id);
                    }
                });

            // 作業内容登録コマンド登録
            RegisterWorkContentClicked = new DelegateCommand(
                () => RegisterWorkContent());
            // 作業内容更新コマンド登録
            UpdateWorkContentClicked = new DelegateCommand(
                () => UpdateWorkContent());
            // 作業内容削除コマンド登録
            DeleteWorkContentClicked = new DelegateCommand(
                () => DeleteWorkContent());
            // 作業内容読み込みコマンド登録(クリックで取得したインスタンスで登録用インスタンスを上書き)
            WorkContentsClicked = new DelegateCommand<Object>(
                (readWorkContent) =>
                {
                    if (readWorkContent is WorkContent)
                    {
                        // リスト内の要素が選択されている場合のみ、インスタンスを上書きする
                        WorkContent = new WorkContent((WorkContent)readWorkContent);
                        WorkerMasterIndex = WorkerMasters.ToList().FindIndex(master => WorkContent.UserId == master.Id);
                    }
                });

            // 農薬マスタ登録コマンド登録
            RegisterPesticideClicked = new DelegateCommand(
                () => RegisterPesticideMaster());

            // 農薬マスタ更新コマンド登録
            UpdatePesticideClicked = new DelegateCommand(
                () => UpdatePesticideMaster());

            // 農薬マスタ削除コマンド登録
            DeletePesticideClicked = new DelegateCommand(
                () => DeletePesticideMaster());

            // 農薬マスタ読み込みコマンド登録(クリックで取得したインスタンスで登録用インスタンスを上書き)
            PesticideMasterClicked = new DelegateCommand<Object>(
                (readPesticide) =>
                {
                    if (readPesticide is PesticideMaster)
                    {
                        // リスト内の要素が選択されている場合のみ、インスタンスを上書きする
                        RegisterPesticide = new PesticideMaster((PesticideMaster)readPesticide);
                    }
                });

            // 作業者マスタ登録コマンド登録
            RegisterWorkerClicked = new DelegateCommand(
                () => RegisterWorkerMaster());

            // 作業者マスタ更新コマンド登録
            UpdateWorkerClicked = new DelegateCommand(
                () => UpdateWorkerMaster());

            // 作業者マスタ削除コマンド登録
            DeleteWorkerClicked = new DelegateCommand(
                () => DeleteWorkerMaster());

            // 作業者マスタ読み込みコマンド登録(クリックで取得したインスタンスで登録用インスタンスを上書き)
            WorkerMasterClicked = new DelegateCommand<Object>(
                (readWorker) =>
                {
                    if (readWorker is WorkerMaster)
                    {
                        // リスト内の要素が選択されている場合のみ、インスタンスを上書きする
                        RegisterWorker = new WorkerMaster((WorkerMaster)readWorker);
                    }
                });
        }

        /// <summary>
        /// 農薬内容登録コールバック
        /// </summary>
        private void RegisterPesticideContent()
        {
            PesticideContent.PestcideMaster = new PesticideMaster(PesticideMaster);
            PesticideContent.PesticideId = PesticideContent.PestcideMaster.Id;
            // 既存データの有無により対応を変更
            if(0 == WorkContent.Id)
            {
                WorkContent.PesticideContents.Add(new PesticideContent(PesticideContent));
            }
            else
            {
                PesticideContent.WorkContentId = WorkContent.Id;
                if(false == PesticideContent.RegisterDbRecord())
                {
                    MessageBox.Show("入力情報に不備があります");
                    return;
                }
                WorkContent.PesticideContents = new ObservableCollection<PesticideContent>(DataBaseManager.DBManager.GetPesticideContents(WorkContent.Id));
            }
            PesticideContent = new PesticideContent();
        }

        /// <summary>
        /// 農薬内容更新コールバック
        /// </summary>
        private void UpdatePesticideContent()
        {
            PesticideContent.PestcideMaster = new PesticideMaster(PesticideMaster);
            PesticideContent.PesticideId = PesticideContent.PestcideMaster.Id;

            if(0 == WorkContent.Id)
            {
                WorkContent.PesticideContents[selectedPesticideContentIndex] = new PesticideContent(PesticideContent);
            }
            else
            {
                if(false == PesticideContent.UpdateDbRecord())
                {
                    MessageBox.Show("入力情報に不備があります");
                    return;
                }
                WorkContent.PesticideContents = new ObservableCollection<PesticideContent>(DataBaseManager.DBManager.GetPesticideContents(WorkContent.Id));
            }
            PesticideContent = new PesticideContent();
        }

        /// <summary>
        /// 農薬内容削除コールバック
        /// </summary>
        private void DeletePesticideContent()
        {
            if(0 == WorkContent.Id)
            {
                WorkContent.PesticideContents.RemoveAt(selectedPesticideContentIndex);
            }
            else
            {
                if(false == PesticideContent.DeleteDbRecord())
                {
                    MessageBox.Show("IDに不備があります");
                    return;
                }
                WorkContent.PesticideContents = new ObservableCollection<PesticideContent>(DataBaseManager.DBManager.GetPesticideContents(WorkContent.Id));
            }
            PesticideContent = new PesticideContent();
        }

        /// <summary>
        /// 作業内容登録コールバック
        /// </summary>
        private void RegisterWorkContent()
        {
            // 作業内容の登録
            WorkContent.UserId = WorkerMaster.Id;
            // 選択されている日時を取得
            if (false == WorkContent.RegisterDbRecord())
            {
                MessageBox.Show("入力情報に不備があります");
                return;
            }

            int workContentId = DataBaseManager.DBManager.GetWorkCotentLastId();
            // 農薬内容の登録
            foreach (PesticideContent pc in WorkContent.PesticideContents)
            {
                pc.WorkContentId = workContentId;
                pc.RegisterDbRecord();
            }

            // 直前の登録時間を開始・終了に設定する
            WorkContent tmpWc = new WorkContent();
            tmpWc.SetDateTime(WorkContent.EndWorkTime);
            WorkContent = new WorkContent(tmpWc);

            WorkContents = new ObservableCollection<WorkContent>(DataBaseManager.DBManager.GetWorkContents());
            foreach (WorkContent wc in WorkContents)
            {
                wc.PesticideContents = new ObservableCollection<PesticideContent>(DataBaseManager.DBManager.GetPesticideContents(wc.Id));
            }
        }

        /// <summary>
        /// 作業内容更新コールバック
        /// </summary>
        private void UpdateWorkContent()
        {
            if (false == WorkContent.UpdateDbRecord())
            {
                MessageBox.Show("入力情報に不備があります");
                return;
            }
            WorkContents = new ObservableCollection<WorkContent>(DataBaseManager.DBManager.GetWorkContents());
            WorkContent = new WorkContent();
        }

        /// <summary>
        /// 作業内容削除コールバック
        /// </summary>
        private void DeleteWorkContent()
        {
            if (false == WorkContent.DeleteDbRecord())
            {
                MessageBox.Show("IDに不備があります");
                return;
            }
            WorkContents = new ObservableCollection<WorkContent>(DataBaseManager.DBManager.GetWorkContents());
            WorkContent = new WorkContent();
        }

        /// <summary>
        /// 農薬マスタ登録コールバック
        /// </summary>
        private void RegisterPesticideMaster()
        {
            if (false == RegisterPesticide.RegisterDbRecord())
            {
                MessageBox.Show("入力情報に不備があります");
                return;
            }
            PesticideMasters = new ObservableCollection<PesticideMaster>(DataBaseManager.DBManager.GetPesticideMasters());
            RegisterPesticide = new PesticideMaster();
        }

        /// <summary>
        /// 農薬マスタ更新コールバック
        /// </summary>
        private void UpdatePesticideMaster()
        {
            if (false == RegisterPesticide.UpdateDbRecord())
            {
                MessageBox.Show("入力情報に不備があります");
                return;
            }
            PesticideMasters = new ObservableCollection<PesticideMaster>(DataBaseManager.DBManager.GetPesticideMasters());
            RegisterPesticide = new PesticideMaster();
        }

        /// <summary>
        /// 農薬マスタ削除コールバック
        /// </summary>
        private void DeletePesticideMaster()
        {
            if (false == RegisterPesticide.DeleteDbRecord())
            {
                MessageBox.Show("IDに不備があります");
                return;
            }
            PesticideMasters = new ObservableCollection<PesticideMaster>(DataBaseManager.DBManager.GetPesticideMasters());
            RegisterPesticide = new PesticideMaster();
        }

        /// <summary>
        /// 作業者マスタ登録コールバック
        /// </summary>
        private void RegisterWorkerMaster()
        {
            if (false == RegisterWorker.RegisterDbRecord())
            {
                MessageBox.Show("入力情報に不備があります");
                return;
            }
            WorkerMasters = new ObservableCollection<WorkerMaster>(DataBaseManager.DBManager.GetWorkerMasters());
            RegisterWorker = new WorkerMaster();
        }

        /// <summary>
        /// 作業者マスタ更新コールバック
        /// </summary>
        private void UpdateWorkerMaster()
        {
            if (false == RegisterWorker.UpdateDbRecord())
            {
                MessageBox.Show("入力情報に不備があります");
                return;
            }
            WorkerMasters = new ObservableCollection<WorkerMaster>(DataBaseManager.DBManager.GetWorkerMasters());
            RegisterWorker = new WorkerMaster();
        }

        /// <summary>
        /// 作業者マスタ削除コールバック
        /// </summary>
        private void DeleteWorkerMaster()
        {
            if (false == RegisterWorker.DeleteDbRecord())
            {
                MessageBox.Show("IDに不備があります");
                return;
            }
            WorkerMasters = new ObservableCollection<WorkerMaster>(DataBaseManager.DBManager.GetWorkerMasters());
            RegisterWorker = new WorkerMaster();
        }
    }
}
