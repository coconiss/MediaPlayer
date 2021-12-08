﻿using NReco.VideoInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace WpfApp2
{
    /// <summary>
    /// PlayListForm.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PlayListForm : Window
    {
        
        MainWindow MainWindow;
        List<String[]> videoList = new List<string[]>();//받아온 영상목록
        List<MovieData> videoData;
        Stack<String> folder_parent_List = new Stack<string>(); //상위폴더 스택
        BookmarkForm BookmarkForm;

        String mainFolder = ""; // 메인 폴더 경로
        Memo memo = null;

        public String _mainFolder
        {
            get { return this.mainFolder; }

        }

        String nowFolder = "";
        public PlayListForm(List<String[]> videoList, MainWindow mainWindow, String mainFolder, BookmarkForm BookmarkForm)
        {
            InitializeComponent();



            this.Height = mainWindow.Height;

            this.BookmarkForm = BookmarkForm;
            this.MainWindow = mainWindow;// 값 전달을 위한 메인화면 가져오기

            this.videoList = videoList;//영상목록 받아오기

            this.mainFolder = mainFolder;//메인폴더 경로
            this.nowFolder = mainFolder;
            this.memo = new Memo();


            DirectoryInfo di = new DirectoryInfo("thumbnail");
            /*if (di.Exists == true)//썸네일 폴더 생성
            {
                try
                {
                    di.Delete(true);
                }
                catch
                {

                }
            }*/


            di.Create();

            di = new DirectoryInfo("thumbnail/preview");
            di.Create();

            getList();//영상 리스트 만들기

            this.ShowInTaskbar = false;//작업표시줄에 표시 안함



        }

        private void getList() //영상리스트 메소드
        {

            pathLbl.Content = nowFolder;

            videoData = new List<MovieData>(); // 영상 리스트

            MovieData md = new MovieData(); // 메인폴더로 가는 버튼
            md.ImageData = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Images\Folder.png"));

            md.Title = "홈";
            md.mediaSource = mainFolder;
            md.type = true;
            videoData.Add(md);


            MovieData pd = new MovieData(); // 이전폴더로 가는 버튼 
            pd.ImageData = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Images\Folder.png"));
            pd.Title = "상위 폴더";
            pd.type = true;
            videoData.Add(pd);

            for (int i = 0; i < videoList.Count; i++)
            {
                MovieData vd = new MovieData();
                if (videoList[i][2] == "")//확장자가 폴더면
                {
                    vd.ImageData = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Images\Folder.png"));

                    vd.Title = videoList[i][1];
                    vd.mediaSource = videoList[i][0];
                    vd.type = true;
                    videoData.Add(vd);
                }

                else if (videoList[i][2].Equals(".mp4") || videoList[i][2].Equals(".m4p") || videoList[i][2].Equals(".avi") || videoList[i][2].Equals(".wmv"))
                {
                    vd.Title = videoList[i][1];
                    vd.mediaSource = videoList[i][0] + "/" + videoList[i][1];
                    vd.type = false;

                    int time = Image.get_VedioTime(videoList[i][0] + "/" + videoList[i][1]);

                    vd.Time = Image.TimeToString(time);
                    vd.ImageData = Image.LoadImage(videoList[i][0] + "/" + videoList[i][1], videoList[i][1], time);

                    vd.star = memo.starPoint_Output(videoList[i][0] + "/" + videoList[i][1]);



                    if (vd.star > 0)//별점 출력
                    {
                        vd.starText = "";
                        for (int s = 1; s <= vd.star; s++)
                        {
                            vd.starText += "★";
                        }
                    }

                    videoData.Add(vd);
                }
            }

            if (folder_parent_List.Count <= 0)//최상위 폴더면
            {
                pd.mediaSource = mainFolder;
            }
            else
            {
                pd.mediaSource = folder_parent_List.Pop();
            }

            TvBox.ItemsSource = videoData;

        }

        private void getList(List<String[]> videoList) //검색 시에 사용할 리스트 생성
        {

            videoData.Clear();

            videoData = new List<MovieData>(); // 영상 리스트


            for (int i = 0; i < videoList.Count; i++)
            {
                MovieData vd = new MovieData();
                if (videoList[i][2] == "")//확장자가 폴더면
                {
                    vd.ImageData = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"..\..\Images\Folder.png"));


                    vd.Title = videoList[i][1];
                    vd.mediaSource = videoList[i][0];
                    vd.type = true;
                    videoData.Add(vd);
                }

                else if (videoList[i][2].Equals(".mp4") || videoList[i][2].Equals(".m4p") || videoList[i][2].Equals(".avi") || videoList[i][2].Equals(".wmv"))
                {
                    vd.Title = videoList[i][1];
                    vd.mediaSource = videoList[i][0] + "/" + videoList[i][1];
                    vd.type = false;

                    int time = Image.get_VedioTime(videoList[i][0] + "/" + videoList[i][1]);

                    vd.Time = Image.TimeToString(time);
                    vd.ImageData = Image.LoadImage(videoList[i][0] + "/" + videoList[i][1], videoList[i][1], time);
                    videoData.Add(vd);
                }


            }




            TvBox.ItemsSource = videoData;

        }

        

        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }


        public string runfile { get; set; }
        private void TvBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ButtonState == MouseButtonState.Pressed)
            {
                if (TvBox.SelectedItems.Count == 1)
                {
                    DependencyObject dep = (DependencyObject)e.OriginalSource;

                    while ((dep != null) && !(dep is ListViewItem))
                    {
                        dep = VisualTreeHelper.GetParent(dep);
                    }

                    if (dep == null) return;

                    ListSelectEvent(dep);
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }
        //Thread t1 = null;
        //bool SearchI = false;
        private void Search_Text_TextChanged(object sender, TextChangedEventArgs e)
        {


            if (Search_Text.Text == "")//검색창이 비어있으면
            {
                getList();
                return;
            }
            /*
           if (t1 != null)
            {
                t1.Abort();
            }

           t1 = new Thread(
                (search) =>
                {
                    List<String[]> videoList = new List<string[]>();//받아온 영상목록
                    DirectoryInfo di = new DirectoryInfo(nowFolder); // 해당 폴더 정보를 가져옵니다. 
                    foreach (FileInfo File in di.GetFiles("*" + search + "*", SearchOption.AllDirectories)) // 선택 폴더의 파일 목록을 스캔합니다. 
                    {
                        String[] video = new string[] { File.DirectoryName, File.Name, File.Extension };
                        videoList.Add(video);
                        getList(videoList);
                        SearchI = true;
                    }
                    TvBox.Dispatcher.Invoke(DispatcherPriority.DataBind , new Action(
                        () =>
                        {
                            TvBox.ItemsSource = videoData;
                            
                            

                        }
                        ));
                }
                );
            
            t1.Start(Search_Text.Text);


            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                     (ThreadStart)delegate ()
                     {
                         List<String[]> videoList = new List<string[]>();//받아온 영상목록
                         DirectoryInfo di = new DirectoryInfo(nowFolder); // 해당 폴더 정보를 가져옵니다. 
                         foreach (FileInfo File in di.GetFiles("*" + Search_Text.Text + "*", SearchOption.AllDirectories)) // 선택 폴더의 파일 목록을 스캔합니다. 
                         {
                             String[] video = new string[] { File.DirectoryName, File.Name, File.Extension };
                             videoList.Add(video);
                             getList(videoList);
                             SearchI = true;
                             TvBox.ItemsSource = videoData;
                         }
                     }
            );

           if (SearchI) {
                MessageBox.Show("asd");

                TvBox.ItemsSource = videoData;
                SearchI = false;
            }*/

        }

        private void test()
        {
            TvBox.ItemsSource = videoData;
        }



        private void Search_btn_Click(object sender, RoutedEventArgs e)
        {
            List<String[]> videoList = new List<string[]>();//받아온 영상목록
            DirectoryInfo di = new DirectoryInfo(nowFolder); // 해당 폴더 정보를 가져옵니다. 
            foreach (FileInfo File in di.GetFiles("*" + Search_Text.Text + "*", SearchOption.AllDirectories)) // 선택 폴더의 파일 목록을 스캔합니다. 
            {
                String[] video = new string[] { File.DirectoryName, File.Name, File.Extension };
                videoList.Add(video);

            }

            getList(videoList);

        }

        private void pathLbl_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        MovieData seleteMD = null;//우클릭한 영상 저장
        private void TvBox_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)//리스트 우클릭 이벤트
        {
            optionStatk.Visibility = Visibility.Hidden;
            starStatk.Visibility = Visibility.Hidden;
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null) return;

            seleteMD = (MovieData)TvBox.ItemContainerGenerator.ItemFromContainer(dep);




            if (seleteMD.type)
            {
                seleteMD = null;
                optionStatk.Visibility = Visibility.Hidden;
                starStatk.Visibility = Visibility.Hidden;
                return;
            }

            Point pos = e.GetPosition((IInputElement)sender);
            optionStatk.Margin = new Thickness(pos.X, pos.Y, 0, 0);
            optionStatk.Visibility = Visibility.Visible;

        }
        private void TvBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            optionStatk.Visibility = Visibility.Hidden;
            starStatk.Visibility = Visibility.Hidden;
            SortStack.Visibility = Visibility.Hidden;
        }

        bool sort_Switch = false;
        private void SortBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!sort_Switch)
            {
                SortStack.Visibility = Visibility.Visible;
                sort_Switch = true;
            }
            else
            {
                SortStack.Visibility = Visibility.Hidden;
                sort_Switch = false;
            }
        }

        bool starSortI = true;
        private void StarSortBtn_Click(object sender, RoutedEventArgs e)
        {
            if (starSortI)//내림
            {
                StarSortBtn.Content = "별점↓";
                starSortI = false;

                videoData = videoData.OrderByDescending(x => x.type).ThenByDescending(x => x.star).ToList();
                TvBox.ItemsSource = videoData;
                TvBox.Items.Refresh();

            }
            else//오름
            {
                StarSortBtn.Content = "별점↑";
                starSortI = true;

                videoData = videoData.OrderByDescending(x => x.type).ThenBy(x => x.star).ToList();//오름차순 정렬
                TvBox.ItemsSource = videoData;
                TvBox.Items.Refresh();
            }
        }

        bool NameSortI = true;
        private void NameSortBtn_Click(object sender, RoutedEventArgs e)//이름 정렬
        {
            if (NameSortI)//내림차순 정렬
            {
                NameSortBtn.Content = "이름↓";
                NameSortI = false;

                videoData = videoData.OrderByDescending(x => x.type).ThenByDescending(x => x.Title).ToList();
                TvBox.ItemsSource = videoData;
                TvBox.Items.Refresh();

            }
            else//오름차순 정렬
            {
                NameSortBtn.Content = "이름↑";
                NameSortI = true;

                videoData = videoData.OrderByDescending(x => x.type).ThenBy(x => x.Title).ToList();//오름차순 정렬
                TvBox.ItemsSource = videoData;
                TvBox.Items.Refresh();
            }
        }

        private void StarBtn_Click(object sender, RoutedEventArgs e)//별점 클릭 이벤트
        {
            starStatk.Margin = new Thickness(optionStatk.Margin.Left + 40, optionStatk.Margin.Top, 0, 0);
            starStatk.Visibility = Visibility.Visible;

            if (starStatk.Margin.Left + starStatk.Width > this.Width)
            {
                starStatk.Margin = new Thickness(starStatk.Margin.Left - (starStatk.Margin.Left + starStatk.Width - this.Width), optionStatk.Margin.Top, 0, 0);
            }
        }



        private void starSetBtn_Click(object sender, RoutedEventArgs e)//별점 저장 버튼
        {


            if (star1.IsChecked == true)
            {
                seleteMD.star = 1;
                seleteMD.starText = "★";
            }
            else if (star2.IsChecked == true)
            {
                seleteMD.star = 2;
                seleteMD.starText = "★★";
            }
            else if (star3.IsChecked == true)
            {
                seleteMD.star = 3;
                seleteMD.starText = "★★★";
            }
            else if (star4.IsChecked == true)
            {
                seleteMD.star = 4;
                seleteMD.starText = "★★★★";
            }
            else if (star5.IsChecked == true)
            {
                seleteMD.star = 5;
                seleteMD.starText = "★★★★★";
            }
            else
            {
                MessageBox.Show("별점을 선택해주세요");
            }
            optionStatk.Visibility = Visibility.Hidden;
            starStatk.Visibility = Visibility.Hidden;
            memo.starPoint_Input(seleteMD.mediaSource, seleteMD.star.ToString());


            TvBox.ItemsSource = videoData;
            TvBox.Items.Refresh();
        }

        private void vdDeleteBtn_Click(object sender, RoutedEventArgs e)//파일 삭제 이벤트
        {
            MessageBoxResult result = MessageBox.Show("삭제 하시겠습니까?", "파일 삭제", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
            {
                optionStatk.Visibility = Visibility.Hidden;
                starStatk.Visibility = Visibility.Hidden;

                return;
            }

            if (System.IO.File.Exists(seleteMD.mediaSource))
            {
                try
                {
                    System.IO.File.Delete(seleteMD.mediaSource);
                    memo.data_Delete(seleteMD.mediaSource);
                    videoData.Remove(seleteMD);
                    TvBox.ItemsSource = videoData;
                    TvBox.Items.Refresh();

                    optionStatk.Visibility = Visibility.Hidden;
                    starStatk.Visibility = Visibility.Hidden;
                }
                catch
                {
                    // handle exception
                }
            }




        }

        public string mediaSource { get; set; }
        private void ListSelectEvent(DependencyObject dep)
        {
            MovieData movieData = new MovieData();
            movieData = (MovieData)TvBox.ItemContainerGenerator.ItemFromContainer(dep);

            if (movieData.type)//폴더면
            {
                if (nowFolder.Equals(movieData.mediaSource))//현재폴더랑 같으면
                {
                    MessageBox.Show("현재폴더입니다.");
                    return;
                }

                if (movieData.Title == "상위 폴더")//상위폴더면
                {
                    nowFolder = movieData.mediaSource;

                }
                else if (movieData.Title == "홈")//홈이면 
                {
                    nowFolder = mainFolder;
                    folder_parent_List.Clear();
                }
                else
                {
                    folder_parent_List.Push(nowFolder);//스택에 추가 
                    nowFolder = movieData.mediaSource;

                }
                pathLbl.Content = nowFolder;

                List<String[]> videoList = new List<string[]>();//영상목록 리스트

                DirectoryInfo di = new DirectoryInfo(nowFolder); // 해당 폴더 정보를 가져옵니다. 

                videoList.Clear();

                DirectoryInfo[] dirs = di.GetDirectories("*.*", SearchOption.TopDirectoryOnly);//폴더 목록 검색
                foreach (DirectoryInfo d in dirs)
                {
                    if (d.Name == "thumbnail")
                    {//썸네일 폴더 안나오게
                        continue;
                    }

                    String[] dir = new string[] { d.FullName, d.Name, d.Extension };
                    videoList.Add(dir);
                }

                foreach (FileInfo File in di.GetFiles()) // 선택 폴더의 파일 목록을 스캔합니다. 
                {

                    String[] video = new string[] { File.DirectoryName, File.Name, File.Extension };
                    videoList.Add(video);
                }
                this.videoList = videoList;


                getList();


            }

            else
            {
                MainWindow.Media_Play(movieData.mediaSource);
                MainWindow.p = 0;
                MainWindow.btnPause.Visibility = Visibility.Visible;
                MainWindow.btnStart.Visibility = Visibility.Hidden;
                BookmarkForm.BookMarkSet(new Uri(movieData.mediaSource));
                MainWindow.Focus();
                MainWindow.Thumbnail_Create(movieData.mediaSource);
                mediaSource = movieData.mediaSource;
            }
        }
        private void TvBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (TvBox.SelectedItems.Count == 1)
                {
                    DependencyObject dep = (DependencyObject)e.OriginalSource;

                    while ((dep != null) && !(dep is ListViewItem))
                    {
                        dep = VisualTreeHelper.GetParent(dep);
                    }

                    if (dep == null) return;

                    ListSelectEvent(dep);


                }
            }
        }



        private void TvBox_MouseWheel(object sender, MouseWheelEventArgs e)
        {

            optionStatk.Visibility = Visibility.Hidden;
            starStatk.Visibility = Visibility.Hidden;
        }

        String _reName = "";
        public String reName
        {
            get { return _reName; }
            set { _reName = value; }
        }

        String _extension;

        public String extension
        {
            get { return _extension; }
            set { _extension = value;}
        }
        private void vdEditBtn_Click(object sender, RoutedEventArgs e)
        {
            reName = System.IO.Path.GetFileName(seleteMD.mediaSource);
            reName = reName.Substring(0, reName.Length - 4);
            extension = System.IO.Path.GetExtension(seleteMD.mediaSource);

            string before_name = seleteMD.mediaSource; //바뀌기전 이름

            optionStatk.Visibility = Visibility.Hidden;
            starStatk.Visibility = Visibility.Hidden;

            RenameForm renameForm = new RenameForm(this);
            renameForm.Top = this.Top;
            renameForm.Left = this.Left;
            renameForm.ShowDialog();

            if (reName != "") {

               

                try
                {
                    File.Move(seleteMD.mediaSource, nowFolder + "/" + _reName + extension);
                    seleteMD.Title = reName + extension;

                    seleteMD.mediaSource = nowFolder + "/" + _reName + extension;
                    memo.Name_Change(before_name, seleteMD.mediaSource);
                    TvBox.ItemsSource = videoData;
                    TvBox.Items.Refresh();

                    
                }
                catch
                {

                }
            } 
        }
    }


}
