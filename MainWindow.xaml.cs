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

namespace OseroTest
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public int[,] arrayBoard = null;
        //手番
        public int TurnStatus = 0;
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
            MesssageLabel.Content = "白の手番です";
            TurnStatus = -1;//白
            MesssageLabel.FontSize = 20;

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
            arrayBoard[5,5] = (int)OseroKind.White ; 

            drawBoard();

            // 駒の初期配置
            // OseroSet(OseroKind.Black,3,3);
            // OseroSet(OseroKind.White,3,4);
            // OseroSet(OseroKind.White,4,3);
            // OseroSet(OseroKind.Black,4,4);


        }

        // /// <summary>
        // /// 駒配置処理
        // /// </summary>
        // /// <param name="Kind">オセロ種別</param>
        // /// <param name="rows">対象行</param>
        // /// <param name="columns">対象列</param>
        // /// <returns>なし</returns>    }
        // private void OseroSet(OseroKind kind, int rows, int columns )
        // {

        //     String imgFileName = "";
        //     // 駒配置
        //     if(kind == OseroKind.Black ){
        //         imgFileName = "osero-illust1.png";
        //     }else if(kind == OseroKind.White ){
        //         imgFileName = "osero-illust2.png";
        //     }else{
        //         Console.WriteLine("OseroSet Kind Error");
        //         return;
        //     }

        //     Image img = new Image();
        //     BitmapImage imgSrc = new BitmapImage();
        //     imgSrc.BeginInit();
        //     imgSrc.UriSource = new Uri(imgFileName,UriKind.Relative);
        //     imgSrc.CacheOption = BitmapCacheOption.OnLoad;
        //     imgSrc.EndInit();

        //     img.Source = imgSrc;
        //     img.Height=Constants.OSERO_SIZE_HEIGHT;
        //     img.Width=Constants.OSERO_SIZE_HEIGHT;

        //     img.SetValue(Grid.RowProperty, rows);
        //     img.SetValue(Grid.ColumnProperty, columns);

        //     GridBoard.Children.Add(img);

            
        // }

        /// <summary>
        /// 盤面描画
        /// </summary>
        /// <returns>なし</returns>    }
        private void drawBoard()
        {
            // Create a name scope for the page.
            NameScope.SetNameScope(this, new NameScope());
            // 枠線の設定
            for(int row = 0 ; row < 8 ; row++){
                for(int col = 0 ; col < 8 ; col++){
                    int kind = arrayBoard[row,col];

                    String imgFileName = "";
                    // Gridの要素削除
                    GridBoard.Children.Remove(this);    

                    // 駒配置
                    if(kind == (int)OseroKind.Black ){
                        // Console.WriteLine(arrayBoard[row,col] + ": Black");
                        imgFileName = "osero-illust1.png";
                    }else if(kind == (int)OseroKind.White ){
                        // Console.WriteLine(arrayBoard[row,col] + ": White");
                        imgFileName = "osero-illust2.png";
                    }else{
                        // Console.WriteLine(row + ","  + col + ": None");
                        continue;
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

                    // ColorAnimation myColorAnimation = new ColorAnimation();
                    // myColorAnimation.From = Colors.Blue;
                    // myColorAnimation.To = Colors.AliceBlue;
                    // myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
                    DoubleAnimation myDoubleAnimation = new DoubleAnimation();
                    myDoubleAnimation.From = 0;
                    myDoubleAnimation.To = 200;
                    myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(2));

                    Storyboard.SetTargetName(myDoubleAnimation, img.Name);
                    Storyboard.SetTargetProperty(myDoubleAnimation,
                    new PropertyPath(Rectangle.WidthProperty));

                        // DependencyProperty[] propertyChain =
                    //     new DependencyProperty[]
                    //         {Rectangle.FillProperty, SolidColorBrush.ColorProperty};
                    // string thePath = "(0).(1)";
                    // PropertyPath myPropertyPath = new PropertyPath(thePath, propertyChain);
                    // Storyboard.SetTargetProperty(myColorAnimation, myPropertyPath);

                    // Storyboardを取得
                    // Storyboard rotateStoryboard = (Storyboard)this.Resources["rotateAnimation"];
                    // rotateStoryboard.Children.
                    // Storyboard.SetTarget(rotateStoryboard.Children,img);

                    Storyboard myStoryboard = new Storyboard();
                    myStoryboard.Children.Add(myDoubleAnimation);

                    BeginStoryboard myBeginStoryboard = new BeginStoryboard();
                    myBeginStoryboard.Storyboard = myStoryboard;

                    EventTrigger myEnterTrigger = new EventTrigger();
                    myEnterTrigger.Actions.Add(myBeginStoryboard);

                    // myStoryboard.Begin();
                    // img.Loaded += delegate(object sender, MouseEventArgs e)
                    // {
                    //     myStoryboard.Begin(this);
                    // };                                        

                    // img.Loaded += 


                    OseroUserControl osero = new OseroUserControl();
                    img.SetValue(Grid.RowProperty, row);
                    img.SetValue(Grid.ColumnProperty, col);

                    GridBoard.Children.Add(osero);
//                    GridBoard.Children.Add(img);


                    // myStoryboard.Begin();
                }   
            }
            
        }

        /// <summary>
        /// Grid上でのマウスのボタン押下イベント(タッチ操作含む)
        /// </summary>
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point pos = e.GetPosition(GridBoard);
            int col = (int)Math.Floor(pos.X / 50);
            int row =(int)Math.Floor(pos.Y / 50);
            // MesssageLabel.Content 
            //     = string.Format("MouseDown: {0}, {1} ({2})",
            //                     pos.X, pos.Y, e.Source.GetType().Name);

            if(!Check_OseroSet(col,row,TurnStatus)){
                // チェック結果false
                MesssageLabel.Content = "そこはおけません";
                return;
            }
            arrayBoard[row,col] = TurnStatus ; 

            drawBoard();

            // 駒を置いたら手番を入れ替える
            TurnStatus = TurnStatus * -1;

            if(TurnStatus == -1){
                MesssageLabel.Content = "白の手番です";

            }else{
                MesssageLabel.Content = "黒の手番です";

            }

            // MesssageLabel.Content 
            //     = string.Format("MouseDown: {0}, {1} ({2})",
            //                     row, col, e.Source.GetType().Name);
        }

        /// <summary>
        /// オセロ配置判定処理
        /// </summary>
        private Boolean Check_OseroSet(int col,int row, int kind)
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

                    if(target == nowTurn){
                        // 2つ目以降、自分の駒が見つかればtrue
                        chkResult =  true;
                        foreach(int[] stackArray in reverseList){
                            // リストのデータをひっくり返す
                            arrayBoard[stackArray[0],stackArray[1]] = nowTurn;
                        }
                    }
                }
            }

            Console.WriteLine("おけない場所");
            return chkResult;

        }

        // /// <summary>
        // /// リバース処理
        // /// </summary>
        // private Boolean Reverse(int col,int row, int kind)
        // {

        //     int nowTurn = kind;//今の手番
        //     int target = 0;
        //     Boolean chkResult = false;

        //     // 8方向について、それぞれひっくり返す
        //     for(int i = 0; i < 8 ; i++){
        //         // 最大値を決め、その数まで、特定方向に対してチェックする
        //         int iMax = 0;
        //         int rowAdd = 0;
        //         int colAdd = 0;
        //         switch(i){
        //             case 0:  //下　方向
        //                 iMax = row;
        //                 rowAdd = 1;
        //                 colAdd = 0;
        //                 break;
        //             case 1:  //右下方向
        //                 iMax = (row > col) ? row : col;
        //                 rowAdd = 1;
        //                 colAdd = 1;
        //                 break;
        //             case 2:  //右　方向
        //                 iMax = col;
        //                 rowAdd = 0;
        //                 colAdd = 1;
        //                 break;
        //             case 3:  //右上方向
        //                 iMax = ( (7 - row) > col) ? (7 - row) : col;
        //                 rowAdd = -1;
        //                 colAdd = 1;
        //                 break;
        //             case 4:  //上　方向
        //                 iMax = 7 - row;
        //                 rowAdd = -1;
        //                 colAdd = 0;
        //                 break;
        //             case 5:  //左上方向
        //                 iMax = ( (7 - row) > (7 - col) ) ? (7 - row) : (7 - col);
        //                 rowAdd = -1;
        //                 colAdd = -1;
        //                 break;
        //             case 6:  //左　方向
        //                 iMax = 7 - col;
        //                 rowAdd = 0;
        //                 colAdd = -1;
        //                 break;
        //             case 7:  //左下方向
        //                 iMax = ( row > (7 - col) ) ? row : (7 - col);
        //                 rowAdd = 1;
        //                 colAdd = -1;
        //                 break;
        //         }
        //         Console.WriteLine("方向：" + i);

        //         String[] stack = new String[2];
        //         for(int j = 1 ; j + iMax < 8 ; j ++){
        //             target = arrayBoard[row + (j * rowAdd) , col + (j * colAdd) ];
        //             if(j == 1 && target*nowTurn != -1){
        //                 // 一つ目が相手の駒以外(自分の駒or無し)なら、チェック終了 
        //                 break;
        //             }
        //             if(target == nowTurn){
        //                 // 2つ目以降、自分の駒が見つかればtrue
        //                 chkResult = true;
        //             }
        //         }
        //     }

        //     Console.WriteLine("おけない場所");
        //     return chkResult;

        // }

    }


    /// <summary>
    /// enum OseroKind
    /// </summary>



    public enum OseroKind { White=-1, None=0, Black=1 };
}
