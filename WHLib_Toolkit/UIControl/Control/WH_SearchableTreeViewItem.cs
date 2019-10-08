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
    /// 可搜索ListViewItem
    /// </summary>
    public class WH_SearchableTreeViewItem : TreeViewItem
    {
        #region 属性字段

        #region 依赖项属性

        #region 搜索标签依赖项属性

        /// <summary>
        /// Icon图标
        /// </summary>
        public static readonly DependencyProperty IconImageProperty = DependencyProperty.Register(nameof(IconImage), typeof(object), typeof(WH_SearchableTreeViewItem));

        /// <summary>
        /// Icon图标
        /// </summary>
        public object IconImage
        {
            set { SetValue(IconImageProperty, value); }
            get { return (BitmapImage)GetValue(IconImageProperty); }
        }

        #endregion 搜索标签依赖项属性

        #region 构造函数

        #endregion 依赖项属性

        #endregion 属性字段

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static WH_SearchableTreeViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WH_SearchableTreeViewItem), new FrameworkPropertyMetadata(typeof(WH_SearchableTreeViewItem)));
        }

        #endregion 构造函数
    }
}
