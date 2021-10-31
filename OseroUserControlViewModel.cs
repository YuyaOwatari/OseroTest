namespace OseroTest
{
    using System;
    using System.ComponentModel;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;

    public class OseroUserControlViewModel : INotifyPropertyChanged
    {
        private string message;

        public OseroUserControlViewModel()
        {

        }

        public string Message
        {
            get
            {
                return this.message;
            }

            set
            {
                this.message = value;
                this.NotifyPropertyChanged("Message");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Greetメソッドが終了したときに発生するイベント
        /// </summary>
        public event EventHandler GreetFinished;

        /// <summary>
        /// 今回のメインの処理
        /// </summary>
        public void Greet()
        {
            this.Message = "Hello world";
            // 処理が終わったことをイベントで通知しておく
            OnGreetFinished();
        }

        protected virtual void OnGreetFinished()
        {
            var h = GreetFinished;
            if (h != null)
            {
                h(this, EventArgs.Empty);
            }
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}