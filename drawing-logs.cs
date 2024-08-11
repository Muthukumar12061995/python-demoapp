using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Automator;

namespace Tanker_Pro
{
    public class DrawingLogControl : INotifyPropertyChanged
    {
        public DrawingLogControl()
        {
           // FillData();
        }

        private ICollectionView collView;

        private string search;
        public ObservableCollection<DrawingLog> DrawingLogs { get; set; }
        public ObservableCollection<DrawingLog> FilteredList { get; set; }
        public TankData TankData { get; set; }


        /// <summary>
        /// Global filter
        /// </summary>
        public string Search
        {
            get => search;
            set
            {
                search = value;

                collView.Filter = e =>
                {
                    var item = (DrawingLog)e;
                    return item != null && (item.DrawingNo?.StartsWith(search, StringComparison.OrdinalIgnoreCase) ?? false);
                };

                collView.Refresh();

                FilteredList = new ObservableCollection<DrawingLog>(collView.OfType<DrawingLog>().ToList());

                OnPropertyChanged("Search");
                OnPropertyChanged("FilteredList");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void FillData()
        {
            search = "";

            FilteredList = new ObservableCollection<DrawingLog>(DrawingLogs);
            collView = CollectionViewSource.GetDefaultView(FilteredList);

            OnPropertyChanged("Search");
            OnPropertyChanged("DrawingLogs");
            OnPropertyChanged("FilteredList");
        }

        private void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}