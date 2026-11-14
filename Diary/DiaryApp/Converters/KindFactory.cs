using DiaryApp.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiaryApp.Converters
{
    public static class KindFactory
    {
        public static bool IsSuccess(MessageKind kind)
        {
            return kind == MessageKind.Success;
        }
        public static bool IsError(MessageKind kind)
        {
            return kind == MessageKind.Error;
        }
        public static bool IsInfo(MessageKind kind)
        {
            return kind == MessageKind.Info;
        }
        public static bool IsWarning(MessageKind kind)
        {
            return kind == MessageKind.Warning;
        }
        public static bool IsUpload(MessageKind kind)
        {
            return kind == MessageKind.Upload;
        }
        public static bool IsDownload(MessageKind kind)
        {
            return kind == MessageKind.Download;
        }
        public static bool IsSelected(MessageKind kind)
        {
            return kind == MessageKind.Selected;
        }
        public static bool IsExecuted(MessageKind kind)
        {
            return kind == MessageKind.Executed;
        }
        public static bool IsUndefined(MessageKind kind)
        {
            return kind == MessageKind.Undefined;
        }
    }
}
