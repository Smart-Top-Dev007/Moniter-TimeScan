using Monitor.TaskControl.Utils;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using System.Windows.Navigation;


namespace Monitor.TaskControl.View
{
    /// <summary>
    /// Interaction logic for ImageModal.xaml
    /// </summary>
    public partial class ImageModal : Window
    {

        public void OnImageShow(string filePath)
        {
            try
            {
                String[] spearator = { "\\" };
                String[] strArray = filePath.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                string strRealFileName = Md5Crypto.OnGetRealName(strArray[strArray.Count() - 1]).Replace("-", ":");
            
                this.Title = strRealFileName;
                byte[] temp = Md5Crypto.OnReadImgFile(filePath);
            
                BitmapImage imageSource = new BitmapImage();
                imageSource.BeginInit();
                MemoryStream ms1 = new MemoryStream(temp);
                imageSource.StreamSource = ms1;
                imageSource.EndInit();
                imgModal.Source = imageSource;
            }
            catch (Exception ex)
            {

            }
        }
        public System.Windows.FrameworkElement Content
        {
            get => (System.Windows.FrameworkElement)GridContent.Children[0];
            set
            {
                GridContent.Children.Clear();
                GridContent.Children.Add(value);
                Rect view = new Rect(0, 0, value.ActualWidth, value.ActualHeight);
                ViewArea = view;
                value.SizeChanged += Value_SizeChanged;
            }
        }

        //public Item AddItem(Rect rect)
        //{
        //    Item item = new Item();
        //    item.Rect = rect;
        //    item.ScrollViewer = this;
        //    GridItem.Children.Add(item);
        //    SelectedItem = item;
        //    return item;
        //}

        //public List<Rect> GetAllRects()
        //{
        //    List<Rect> result = new List<Rect>();
        //    foreach (Item item in GridItem.Children)
        //    {
        //        result.Add(item.Rect);
        //    }

        //    return result;
        //}

        private void Value_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //ZoomInFull();
        }

        Point? lastCenterPositionOnTarget;
        Point? lastMousePositionOnTarget;
        Point? lastDragPoint;

        public ImageModal()
        {
            InitializeComponent();

            scrollViewer.ScrollChanged += OnScrollViewerScrollChanged;
            scrollViewer.PreviewMouseWheel += OnPreviewMouseWheel;

            
            GridContent.MouseLeftButtonDown += OnMouseLeftButtonDown;

            
            GridContent.MouseMove += OnMouseMove;
            
            scrollViewer.MouseMove += ScrollViewer_MouseMove;

           
            scrollViewer.PreviewMouseLeftButtonUp += OnMouseLeftButtonUp;

           
            scrollViewer.PreviewMouseRightButtonDown += ScrollViewer_PreviewMouseRightButtonDown;
            scrollViewer.PreviewMouseRightButtonUp += ScrollViewer_PreviewMouseRightButtonUp;

            scrollViewer.KeyDown += ScrollViewer_KeyDown;

            slider.ValueChanged += OnSliderValueChanged;

            ZoomInFull();


            
        }

