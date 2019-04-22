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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WHLib_Toolkit.UIControl.Control
{
    /// <summary>
    /// 路由事件参数
    /// </summary>
    public class TextChangeRoutedEventArgs : RoutedEventArgs
    {
        #region 属性字段

        /// <summary>
        /// 事件文本
        /// </summary>
        public string Text { set; get; }

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="routedEvent">路由事件</param>
        /// <param name="source">事件源</param>
        /// <param name="text">事件文本</param>
        public TextChangeRoutedEventArgs(RoutedEvent routedEvent, object source, string text) : base(routedEvent, source)
        {
            Text = text;
        }
        #endregion

    }

    /// <summary>
    /// WH_SelectPathControl.xaml 的交互逻辑
    /// </summary>
    public partial class WH_SelectPathControl : UserControl
    {
        #region 定义
        /// <summary>
        /// 控件类型
        /// </summary>
        public enum EnumControlType
        {
            /// <summary>
            /// 读取文件
            /// </summary>
            LoadFile,
            /// <summary>
            /// 保存文件
            /// </summary>
            SaveFile,
            /// <summary>
            /// 选择路径
            /// </summary>
            SelectPath,
        }
        
        /// <summary>
        /// 事件委托
        /// </summary>
        /// <param name="sender">响应对象</param>
        /// <param name="e">响应参数</param>
        public delegate void TextChangeRoutedEventHandler(object sender, TextChangeRoutedEventArgs e);
        #endregion

        #region 属性

        #region 依赖项属性

        #region 控件类型

        /// <summary>
        /// 控件类型依赖项
        /// </summary>
        public static readonly DependencyProperty ControlTypeProperty = DependencyProperty.Register(nameof(ControlType), typeof(EnumControlType), typeof(WH_SelectPathControl));

        /// <summary>
        /// 控件类型属性
        /// </summary>
        public EnumControlType ControlType
        {
            set { SetValue(ControlTypeProperty, value); }
            get { return (EnumControlType)GetValue(ControlTypeProperty); }
        }

        #endregion

        #region 文件类型

        /// <summary>
        /// 文件类型依赖项
        /// </summary>
        public static readonly DependencyProperty FilterProperty = DependencyProperty.Register(nameof(Filter), typeof(string), typeof(WH_SelectPathControl));

        /// <summary>
        /// 文件类型属性
        /// </summary>
        public string Filter
        {
            set { SetValue(FilterProperty, value); }
            get { return (string)GetValue(FilterProperty); }
        }

        #endregion

        #region 窗口标题

        /// <summary>
        /// 窗口标题依赖项
        /// </summary>
        public static readonly DependencyProperty TitleDescriptionProperty = DependencyProperty.Register(nameof(TitleDescription), typeof(string), typeof(WH_SelectPathControl));

        /// <summary>
        /// 窗口标题属性
        /// </summary>
        public string TitleDescription
        {
            set { SetValue(TitleDescriptionProperty, value); }
            get { return (string)GetValue(TitleDescriptionProperty); }
        }

        #endregion

        #region 默认目录

        /// <summary>
        /// 默认目录依赖项
        /// </summary>
        public static readonly DependencyProperty DefaultDirectoryProperty = DependencyProperty.Register(nameof(DefaultDirectory), typeof(string), typeof(WH_SelectPathControl));

        /// <summary>
        /// 窗口标题属性
        /// </summary>
        public string DefaultDirectory
        {
            set { SetValue(DefaultDirectoryProperty, value); }
            get { return (string)GetValue(DefaultDirectoryProperty); }
        }

        #endregion

        #region 按钮文本

        /// <summary>
        /// 按钮文本依赖项
        /// </summary>
        public static readonly DependencyProperty ButtonContentProperty = DependencyProperty.Register(nameof(ButtonContent), typeof(string), typeof(WH_SelectPathControl));

        /// <summary>
        /// 按钮文本属性
        /// </summary>
        public string ButtonContent
        {
            set { SetValue(ButtonContentProperty, value); }
            get { return (string)GetValue(ButtonContentProperty); }
        }

        #endregion

        #region 路径文本

        /// <summary>
        /// 路径文本依赖项
        /// </summary>
        public static readonly DependencyProperty PathTextProperty = DependencyProperty.Register(nameof(PathText), typeof(string), typeof(WH_SelectPathControl));
        /// <summary>
        /// 路径文本属性
        /// </summary>
        public string PathText
        {
            set { TextBox_Path.Text = value; SetValue(PathTextProperty, value); }
            get { SetValue(PathTextProperty, TextBox_Path.Text); return (string)GetValue(PathTextProperty); }
        }

        #endregion

        #region 按钮宽度

        /// <summary>
        /// 按钮宽度依赖项
        /// </summary>
        public static readonly DependencyProperty ButtonWidthProperty = DependencyProperty.Register("ButtonWidth", typeof(double), typeof(WH_SelectPathControl));
        /// <summary>
        /// 按钮宽度属性
        /// </summary>
        public double ButtonWidth
        {
            set { SetValue(ButtonWidthProperty, value); }
            get { return (double)GetValue(ButtonWidthProperty); }
        }

        #endregion

        #region 标题内容

        /// <summary>
        /// 标题内容依赖项
        /// </summary>
        public static readonly DependencyProperty LabelContentProperty = DependencyProperty.Register(nameof(LabelContent), typeof(object), typeof(WH_SelectPathControl));

        /// <summary>
        /// 标题内容属性
        /// </summary>
        public object LabelContent
        {
            set { SetValue(LabelContentProperty, value); }
            get { return (double)GetValue(LabelContentProperty); }
        }

        #endregion

        #region 已选择

        /// <summary>
        /// 已选择依赖项
        /// </summary>
        public static readonly DependencyProperty IsHaveSelectedProperty = DependencyProperty.Register(nameof(IsHaveSelected), typeof(bool?), typeof(WH_SelectPathControl));
        /// <summary>
        /// 已选择属性
        /// </summary>
        public bool? IsHaveSelected
        {
            private set { SetValue(IsHaveSelectedProperty, value); }
            get { return (bool?)GetValue(IsHaveSelectedProperty); }
        }

        #endregion

        #region 文本变化路由事件

        /// <summary>
        /// 文本变化路由事件依赖项
        /// </summary>
        public static readonly RoutedEvent TextChangeRoutedEvent = EventManager.RegisterRoutedEvent(nameof(TextChangeHandler), RoutingStrategy.Bubble, typeof(EventHandler<TextChangeRoutedEventArgs>), typeof(WH_SelectPathControl));

        /// <summary>
        ///  文本变化路由事件属性
        /// </summary>
        public event RoutedEventHandler TextChangeHandler
        {
            add { this.AddHandler(TextChangeRoutedEvent, value); }
            remove { this.RemoveHandler(TextChangeRoutedEvent, value); }
        }

        #endregion

        #endregion

        #region 属性

        /// <summary>
        /// 选定文件
        /// </summary>
        public FileInfo SelectedFile { set; get; }

        /// <summary>
        /// 选定目录
        /// </summary>
        public DirectoryInfo SelectedDirectory { set; get; }

        #endregion

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public WH_SelectPathControl()
        {
            InitializeComponent();
            ButtonWidth = double.NaN;
            IsHaveSelected = false;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 打开文件浏览器选择文件
        /// </summary>
        /// <param name="textBox">设置路径的TextBox控件</param>
        /// <param name="filter">文件类型筛选字符串</param>
        /// <param name="title">标题</param>
        /// <param name="defaultPath">默认路径</param>
        /// <returns>OpenFileDialog</returns>
        static System.Windows.Forms.OpenFileDialog OpenFileDialogGetOpenFile(TextBox textBox, string filter, string title, string defaultPath)
        {
            System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
            if (File.Exists(textBox.Text))
            {
                fileDialog.InitialDirectory = new FileInfo(textBox.Text).DirectoryName;
            }
            else if (defaultPath != null && Directory.Exists(defaultPath))
            {
                fileDialog.InitialDirectory = defaultPath;
            }
            else
            {
                fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            }
            fileDialog.Filter = filter;
            fileDialog.Multiselect = false;
            fileDialog.RestoreDirectory = true;
            fileDialog.Title = title;
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox.Text = fileDialog.FileName;
                return fileDialog;
            }
            else
            {
                textBox.Text = "";
                return null;
            }
        }

        /// <summary>
        /// 保存文件选择路径
        /// </summary>
        /// <param name="textBox">设置路径的TextBox控件</param>
        /// <param name="filter">文件类型筛选字符串</param>
        /// <param name="title">标题</param>
        /// <param name="defaultPath">默认路径</param>
        /// <returns>SaveFileDialog</returns>
        static System.Windows.Forms.SaveFileDialog OpenFileDialogGetSavePath(TextBox textBox, string filter, string title, string defaultPath)
        {
            System.Windows.Forms.SaveFileDialog fileDialog = new System.Windows.Forms.SaveFileDialog();

            if (File.Exists(textBox.Text))
            {
                fileDialog.InitialDirectory = new FileInfo(textBox.Text).DirectoryName;
            }
            else if (defaultPath != null && Directory.Exists(defaultPath))
            {
                fileDialog.InitialDirectory = defaultPath;
            }
            else
            {
                fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            }

            fileDialog.Filter = filter;
            fileDialog.RestoreDirectory = true;
            fileDialog.Title = title;
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox.Text = fileDialog.FileName;
                return fileDialog;
            }
            else
            {
                textBox.Text = "";
                return null;
            }
        }
        /// <summary>
        /// 选择文件夹路径
        /// </summary>
        /// <param name="textBox">设置路径的TextBox控件</param>
        /// <param name="description">打开描述</param>
        /// <param name="defaultPath">默认路径</param>
        /// <returns>FolderBrowserDialog</returns>
        static System.Windows.Forms.FolderBrowserDialog OpenDirectoryDialogGetFolder(TextBox textBox, string description, string defaultPath)
        {
            System.Windows.Forms.FolderBrowserDialog dirDialog = new System.Windows.Forms.FolderBrowserDialog();
            if (Directory.Exists(textBox.Text))
            {
                dirDialog.SelectedPath = new FileInfo(textBox.Text).DirectoryName;
            }
            else if (defaultPath != null && Directory.Exists(defaultPath))
            {
                dirDialog.SelectedPath = defaultPath;
            }
            else
            {
                dirDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            }
            dirDialog.ShowNewFolderButton = true;
            dirDialog.Description = description;
            if (dirDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox.Text = dirDialog.SelectedPath;
                return dirDialog;
            }
            else
            {
                textBox.Text = "";
                return null;
            }
        }
        #endregion

        #region 控件方法
        /// <summary>
        /// 点击选择按钮事件
        /// </summary>
        /// <param name="sender">响应对象</param>
        /// <param name="e">响应参数</param>
        private void Button_Select_Click(object sender, RoutedEventArgs e)
        {
            switch (ControlType)
            {
                case EnumControlType.LoadFile:
                    OpenFileDialogGetOpenFile(TextBox_Path, Filter, TitleDescription, DefaultDirectory);
                    break;
                case EnumControlType.SaveFile:
                    OpenFileDialogGetSavePath(TextBox_Path, Filter, TitleDescription, DefaultDirectory);
                    break;
                case EnumControlType.SelectPath:
                    OpenDirectoryDialogGetFolder(TextBox_Path, TitleDescription, DefaultDirectory);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 路径文本变化事件
        /// </summary>
        /// <param name="sender">响应对象</param>
        /// <param name="e">响应参数</param>
        private void TextBox_Path_TextChanged(object sender, TextChangedEventArgs e)
        {
            string path = TextBox_Path.Text;
            if (File.Exists(path))
            {
                SelectedFile = new FileInfo(path);
                IsHaveSelected = true;
            }
            else if (Directory.Exists(path))
            {
                SelectedDirectory = new DirectoryInfo(path);
                IsHaveSelected = true;
            }
            else
            {

                IsHaveSelected = false;
            }
            TextChangeRoutedEventArgs args = new TextChangeRoutedEventArgs(TextChangeRoutedEvent, this, path);
            this.RaiseEvent(args);
        }

        #endregion

    }
}
