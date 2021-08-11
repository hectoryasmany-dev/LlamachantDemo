using System;
using DevExpress.ExpressApp.Xpo;

namespace LlamachantDemo.Blazor.Server.Services {
    public class XpoDataStoreProviderAccessor {
        public IXpoDataStoreProvider DataStoreProvider { get; set; }
    }
}
