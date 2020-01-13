using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WHLib_Toolkit.CommonClass.CommonFunc
{
    /// <summary>
    /// 通用函数
    /// </summary>
    public static class WH_CommonFunc
    {
        #region 方法

        /// <summary>
        /// 打开文件浏览器选择文件
        /// </summary>
        /// <param name="textBox">设置路径的TextBox控件</param>
        /// <param name="filter">文件类型筛选字符串</param>
        /// <param name="title">标题</param>
        /// <param name="defaultPath">默认路径</param>
        /// <returns>OpenFileDialog</returns>
        public static System.Windows.Forms.OpenFileDialog OpenFileDialogGetOpenFile(TextBox textBox, string filter, string title, string defaultPath)
        {
            using (System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog())
            {
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
        }

        /// <summary>
        /// 保存文件选择路径
        /// </summary>
        /// <param name="textBox">设置路径的TextBox控件</param>
        /// <param name="filter">文件类型筛选字符串</param>
        /// <param name="title">标题</param>
        /// <param name="defaultPath">默认路径</param>
        /// <returns>SaveFileDialog</returns>
        public static System.Windows.Forms.SaveFileDialog OpenFileDialogGetSavePath(TextBox textBox, string filter, string title, string defaultPath)
        {
            using (System.Windows.Forms.SaveFileDialog fileDialog = new System.Windows.Forms.SaveFileDialog())
            {
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
        }
        /// <summary>
        /// 选择文件夹路径
        /// </summary>
        /// <param name="textBox">设置路径的TextBox控件</param>
        /// <param name="description">打开描述</param>
        /// <param name="defaultPath">默认路径</param>
        /// <returns>FolderBrowserDialog</returns>
        public static System.Windows.Forms.FolderBrowserDialog OpenDirectoryDialogGetFolder(TextBox textBox, string description, string defaultPath)
        {
            using (System.Windows.Forms.FolderBrowserDialog dirDialog = new System.Windows.Forms.FolderBrowserDialog())
            {
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
        }

        /// <summary>
        /// 打开文件路径窗口
        /// </summary>
        /// <param name="baseFolder">默认打开目录</param>
        /// <param name="filter">文件类型验证</param>
        /// <param name="title">标题</param>
        /// <param name="fileDialog">对话框</param>
        /// <returns>打开结果</returns>
        public static System.Windows.Forms.DialogResult OpenFilePathDialog(string baseFolder, string filter, string title, out System.Windows.Forms.OpenFileDialog fileDialog)
        {
            fileDialog = new System.Windows.Forms.OpenFileDialog();
            if (Directory.Exists(baseFolder))
            {
                fileDialog.InitialDirectory = baseFolder;
            }
            else
            {
                fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
            fileDialog.Filter = filter;
            fileDialog.Title = title;
            fileDialog.Multiselect = false;
            fileDialog.RestoreDirectory = true;
            return fileDialog.ShowDialog();
        }

        /// <summary>
        /// 保存文件路径窗口
        /// </summary>
        /// <param name="baseFolder">默认保存目录</param>
        /// <param name="filter">文件类型验证</param>
        /// <param name="title">标题</param>
        /// <param name="fileDialog">对话框</param>
        /// <returns>打开结果</returns>
        public static System.Windows.Forms.DialogResult SaveFilePathDialog(string baseFolder, string filter, string title, out System.Windows.Forms.SaveFileDialog fileDialog)
        {
            fileDialog = new System.Windows.Forms.SaveFileDialog();
            if (Directory.Exists(baseFolder))
            {
                fileDialog.InitialDirectory = baseFolder;
            }
            else
            {
                fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
            fileDialog.Filter = filter;
            fileDialog.Title = title;
            fileDialog.RestoreDirectory = true;
            return fileDialog.ShowDialog();
        }

        #endregion
    }
}
