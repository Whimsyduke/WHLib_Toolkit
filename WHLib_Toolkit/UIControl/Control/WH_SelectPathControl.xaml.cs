﻿using System;
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
using WHLib_Toolkit.CommonClass.CommonFunc;

namespace WHLib_Toolkit.UIControl.Control
{
    #region 路由事件

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
    /// 路由事件参数
    /// </summary>
    public class IsPathExistRoutedEventArgs : RoutedEventArgs
    {
        #region 属性字段

        /// <summary>
        /// 事件文本
        /// </summary>
        public bool? IsExist { set; get; }

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="routedEvent">路由事件</param>
        /// <param name="source">事件源</param>
        /// <param name="text">事件文本</param>
        public IsPathExistRoutedEventArgs(RoutedEvent routedEvent, object source, bool? exist) : base(routedEvent, source)
        {
            IsExist = exist;
        }
        #endregion

    }

    #endregion

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

        #region 文本水平模式

        /// <summary>
        /// 已选择依赖项
        /// </summary>
        public static readonly DependencyProperty LabelHorizontalAlignmentProperty = DependencyProperty.Register(nameof(LabeltHorizontalAlignment), typeof(HorizontalAlignment), typeof(WH_SelectPathControl));

        /// <summary>
        /// 已选择属性
        /// </summary>
        public HorizontalAlignment LabeltHorizontalAlignment
        {
            set { SetValue(LabelHorizontalAlignmentProperty, value); }
            get { return (HorizontalAlignment)GetValue(LabelHorizontalAlignmentProperty); }
        }

        #endregion

        #region 标题宽度

        /// <summary>
        /// 已选择依赖项
        /// </summary>
        public static readonly DependencyProperty LabelWidthProperty = DependencyProperty.Register(nameof(LabelWidth), typeof(GridLength), typeof(WH_SelectPathControl), new PropertyMetadata(new GridLength(20, GridUnitType.Auto)));

        /// <summary>
        /// 已选择属性
        /// </summary>
        public GridLength LabelWidth
        {
            set { SetValue(LabelWidthProperty, value); }
            get { return (GridLength)GetValue(LabelWidthProperty); }
        }

        #endregion

        #region 文本垂直模式

        /// <summary>
        /// 已选择依赖项
        /// </summary>
        public static readonly DependencyProperty TextVerticalAlignmentProperty = DependencyProperty.Register(nameof(TextVerticalAlignment), typeof(VerticalAlignment), typeof(WH_SelectPathControl));

        /// <summary>
        /// 已选择属性
        /// </summary>
        public VerticalAlignment TextVerticalAlignment
        {
            set { SetValue(TextVerticalAlignmentProperty, value); }
            get { return (VerticalAlignment)GetValue(TextVerticalAlignmentProperty); }
        }

        #endregion

        #region 内容Margin

        /// <summary>
        /// 已选择依赖项
        /// </summary>
        public static readonly DependencyProperty ContentMarginProperty = DependencyProperty.Register(nameof(ContentMargin), typeof(Thickness), typeof(WH_SelectPathControl), new PropertyMetadata(new Thickness(0)));

        /// <summary>
        /// 已选择属性
        /// </summary>
        public Thickness ContentMargin
        {
            set { SetValue(ContentMarginProperty, value); }
            get { return (Thickness)GetValue(ContentMarginProperty); }
        }

        #endregion

        #region Label Margin 

        /// <summary>
        /// 已选择依赖项
        /// </summary>
        public static readonly DependencyProperty LabelMarginProperty = DependencyProperty.Register(nameof(LabelMargin), typeof(Thickness), typeof(WH_SelectPathControl), new PropertyMetadata(new Thickness(0)));

        /// <summary>
        /// 已选择属性
        /// </summary>
        public Thickness LabelMargin
        {
            set { SetValue(LabelMarginProperty, value); }
            get { return (Thickness)GetValue(LabelMarginProperty); }
        }

        #endregion

        #region TextBox Margin

        /// <summary>
        /// 已选择依赖项
        /// </summary>
        public static readonly DependencyProperty TextBoxMarginProperty = DependencyProperty.Register(nameof(TextBoxMargin), typeof(Thickness), typeof(WH_SelectPathControl), new PropertyMetadata(new Thickness(0)));

        /// <summary>
        /// 已选择属性
        /// </summary>
        public Thickness TextBoxMargin
        {
            set { SetValue(TextBoxMarginProperty, value); }
            get { return (Thickness)GetValue(TextBoxMarginProperty); }
        }

        #endregion

        #region Button Margin

        /// <summary>
        /// 已选择依赖项
        /// </summary>
        public static readonly DependencyProperty ButtonMarginProperty = DependencyProperty.Register(nameof(ButtonMargin), typeof(Thickness), typeof(WH_SelectPathControl), new PropertyMetadata(new Thickness(0)));

        /// <summary>
        /// 已选择属性
        /// </summary>
        public Thickness ButtonMargin
        {
            set { SetValue(ButtonMarginProperty, value); }
            get { return (Thickness)GetValue(ButtonMarginProperty); }
        }

        #endregion

        #region 已选择

        /// <summary>
        /// 已选择依赖项
        /// </summary>
        public static readonly DependencyProperty IsPathExistProperty = DependencyProperty.Register(nameof(IsPathExist), typeof(bool?), typeof(WH_SelectPathControl));

        /// <summary>
        /// 已选择属性
        /// </summary>
        public bool? IsPathExist
        {
            private set
            {
                bool isChange = value != IsPathExist;
                SetValue(IsPathExistProperty, value);
                if(isChange)
                {
                    IsPathExistRoutedEventArgs args = new IsPathExistRoutedEventArgs(IsPathExistRoutedEvent, this, value);
                    this.RaiseEvent(args);
                }
            }
            get { return (bool?)GetValue(IsPathExistProperty); }
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

        #region 文本变化路由事件

        /// <summary>
        /// 文本变化路由事件依赖项
        /// </summary>
        public static readonly RoutedEvent IsPathExistRoutedEvent = EventManager.RegisterRoutedEvent(nameof(IsPathExistHandler), RoutingStrategy.Bubble, typeof(EventHandler<IsPathExistRoutedEventArgs>), typeof(WH_SelectPathControl));

        /// <summary>
        ///  文本变化路由事件属性
        /// </summary>
        public event RoutedEventHandler IsPathExistHandler
        {
            add { this.AddHandler(IsPathExistRoutedEvent, value); }
            remove { this.RemoveHandler(IsPathExistRoutedEvent, value); }
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
            IsPathExist = false;
        }
        #endregion

        #region 方法
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
                    WH_CommonFunc.OpenFileDialogGetOpenFile(TextBox_Path, Filter, TitleDescription, DefaultDirectory);
                    break;
                case EnumControlType.SaveFile:
                    WH_CommonFunc.OpenFileDialogGetSavePath(TextBox_Path, Filter, TitleDescription, DefaultDirectory);
                    break;
                case EnumControlType.SelectPath:
                    WH_CommonFunc.OpenDirectoryDialogGetFolder(TextBox_Path, TitleDescription, DefaultDirectory);
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
                IsPathExist = true;
            }
            else if (Directory.Exists(path))
            {
                SelectedDirectory = new DirectoryInfo(path);
                IsPathExist = true;
            }
            else
            {

                IsPathExist = false;
            }
            TextChangeRoutedEventArgs args = new TextChangeRoutedEventArgs(TextChangeRoutedEvent, this, path);
            this.RaiseEvent(args);
        }

        #endregion

    }
}