        private void ScrollViewer_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        //creating
        Point creatingStart;
        private void ScrollViewer_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void ScrollViewer_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            scrollViewer.ReleaseMouseCapture();
            lastDragPoint = null;
            MoveMode = MoveModes.None;
        }


        private void ScrollViewer_MouseMove(object sender, MouseEventArgs e)
        {
            if (lastDragPoint.HasValue)
            {
                if (moveMode == MoveModes.MoveAll)
                {
                    Point posNow = e.GetPosition(scrollViewer);

                    double dX = posNow.X - lastDragPoint.Value.X;
                    double dY = posNow.Y - lastDragPoint.Value.Y;

                    lastDragPoint = posNow;

                    //scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - dX);
                    //scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - dY);

                    Rect rect = ViewArea;

                    rect.X -= dX / Scale;
                    rect.Y -= dY / Scale;

                    ViewArea = rect;

                    Point pos = e.GetPosition(GridContent);
                }
                else if (moveMode == MoveModes.Creating)
                {
                    Point posNow = e.GetPosition(GridContent);

                    double x = Math.Min(creatingStart.X, posNow.X);
                    double y = Math.Min(creatingStart.Y, posNow.Y);
                    double width = Math.Abs(creatingStart.X - posNow.X);
                    double height = Math.Abs(creatingStart.Y - posNow.Y);

                    //SelectedItem.Rect = new Rect(x, y, width, height);
                }
                else if (SelectedItem != null)
                {
                    //SelectedItem.Move(e, (int)moveMode);
                }
            }
            else
            {

            }
        }



        Rect viewArea = new Rect();

        public double Scale
        {
            get => scaleTransform.ScaleX;
            set
            {
                scaleTransform.ScaleX = value;
                scaleTransform.ScaleY = value;

                
            }
        }

        public Rect ViewArea
        {
            set
            {
                double windowWidth = scrollViewer.ViewportWidth;
                double windowHeight = scrollViewer.ViewportHeight;
                double windowRate = windowWidth / windowHeight;

                if (windowWidth == 0)
                {
                    windowWidth = scrollViewer.ActualWidth;
                    windowHeight = scrollViewer.ActualHeight;
                }

                double a = GridContent.Width;

                //double contentWidth = scrollViewer.ExtentWidth;
                //double contentHeight = scrollViewer.ExtentHeight; 
                double contentWidth = grid.ActualWidth;
                double contentHeight = grid.ActualHeight;
                double contentRate = contentWidth / contentHeight;

                //oriented in content.
                Rect rect = value;

                if (rect.Width == 0 || contentWidth == 0 || windowWidth == 0)
                {
                    viewArea = rect;
                    return;
                }

                //--decide scale
                //allowed by scrollViewer
                double minScale = Math.Min(windowWidth / contentWidth, windowHeight / contentHeight);


                double scaleX = Math.Max(windowWidth / rect.Width, minScale);
                double scaleY = Math.Max(windowHeight / rect.Height, minScale);

                double scale;
                //(x or y) axis should be extended.
                if (scaleX > scaleY)
                {
                    scale = scaleY;
                    double oldWidth = rect.Width;
                    rect.Width = windowWidth / scale;
                    rect.X -= (rect.Width - oldWidth) / 2;//extend from center
                }
                else
                {
                    scale = scaleX;
                    double oldHeight = rect.Height;
                    rect.Height = windowHeight / scale;
                    rect.Y -= (rect.Height - oldHeight) / 2;
                }

                Scale = scale;

                //double extendedWidth = contentWidth * scale;
                //double extendedHeight = contentHeight * scale;

                scrollViewer.ScrollToHorizontalOffset(rect.X * scale);
                scrollViewer.ScrollToVerticalOffset(rect.Y * scale);

                //viewArea = rect;
            }

            get
            {
                return viewArea;
            }
        }

        void ZoomInFull()
        {
            ViewArea = new Rect(0, 0, GridContent.ActualWidth, GridContent.ActualHeight);
        }

        void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (lastDragPoint.HasValue)
            {
                Point posNow = e.GetPosition(scrollViewer);

                double dX = posNow.X - lastDragPoint.Value.X;
                double dY = posNow.Y - lastDragPoint.Value.Y;

                lastDragPoint = posNow;

                //scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - dX);
                //scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - dY);

                Rect rect = ViewArea;

                rect.X -= dX / Scale;
                rect.Y -= dY / Scale;

                ViewArea = rect;

                Point pos = e.GetPosition(GridContent);
            }
            else
            {
                MoveMode = MoveModes.MoveAll;
            }
        }

        void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MouseButtonDownHandler(null, e);
            //Mouse.Capture(scrollViewer);
        }

        void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            double scale = 1;
            if (e.Delta > 0)
            {
                scale /= 1.2;
            }
            if (e.Delta < 0)
            {
                scale *= 1.2;
            }

            lastMousePositionOnTarget = Mouse.GetPosition(grid);

            Point pos = e.GetPosition(GridContent);

            Rect view = ViewArea;

            double nuWidth = view.Width * scale;
            double nuHeight = view.Height * scale;

            // leftSide / total width
            double rateX = (pos.X - view.X) / view.Width;
            view.X -= (nuWidth - view.Width) * rateX;

            //topSide / total height
            double rateY = (pos.Y - view.Y) / view.Height;
            view.Y -= (nuHeight - view.Height) * rateY;

            view.Width = nuWidth;
            view.Height = nuHeight;

            ViewArea = view;
        }

        void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            scrollViewer.ReleaseMouseCapture();
            lastDragPoint = null;
        }

        void OnSliderValueChanged(object sender,
             RoutedPropertyChangedEventArgs<double> e)
        {
            Scale = e.NewValue;

            var centerOfViewport = new Point(scrollViewer.ViewportWidth / 2,
                                             scrollViewer.ViewportHeight / 2);
            lastCenterPositionOnTarget = scrollViewer.TranslatePoint(centerOfViewport, grid);
        }

        void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            double scale = Scale;
            if (double.IsNaN(scale))
            {
                //scale = 1;
            }


            if (scale != 0)
            {
                viewArea.X = scrollViewer.HorizontalOffset / scale;
                viewArea.Y = scrollViewer.VerticalOffset / scale;
                viewArea.Width = scrollViewer.ViewportWidth / scale;
                viewArea.Height = scrollViewer.ViewportHeight / scale;

                double contentWidth = GridContent.ActualWidth;
                double contentHeight = GridContent.ActualHeight;

                if (viewArea.Width > contentWidth)
                {
                    viewArea.X -= (viewArea.Width - contentWidth) / 2;
                }

                if (viewArea.Height > contentHeight)
                {
                    viewArea.Y -= (viewArea.Height - contentHeight) / 2;
                }
            }
        }

        //---------------------------------------------------------------------------------------

        Item selectedItem = null;
        public Item SelectedItem
        {
            get
            {
                return selectedItem;
            }

            set
            {
                //if (selectedItem != null)
                //{
                //    selectedItem.Selected = false;
                //}

                //if (value != null)
                //{
                //    value.UiWidth = Scale;
                //    value.Selected = true;
                //}

                selectedItem = value;
            }
        }

        public enum MoveModes : int
        {
            LeftTop = 0,
            Top = 1,
            RightTop = 2,
            Left = 3,
            Right = 4,
            LeftBottom = 5,
            Bottom = 6,
            RightBottom = 7,
            MoveSelected = 8,

            MoveAll,
            None,
            Creating
        }

        MoveModes moveMode;
        public MoveModes MoveMode
        {
            set
            {
                if (lastDragPoint.HasValue)
                {
                    return;
                }

                Console.WriteLine(value.ToString());
                if (value == MoveModes.LeftTop)
                {
                    Cursor = Cursors.SizeNWSE;
                }
                else if (value == MoveModes.Top)
                {
                    Cursor = Cursors.SizeNS;
                }
                else if (value == MoveModes.RightTop)
                {
                    Cursor = Cursors.SizeNESW;
                }
                else if (value == MoveModes.Left)
                {
                    Cursor = Cursors.SizeWE;
                }
                else if (value == MoveModes.Right)
                {
                    Cursor = Cursors.SizeWE;
                }
                else if (value == MoveModes.LeftBottom)
                {
                    Cursor = Cursors.SizeNESW;
                }
                else if (value == MoveModes.Bottom)
                {
                    Cursor = Cursors.SizeNS;
                }
                else if (value == MoveModes.RightBottom)
                {
                    Cursor = Cursors.SizeNWSE;
                }
                else if (value == MoveModes.MoveSelected)
                {
                    Cursor = Cursors.SizeAll;
                }
                else
                {
                    Cursor = Cursors.Arrow;
                }
                moveMode = value;
            }

            get
            {
                return moveMode;
            }
        }

        public void MouseButtonDownHandler(Item sender, MouseEventArgs e)
        {
            SelectedItem = sender;

            var mousePos = e.GetPosition(scrollViewer);
            if (mousePos.X <= scrollViewer.ViewportWidth && mousePos.Y <
                scrollViewer.ViewportHeight) //make sure we still can use the scrollbars
            {
                lastDragPoint = mousePos;
                Mouse.Capture(scrollViewer);
            }
        }
    }
}
