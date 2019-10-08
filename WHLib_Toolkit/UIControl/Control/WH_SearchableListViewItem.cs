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
    /// 可搜索TreeViewItem
    /// </summary>
    public class WH_SearchableListViewItem : ListViewItem
    {
        #region 构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static WH_SearchableListViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WH_SearchableListViewItem), new FrameworkPropertyMetadata(typeof(WH_SearchableListViewItem)));
        }

        #endregion 构造函数
    }
}
