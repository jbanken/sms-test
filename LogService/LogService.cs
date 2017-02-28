using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DataProvider.Interfaces;

namespace Logger
{
    public class LogService : Interfaces.ILogService
    {
        public ILogDataProvider LogDataProvider { get; set; }

        public LogService(ILogDataProvider logDataProvider)
        {
            LogDataProvider = logDataProvider;
        }

        public async Task<APILog> LogRequest(APILog log)
        {
            return await LogDataProvider.LogRequest(log);
        }

        public async Task<APILog> LogResponse(APILog log)
        {
            return await LogDataProvider.LogResponse(log);
        }
    }
}
