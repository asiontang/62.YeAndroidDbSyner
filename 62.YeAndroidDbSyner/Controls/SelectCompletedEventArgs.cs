using System;

namespace YEControl
{
    /// <summary>
    /// 为选择完成时事件传递指定路径信息。
    /// </summary>
    public class SelectCompletedEventArgs : EventArgs
    {
        public SelectCompletedEventArgs(string slectedPath)
        {
            SelectedPath = slectedPath;
        }

        public string SelectedPath
        {
            get;
            set;
        }
    }
}
