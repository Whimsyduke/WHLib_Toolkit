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

namespace WHLib_Toolkit.UIControl.Control
{
    /// <summary>
    /// 可搜索TreeView
    /// </summary>
    [TemplatePart(Name = "PART_TextBlock_Label", Type = typeof(TextBlock))]
    [TemplatePart(Name = "PART_TextBox_Search", Type = typeof(TextBox))]
    public class WH_SearchableTreeView : TreeView
    {
        #region 属性字段

        #region 依赖项属性

        #region 搜索标签依赖项属性

        /// <summary>
        /// 搜索标签依赖项
        /// </summary>
        public static readonly DependencyProperty SearchLableProperty = DependencyProperty.Register(nameof(SearchLable), typeof(object), typeof(WH_SearchableTreeView));

        /// <summary>
        /// 搜索标签依赖项属性
        /// </summary>
        public object SearchLable
        {
            set => SetValue(SearchLableProperty, value);
            get => GetValue(SearchLableProperty);
        }

        #endregion 搜索标签依赖项属性

        #region 文本变化路由事件

        /// <summary>
        /// 搜索文本变化路由事件依赖项
        /// </summary>
        public static readonly RoutedEvent SearchChangeRoutedEvent = EventManager.RegisterRoutedEvent(nameof(SearchChange), RoutingStrategy.Bubble, typeof(TextChangedEventHandler), typeof(WH_SearchableTreeView));

        /// <summary>
        ///  搜索文本变化路由事件属性
        /// </summary>
        public event TextChangedEventHandler SearchChange
        {
            add
            {
                AddHandler(SearchChangeRoutedEvent, value);
            }
            remove
            {
                RemoveHandler(SearchChangeRoutedEvent, value);
            }
        }

        #endregion 文本变化路由事件

        #endregion 依赖项属性

        #region 属性

        /// <summary>
        /// 搜索Label子控件
        /// </summary>
        public TextBlock TextBlock_Label { private set; get; }

        /// <summary>
        /// 搜索框子控件
        /// </summary>
        public TextBox TextBox_Search { private set; get; }

        #endregion 属性

        #endregion 属性字段

        #region 构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static WH_SearchableTreeView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WH_SearchableTreeView), new FrameworkPropertyMetadata(typeof(WH_SearchableTreeView)));
        }

        #endregion 构造函数

        #region 方法

        #region 普通方法

        /// <summary>
        /// 加载模板子控件
        /// </summary>
        public void LoadPartControl()
        {
            TextBlock_Label = GetTemplateChild($"PART_{nameof(TextBlock_Label)}") as TextBlock;
            TextBox_Search = GetTemplateChild($"PART_{nameof(TextBox_Search)}") as TextBox;
            TextBox_Search.TextChanged += TextBox_Search_TextChanged;
        }

        #endregion 普通方法

        #region 重载方法

        /// <summary>
        /// 重载加载模板
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            LoadPartControl();
        }

        #endregion 重载方法

        #endregion 方法

        #region 属性字段

        /// <summary>
        /// 搜索文本变化事件
        /// </summary>
        /// <param name="sender">响应对象</param>
        /// <param name="e">响应参数</param>
        private void TextBox_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextChangedEventArgs args = new TextChangedEventArgs(SearchChangeRoutedEvent, e.UndoAction, e.Changes);
            this.RaiseEvent(args);
        }

        #endregion 属性字段
    }
}
