// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.ObjectModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DesktopWinUI3
{
    public class GroupInfoCollection<T> : ObservableCollection<T>
    {
        public string Key { get; set; }
    }
}
