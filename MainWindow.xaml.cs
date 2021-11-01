using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.Windows.Media.Animation;
using System.Threading;
using System.Windows.Threading;

namespace OseroTest
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public int[,] arrayBoard = null;
        public int[,] beforeBoard = null;
        public int TurnStatus = 0;//手番ステータス
        public int whiteCount = 0;//白カウント
        public int BlackCount = 0;//黒カウント
        public int ableCount = 0;//置ける数カウント
        public MainWindow()
        {
            InitializeComponent();

            // 枠線の設定
            for(int row = 0 ; row < 8 ; row++){
                for(int col = 0 ; col < 8 ; col++){
                    Border border = new Border();
                    border.BorderBrush = new SolidColorBrush(Colors.Black);
                    border.BorderThickness = new System.Windows.Thickness(1);
                    border.SetValue(Grid.RowProperty, row);
                    border.SetValue(Grid.ColumnProperty, col);
                    GridBoard.Children.Add(border);
                }
            }

            // メッセージ表示
            MessageLabel.Content = "白の手番です";
            TurnStatus = -1;//白
            MessageLabel.FontSize = 20;

            // 盤面状態の配列を初期化
            arrayBoard = new int[8, 8] {
                 { 0, 0 ,0 ,0 ,0 ,0 ,0 ,0},
                 { 0, 0 ,0 ,0 ,0 ,0 ,0 ,0},
                 { 0, 0 ,0 ,0 ,0 ,0 ,0 ,0},
                 { 0, 0 ,0 ,0 ,0 ,0 ,0 ,0},
                 { 0, 0 ,0 ,0 ,0 ,0 ,0 ,0},
                 { 0, 0 ,0 ,0 ,0 ,0 ,0 ,0},
                 { 0, 0 ,0 ,0 ,0 ,0 ,0 ,0},
                 { 0, 0 ,0 ,0 ,0 ,0 ,0 ,0},
                 };

            // 初期配置セット
            arrayBoard[3,3] = (int)OseroKind.Black ; 
            arrayBoard[3,4] = (int)OseroKind.White ; 
            arrayBoard[4,3] = (int)OseroKind.White ; 
            arrayBoard[4,4] = (int)OseroKind.Black ; 

            drawBoard();

        }

        /// <summary>
        /// 盤面描画
        /// </summary>
        /// <returns>なし</returns>    }
        private void drawBoard()
        {
            // Create a name scope for the page.
            NameScope.SetNameScope(this, new NameScope());
            ClearGrid();
            ableCount = 0;
            whiteCount = 0;
            BlackCount = 0;
            for(int row = 0 ; row < 8 ; row++){
                for(int col = 0 ; col < 8 ; col++){

                    // 枠線の設定
                    Border border = new Border();
                    border.BorderBrush = new SolidColorBrush(Colors.Black);
                    border.BorderThickness = new System.Windows.Thickness(1);
                    border.SetValue(Grid.RowProperty, row);
                    border.SetValue(Grid.ColumnProperty, col);
                    GridBoard.Children.Add(border);

                    int kind = arrayBoard[row,col];

                    String imgFileName = "";
                    // Gridの要素削除
                    GridBoard.Children.Remove(this);    
                    Boolean ableFlg = false;

                    // 駒配置
                    if(kind == (int)OseroKind.Black ){
                        // Console.WriteLine(arrayBoard[row,col] + ": Black");
                        imgFileName = "osero-illust1.png";
                        BlackCount ++;
                    }else if(kind == (int)OseroKind.White ){
                        // Console.WriteLine(arrayBoard[row,col] + ": White");
                        imgFileName = "osero-illust2.png";
                        whiteCount ++;
                    }else{
                        // 手番プレイヤーが配置可能かどうかを判定
                        if(Check_OseroSet(col,row,TurnStatus,0)){
                            ableFlg = true;
                            ableCount ++;
                            if(TurnStatus == (int)OseroKind.Black){
                                imgFileName = "osero-illust1.png";
                            }else if(TurnStatus == (int)OseroKind.White){
                                imgFileName = "osero-illust2.png";
                            }
                        }else{
                            continue;
                        }

                    }

                    Image img = new Image();
                    BitmapImage imgSrc = new BitmapImage();
                    imgSrc.BeginInit();
                    imgSrc.UriSource = new Uri(imgFileName,UriKind.Relative);
                    imgSrc.CacheOption = BitmapCacheOption.OnLoad;
                    imgSrc.EndInit();

                    img.Source = imgSrc;
                    img.Name="img"+ row + col;
                    img.Height=Constants.OSERO_SIZE_HEIGHT;
                    img.Width=Constants.OSERO_SIZE_HEIGHT;

                    img.SetValue(Grid.RowProperty, row);
                    img.SetValue(Grid.ColumnProperty, col);

                    this.RegisterName(img.Name, img);
                    if(ableFlg){
                        // 配置可能の場合は、透過した画像を設定
                        img.Opacity = 0.2;
                    }

                    DoubleAnimation myDoubleAnimation = new DoubleAnimation();
                    myDoubleAnimation.From = 0;
                    myDoubleAnimation.To = 200;
                    myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(2));

                    Storyboard.SetTargetName(myDoubleAnimation, img.Name);
                    Storyboard.SetTargetProperty(myDoubleAnimation,
                    new PropertyPath(Rectangle.WidthProperty));

                    Storyboard myStoryboard = new Storyboard();
                    myStoryboard.Children.Add(myDoubleAnimation);

                    BeginStoryboard myBeginStoryboard = new BeginStoryboard();
                    myBeginStoryboard.Storyboard = myStoryboard;

                    EventTrigger myEnterTrigger = new EventTrigger();
                    myEnterTrigger.Actions.Add(myBeginStoryboard);

                   GridBoard.Children.Add(img);

                }   
            }
            
        }

        /// <summary>
        /// 駒を置く処理
        /// </summary>
        private void BoardSet(int col,int row)
        {
            if(!Check_OseroSet(col,row,TurnStatus,1)){
                // チェック結果false
                //MessageLabel2.Content = "そこはおけません";
                return;
            }
            arrayBoard[row,col] = TurnStatus ; 

            // 駒を置いたら手番を入れ替える
            TurnStatus = TurnStatus * -1;

            drawBoard();

            if(whiteCount + BlackCount == 64){
                // ゲーム終了
                String result = string.Format("白:{0} 黒:{1} ", whiteCount , BlackCount) ;
                if(whiteCount < BlackCount){
                    
                    MessageLabel.Content = result +  "★★黒の勝ちです★★";
                }else if(whiteCount > BlackCount){
                    MessageLabel.Content = result  +"☆☆白の勝ちです☆☆";
                }else{
                    MessageLabel.Content = "！！引き分けです！！";
                }
                return;
            }
            if (ableCount == 0){
                // 置ける数が0なら、手番を再度入れ替え
                MessageBox.Show("置ける場所がないため、パスします");
                TurnStatus = TurnStatus * -1;
                drawBoard();

            } 

            if(TurnStatus == -1){
                MessageLabel.Content = "白の手番です";

            }else{
                MessageLabel.Content = "黒の手番です";
                DoEvents();

                Thread.Sleep(1000);                
                EnemyPlay();

            }

        }

        /// <summary>
        /// 画面再描画処理１
        /// </summary>
        private void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            var callback = new DispatcherOperationCallback(ExitFrames);
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, callback, frame);
            Dispatcher.PushFrame(frame);
        }

        /// <summary>
        /// 画面再描画処理２
        /// </summary>
        private object ExitFrames(object obj)
        {
            ((DispatcherFrame)obj).Continue = false;
            return null;
        }

        /// <summary>
        /// Grid上でのマウスのボタン押下イベント(タッチ操作含む)
        /// </summary>
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point pos = e.GetPosition(GridBoard);
            int col = (int)Math.Floor(pos.X / 50);
            int row =(int)Math.Floor(pos.Y / 50);

            BoardSet(col,row);
        }

        /// <summary>
        /// オセロ配置判定処理
        /// </summary>
        /// <param name="col">対象カラム</param>
        /// <param name="rows">対象行</param>
        /// <param name="kind">駒種別</param>
        /// <param name="chkFlg">チェックフラグ（0:チェックのみ、1:チェック+駒返し）</param>
        private Boolean Check_OseroSet(int col,int row, int kind, int chkFlg )
        {

            if(arrayBoard[row ,col] != (int)OseroKind.None){
                Console.WriteLine("駒がすでに存在");
                return false;
                // 駒がおいてたらfalse
            }

            int nowTurn = kind;//今の手番
            int target = 0;
            Boolean chkResult = false;

            // 8方向について、それぞれ配置可能か判定する
            for(int i = 0; i < 8 ; i++){
                // 最大値を決め、その数まで、特定方向に対してチェックする
                int iMax = 0;
                int rowAdd = 0;
                int colAdd = 0;
                switch(i){
                    case 0:  //下　方向
                        iMax = row;
                        rowAdd = 1;
                        colAdd = 0;
                        break;
                    case 1:  //右下方向
                        iMax = (row > col) ? row : col;
                        rowAdd = 1;
                        colAdd = 1;
                        break;
                    case 2:  //右　方向
                        iMax = col;
                        rowAdd = 0;
                        colAdd = 1;
                        break;
                    case 3:  //右上方向
                        iMax = ( (7 - row) > col) ? (7 - row) : col;
                        rowAdd = -1;
                        colAdd = 1;
                        break;
                    case 4:  //上　方向
                        iMax = 7 - row;
                        rowAdd = -1;
                        colAdd = 0;
                        break;
                    case 5:  //左上方向
                        iMax = ( (7 - row) > (7 - col) ) ? (7 - row) : (7 - col);
                        rowAdd = -1;
                        colAdd = -1;
                        break;
                    case 6:  //左　方向
                        iMax = 7 - col;
                        rowAdd = 0;
                        colAdd = -1;
                        break;
                    case 7:  //左下方向
                        iMax = ( row > (7 - col) ) ? row : (7 - col);
                        rowAdd = 1;
                        colAdd = -1;
                        break;
                }
                Console.WriteLine("方向：" + i);
                ArrayList reverseList = new ArrayList();

                for(int j = 1 ; j + iMax < 8 ; j ++){
                    target = arrayBoard[row + (j * rowAdd) , col + (j * colAdd) ];
                    if(j == 1 && target*nowTurn != -1){
                        // 一つ目が相手の駒以外(自分の駒or無し)なら、チェック終了 
                        break;
                    }
                    // ひっくり返しリストに格納しておく
                    int[] stack = new int[2];                    
                    stack[0] = row + (j * rowAdd) ;
                    stack[1] = col + (j * colAdd)  ;
                    reverseList.Add(stack); 
                    if(target == 0){
                        //　2つ目以降、空があればそこで終了
                        break;
                    }
                    if(target == nowTurn ){
                        // 2つ目以降、自分の駒が見つかればtrue(空は除く)
                        chkResult =  true;
                        foreach(int[] stackArray in reverseList){
                            // リストのデータをひっくり返す
                            if(chkFlg == 1 ){
                                arrayBoard[stackArray[0],stackArray[1]] = nowTurn;
                            }
                        }
                    }
                }
            }

            Console.WriteLine("おけない場所");
            return chkResult;

        }

        /// <summary>
        /// グリッド削除
        /// </summary>
        private void ClearGrid()
        {
            for (int i = GridBoard.Children.Count - 1; i >= 0; i--)
            {
                GridBoard.Children.RemoveAt(i);
            }
        }


        /// <summary>
        /// 相手動作
        /// </summary>
        /// <returns>なし</returns>    }
        private void EnemyPlay()
        {
            for(int row = 0 ; row < 8 ; row++){
                for(int col = 0 ; col < 8 ; col++){
                    if(Check_OseroSet(col,row,TurnStatus,0)){
                        // 置くところが見つかったら、無条件で置く
                        BoardSet(col,row);
                        return;
                    }
                }   
            }
            
        }

    }


    /// <summary>
    /// enum OseroKind
    /// </summary>
    public enum OseroKind { White=-1, None=0, Black=1 };
}
